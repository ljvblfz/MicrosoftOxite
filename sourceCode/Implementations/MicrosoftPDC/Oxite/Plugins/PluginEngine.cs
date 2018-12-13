//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using Oxite.Infrastructure;
using Oxite.Plugins.Extensions;

namespace Oxite.Plugins
{
    /// <summary>
    /// Central repository of plugins
    /// </summary>
    public class PluginEngine : IPluginEngine
    {
        private readonly ReflectionPluginEngine reflectionEngine;
        private readonly object initializationLock;
        private bool isInitialized;
        private List<PluginAssemblyContainer> pluginAssemblies;
        private List<PluginContainer> plugins;

        public PluginEngine()
        {
            SupportedFileExtensions = new [] { ".cs", ".vb" };
            AutoInitializePlugins = true;
            pluginAssemblies = new List<PluginAssemblyContainer>(10);
            plugins = new List<PluginContainer>(10);
            reflectionEngine = new ReflectionPluginEngine();
            initializationLock = new object();
        }

        public string[] SupportedFileExtensions { get; set; }
        public bool AutoInitializePlugins { get; set; }

        public void LoadAssembliesFromCodeFiles(string virtualPath)
        {
            pluginAssemblies.AddRange(loadDynamicAssemblies(virtualPath));
        }

        public IEnumerable<PluginContainer> GetPlugins()
        {
            return GetPlugins(null);
        }

        public IEnumerable<PluginContainer> GetPlugins(Func<PluginContainer, bool> predicate)
        {
            if (predicate != null)
                return plugins.Where(predicate);

            return plugins;
        }
        
        public void LoadPlugins()
        {
            if (!isInitialized)
            {
                lock (initializationLock)
                {
                    if (!isInitialized)
                    {
                        plugins.AddRange(loadPlugins());
                        isInitialized = true;
                    }
                }
            }
        }

        public PluginContainer LoadPlugin(string virtualPath)
        {
            PluginAssemblyContainer assembly = compileAssembly(virtualPath, pluginAssemblies.Where(pa => string.Compare(pa.VirtualPath, virtualPath, true) == 0).FirstOrDefault());

            if (!assembly.IsLoaded) return null;

            PluginContainer plugin = loadPlugins(assembly).FirstOrDefault();

            if (plugin != null)
                return plugin;
            
            return null;
        }

        public void ReloadPlugins(string virtualPath)
        {
            ReloadPlugins(virtualPath, null);
        }

        public void ReloadPlugins(string virtualPath, Func<PluginContainer, bool> getPluginsToReload)
        {
            lock (plugins)
            {
                // figure out which plugins should be kept around (those that are installed) and which plugins should be reloaded
                IEnumerable<PluginContainer> pluginsToReload = getPluginsToReload != null ? plugins.Where(getPluginsToReload) : plugins;
                IEnumerable<PluginContainer> pluginsToKeep = getPluginsToReload != null ? plugins.Where(p => !pluginsToReload.Contains(p)) : Enumerable.Empty<PluginContainer>();
                // based on which plugins should be reloaded, figure out which assemblies those plugins came from and which assemblies should be kept (all others)
                IEnumerable<PluginAssemblyContainer> assembliesToReload = pluginAssemblies.Where(pa => pluginsToReload.Cast<PluginAssemblyContainer>().Contains(pa, new PluginAssemblyContainerComparer()));
                IEnumerable<PluginAssemblyContainer> assembliesToKeep = pluginAssemblies.Where(pa => !assembliesToReload.Contains(pa, new PluginAssemblyContainerComparer()));
                
                // load all dynamically compiled assemblies again
                IEnumerable<PluginAssemblyContainer> loadedAssemblies = loadDynamicAssemblies(virtualPath);
                // remove all assemblies that should be reloaded
                pluginAssemblies.RemoveAll(pa => assembliesToReload.Contains(pa, new PluginAssemblyContainerComparer()));
                // add all the newly compiled assemblies that weren't in the assemblies that should be kept list
                pluginAssemblies.AddRange(loadedAssemblies.Where(pa => !assembliesToKeep.Contains(pa, new PluginAssemblyContainerComparer())));

                // remove newly loaded assemblies that have errors where there was a matching plugin to keep without errors
                pluginAssemblies.Where(pa => !pa.IsLoaded && pluginsToKeep.Where(p => p.IsLoaded).Select(p => p.VirtualPath).Contains(pa.VirtualPath, StringComparer.OrdinalIgnoreCase)).ToList().ForEach(pa => pluginAssemblies.Remove(pa));

                // load all plugins from the newly compiled assemblies
                IEnumerable<PluginContainer> loadedPlugins = loadPlugins();
                // remove all plugins that should be reloaded
                pluginsToReload.ToList().ForEach(p => plugins.Remove(p));
                // add all the newly loaded plugins that weren't in the plugins that should be kept list
                plugins.AddRange(loadedPlugins.Where(p => !pluginsToKeep.Select(pliSub => pliSub.VirtualPath).Contains(p.VirtualPath, StringComparer.OrdinalIgnoreCase)));
            }
        }

