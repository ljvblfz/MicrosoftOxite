//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Plugins;

namespace Oxite.Infrastructure
{
    public interface IPluginEngine
    {
        bool AutoInitializePlugins { get; set; }
        string[] SupportedFileExtensions { get; set; }
        void LoadAssembliesFromCodeFiles(string virtualPath);
        IEnumerable<PluginContainer> GetPlugins();
        IEnumerable<PluginContainer> GetPlugins(Func<PluginContainer, bool> predicate);
        PluginContainer LoadPlugin(string virtualPath);
        void LoadPlugins();
        void ReloadPlugins(string virtualPath);
        void ReloadPlugins(string virtualPath, Func<PluginContainer, bool> getPluginsToReload);
        PluginContainer ReloadPlugin(Func<PluginContainer, bool> findPlugin, Func<PluginContainer, string> getVirtualPath);
        void AddPlugin(PluginContainer pluginContainer);
        void RemovePlugin(PluginContainer pluginContainer);

        T GetMethod<T>(string operation, Func<PluginContainer, bool> predicate) where T : class;
        TResult Execute<TResult>(string pluginName, string operation, object parameters, Func<PluginContainer, bool> predicate);
        TResult ExecuteFirst<TResult>(string operation, object parameters, Func<PluginContainer, bool> predicate);
        void ExecuteAll(string operation, object parameters, Func<PluginContainer, bool> predicate);
        bool AnyTrue(string operation, object parameters, Func<PluginContainer, bool> predicate);
        bool AllTrue(string operation, object parameters, Func<PluginContainer, bool> predicate);
        T Process<T>(string operation, T input, Func<PluginContainer, bool> predicate) where T : class;
    }
}