        public PluginContainer ReloadPlugin(Func<PluginContainer, bool> findPlugin, Func<PluginContainer, string> getVirtualPath)
        {
            lock (plugins)
            {
                PluginContainer foundPlugin = plugins.FirstOrDefault(findPlugin);
                PluginContainer pluginToAdd;

                if (foundPlugin != null)
                {
                    //TODO: (erikpo) Check the file date to make sure the plugin even needs reloaded

                    string virtualPath = getVirtualPath(foundPlugin);

                    if (!string.IsNullOrEmpty(virtualPath))
                    {
                        List<PluginContainer> pluginsToRemove = plugins.Where(p => string.Compare(p.VirtualPath, virtualPath, true) == 0).ToList();
                        PluginAssemblyContainer pluginAssemblyContainer = compileAssembly(virtualPath, foundPlugin);

                        pluginsToRemove.ForEach(p => plugins.Remove(p));
                        pluginAssemblies.Remove(pluginAssemblies.First(pa => string.Compare(pa.VirtualPath, virtualPath, true) == 0));

                        pluginAssemblies.Add(pluginAssemblyContainer);

                        pluginToAdd = loadPlugins(pluginAssemblyContainer).FirstOrDefault();
                    }
                    else
                    {
                        plugins.Remove(foundPlugin);

                        pluginToAdd = loadPlugins(foundPlugin).FirstOrDefault();
                    }

                    if (pluginToAdd != null)
                    {
                        plugins.Add(pluginToAdd);

                        return pluginToAdd;
                    }
                }

                return null;
            }
        }

        public void AddPlugin(PluginContainer pluginContainer)
        {
            plugins.Add(pluginContainer);
        }

        public void RemovePlugin(PluginContainer pluginContainer)
        {
            plugins.Remove(pluginContainer);
        }

        public TResult Execute<TResult>(string pluginName, string operation, object parameters, Func<PluginContainer, bool> predicate)
        {
            if (predicate != null)
                return reflectionEngine.Execute<TResult>(plugins.Where(predicate), pluginName, operation, parameters);

            return reflectionEngine.Execute<TResult>(plugins, pluginName, operation, parameters);
        }

        public TResult ExecuteFirst<TResult>(string operation, object parameters, Func<PluginContainer, bool> predicate)
        {
            if (predicate != null)
                return reflectionEngine.ExecuteFirst<TResult>(plugins.Where(predicate), operation, parameters);

            return reflectionEngine.ExecuteFirst<TResult>(plugins, operation, parameters);
        }

        public void ExecuteAll(string operation, object parameters, Func<PluginContainer, bool> predicate)
        {
            if (predicate != null)
                reflectionEngine.ExecuteAll(plugins.Where(predicate), operation, parameters);
            else
                reflectionEngine.ExecuteAll(plugins, operation, parameters);
        }

        public T GetMethod<T>(string operation, Func<PluginContainer, bool> predicate) where T : class
        {
            if (predicate != null)
                return reflectionEngine.GetMethod<T>(plugins.Where(predicate), operation);

            return reflectionEngine.GetMethod<T>(plugins, operation);
        }

        public T Process<T>(string operation, T input, Func<PluginContainer, bool> predicate) where T : class
        {
            if (predicate == null)
                return reflectionEngine.Process(plugins, operation, input);

            return reflectionEngine.Process(plugins.Where(predicate), operation, input);
        }

        public bool AllTrue(string operation, object parameters, Func<PluginContainer, bool> predicate)
        {
            if (predicate != null)
                return reflectionEngine.AllTrue(plugins.Where(predicate), operation, parameters);

            return reflectionEngine.AllTrue(plugins, operation, parameters);
        }

        public bool AnyTrue(string operation, object parameters, Func<PluginContainer, bool> predicate)
        {
            if (predicate != null)
                return reflectionEngine.AnyTrue(plugins.Where(predicate), operation, parameters);
            
            return reflectionEngine.AnyTrue(plugins, operation, parameters);
        }

        #region Private Methods

        private List<PluginAssemblyContainer> loadDynamicAssemblies(string virtualPath)
        {
            string filePath = HttpContext.Current.Server.MapPath(virtualPath);
            string baseFolder = filePath.Substring(0, filePath.LastIndexOf("\\"));
            string currentFolder = filePath;
            List<PluginAssemblyContainer> pluginAssemblies = new List<PluginAssemblyContainer>();

            loadDynamicAssemblies(pluginAssemblies, baseFolder, currentFolder);

            return pluginAssemblies;
        }

        private void loadDynamicAssemblies(List<PluginAssemblyContainer> pluginAssemblies, string baseFolder, string currentFolder)
        {
            foreach (string fileName in Directory.GetFiles(currentFolder).Where(f => SupportedFileExtensions.Contains(Path.GetExtension(f))))
                pluginAssemblies.Add(compileAssembly("~" + fileName.Replace(baseFolder, "").Replace("\\", "/"), null));

            foreach (string directoryName in Directory.GetDirectories(currentFolder))
                loadDynamicAssemblies(pluginAssemblies, baseFolder, directoryName);
        }

        private PluginAssemblyContainer compileAssembly(string virtualPath, PluginAssemblyContainer previousPluginAssemblyContainer)
        {
            if (previousPluginAssemblyContainer != null)
            {
                PluginAssemblyContainer pluginAssemblyContainer = compileAssembly(virtualPath);
                PluginTimeout timeout = new PluginTimeout(TimeSpan.FromSeconds(5));

                while (previousPluginAssemblyContainer.Equals(pluginAssemblyContainer))
                {
                    if (timeout.Expired)
                        return new PluginAssemblyContainer(virtualPath, new TimeoutException(string.Format("Timeout expired while attempting to recompile '{0}'", virtualPath)));

                    Thread.Sleep(10);

                    pluginAssemblyContainer = compileAssembly(virtualPath);
                }

                return pluginAssemblyContainer;
            }

            return compileAssembly(virtualPath);
        }

        private PluginAssemblyContainer compileAssembly(string virtualPath)
        {
            try
            {
                return new PluginAssemblyContainer(virtualPath, BuildManager.GetCompiledAssembly(virtualPath));
            }
            catch (Exception ex)
            {
                return new PluginAssemblyContainer(virtualPath, ex);
            }
        }

        private IEnumerable<PluginContainer> loadPlugins()
        {
            List<PluginContainer> plugins = new List<PluginContainer>();

            foreach (PluginAssemblyContainer pa in pluginAssemblies)
            {
                IEnumerable<PluginContainer> foundPlugins;

                if (pa.IsLoaded)
                    foundPlugins = loadPlugins(pa);
                else
                    foundPlugins = new [] { new PluginContainer(pa, pa.CompilationException) };

                if (foundPlugins.Count() > 0)
                    plugins.AddRange(foundPlugins);
            }

            return plugins;
        }

        private IEnumerable<PluginContainer> loadPlugins(PluginAssemblyContainer assemblyLoadInfo)
        {
            if (assemblyLoadInfo.IsLoaded)
            {
                List<PluginContainer> plugins = new List<PluginContainer>();

                foreach (Type type in assemblyLoadInfo.CompilationAssembly.GetTypes().Where(t => t.Name.EndsWith("Plugin")))
                {
                    object instance = null;
                    Exception pluginLoadException = null;

                    try
                    {
                        if (assemblyLoadInfo.IsCodeFileValid())
                            instance = Activator.CreateInstance(type);
                    }
                    catch (Exception ex)
                    {
                        pluginLoadException = ex;
                    }

                    if (pluginLoadException == null)
                    {
                        PluginContainer pluginContainer = new PluginContainer(assemblyLoadInfo, instance);

                        plugins.Add(pluginContainer);

                        if (AutoInitializePlugins)
                            pluginContainer.Initialize();
                    }
                    else
                        plugins.Add(new PluginContainer(assemblyLoadInfo, pluginLoadException));
                }

                return plugins;
            }

            return new [] { new PluginContainer(assemblyLoadInfo, assemblyLoadInfo.CompilationException) };
        }

        #endregion
    }
}
