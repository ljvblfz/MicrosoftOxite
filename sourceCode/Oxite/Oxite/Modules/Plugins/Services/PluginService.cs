//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Transactions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Extensions;
using Oxite.Modules.Plugins.Models;
using Oxite.Modules.Plugins.Repositories;
using Oxite.Plugins;
using Oxite.Plugins.Extensions;
using Oxite.Validation;

namespace Oxite.Modules.Plugins.Services
{
    public class PluginService : IPluginService
    {
        private readonly IPluginRepository repository;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly IOxiteCacheModule cache;
        private readonly IModulesLoaded modules;
        private readonly PluginTemplateRegistry pluginTemplateRegistry;
        private readonly PluginStyleRegistry pluginStyleRegistry;
        private readonly PluginScriptRegistry pluginScriptRegistry;
        private readonly OxiteContext context;

        public PluginService(IPluginRepository repository, IValidationService validator, IPluginEngine pluginEngine, IModulesLoaded modules, PluginTemplateRegistry pluginTemplateRegistry, PluginStyleRegistry pluginStyleRegistry, PluginScriptRegistry pluginScriptRegistry, OxiteContext context)
        {
            this.repository = repository;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.modules = modules;
            this.pluginTemplateRegistry = pluginTemplateRegistry;
            this.pluginStyleRegistry = pluginStyleRegistry;
            this.pluginScriptRegistry = pluginScriptRegistry;
            this.context = context;
        }

        #region IPluginService Members

        public IEnumerable<Plugin> GetPlugins()
        {
            return repository.GetPlugins();
        }

        public Plugin GetPlugin(Guid pluginID)
        {
            return repository.GetPlugin(pluginID);
        }

        public Plugin InstallPlugin(PluginInstallInput pluginInstallInput, bool? overrideEnabled)
        {
            PluginContainer pluginContainer = null;

            if (!string.IsNullOrEmpty(pluginInstallInput.VirtualPath))
                pluginContainer = pluginEngine.GetPlugins(p => (p.Tag == null || !(p.Tag is Plugin)) && p.CompilationAssembly != null && string.Compare(p.VirtualPath, pluginInstallInput.VirtualPath, true) == 0).FirstOrDefault();

            if (pluginContainer == null) throw new InvalidOperationException("Could not find plugin to install");

            ValidationStateDictionary validationState = ValidatePlugin(pluginContainer);
            Plugin plugin = repository.Save(new Plugin(context.Site.ID, pluginInstallInput.VirtualPath, validationState.IsValid && overrideEnabled.HasValue ? overrideEnabled.Value : validationState.IsValid, pluginContainer.GetPropertiesUsingDefaultValues()));

            plugin.Container = pluginContainer;
            pluginContainer.Tag = plugin;
            pluginContainer.IsValid = validationState.IsValid;

            pluginTemplateRegistry.Reload(pluginEngine);
            context.Routes.Reload(modules, this, pluginEngine);
            pluginStyleRegistry.Reload(pluginEngine);
            pluginScriptRegistry.Reload(pluginEngine);

            return plugin;
        }

        public ValidationStateDictionary ValidatePlugin(PluginContainer pluginContainer)
        {
            IEnumerable<ExtendedProperty> extendedProperties = pluginContainer.GetPropertiesUsingDefaultValues();

            return validatePluginPropertyValues(new PluginPropertiesInput(pluginContainer.Definitions, extendedProperties, name => pluginContainer.GetPropertyDefinitions(name), name => extendedProperties.First(ep => string.Compare(ep.Name, name, true) == 0).Value));
        }

        private ValidationStateDictionary validatePluginPropertyValues(Plugin plugin, NameValueCollection newPropertyValues)
        {
            return validatePluginPropertyValues(new PluginPropertiesInput(plugin, newPropertyValues));
        }

        private ValidationStateDictionary validatePluginPropertyValues(PluginPropertiesInput input)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(PluginPropertiesInput), validator.Validate(input));

            return validationState;
        }

        public ModelResult<Plugin> EditPlugin(Plugin plugin, PluginEditInput pluginEditInput, bool hasCodeChanges)
        {
            Plugin originalPlugin = plugin;
            bool enabled = originalPlugin.Enabled;
            bool newlyEnabled = false;
            Plugin newPlugin;
            PluginContainer pluginContainer;
            ValidationStateDictionary validationState;

            if (!hasCodeChanges)
            {
                validationState = validatePluginPropertyValues(originalPlugin.FillContainer(pluginEngine), pluginEditInput.PropertyValues);

                pluginContainer = pluginEngine.GetPlugin(originalPlugin);
                pluginContainer.IsValid = validationState.IsValid;

                newlyEnabled = !enabled && validationState.IsValid;

                newPlugin = originalPlugin.Apply(pluginEditInput, newlyEnabled ? true : (bool?)null);

                if (!validationState.IsValid) return new ModelResult<Plugin>(newPlugin, validationState);
            }
            else
            {
                validationState = new ValidationStateDictionary();

                originalPlugin.SaveFileText(pluginEditInput.Code);

                pluginContainer = pluginEngine.ReloadPlugin(p => p.Tag != null && p.Tag is Plugin && ((Plugin)p.Tag).ID == originalPlugin.ID, p => originalPlugin.VirtualPath);

                if (pluginContainer.IsLoaded)
                    newPlugin = originalPlugin.ApplyNew(pluginEditInput, pluginContainer.GetProperties());
                else
                    newPlugin = originalPlugin.Apply(pluginEditInput, null);
            }

            newPlugin = repository.Save(newPlugin);

            newPlugin.Container = pluginContainer;
            pluginContainer.Tag = newPlugin;

            if (!hasCodeChanges)
            {
                pluginContainer.ApplyProperties(newPlugin.ExtendedProperties);

                if (newlyEnabled)
                {
                    pluginTemplateRegistry.Reload(pluginEngine);
                    context.Routes.Reload(modules, this, pluginEngine);
                    pluginStyleRegistry.Reload(pluginEngine);
                    pluginScriptRegistry.Reload(pluginEngine);
                }
                else
                {
                    pluginTemplateRegistry.UpdatePlugin(newPlugin);
                    pluginStyleRegistry.UpdatePlugin(newPlugin);
                    pluginScriptRegistry.UpdatePlugin(newPlugin);
                }
            }
            else
            {
                //TODO: (erikpo) Instead fire off a module event saying a plugin was edited and then Oxite.Blogs, Oxite.CMS and Oxite.Comments can subscribe to those events and invalidate cache by their type on their own.
                //if (pluginContainer.HasMethod("ProcessDisplayOfPost"))
                //    cache.Invalidate<Post>();
                //else if (pluginContainer.HasMethod("ProcessDisplayOfPage"))
                //    cache.Invalidate<Page>();
                //else if (pluginContainer.HasMethod("ProcessDisplayOfComment"))
                //    cache.Invalidate<Comment>();

                pluginTemplateRegistry.Reload(pluginEngine);
                context.Routes.Reload(modules, this, pluginEngine);
                pluginStyleRegistry.Reload(pluginEngine);
                pluginScriptRegistry.Reload(pluginEngine);
            }

            return new ModelResult<Plugin>(newPlugin, validationState);
        }

        public void SetPluginEnabled(Plugin plugin, bool enabled)
        {
            if (plugin == null || plugin.Enabled == enabled) return;

            using (TransactionScope transaction = new TransactionScope())
            {
                repository.SetEnabled(plugin, enabled);

                PluginContainer pluginContainer = pluginEngine.GetPlugins(p => p.Tag != null && p.Tag is Plugin && ((Plugin)p.Tag).ID == plugin.ID).FirstOrDefault();

                plugin.Container = pluginContainer;
                pluginContainer.Tag = plugin;

                pluginTemplateRegistry.Reload(pluginEngine);
                context.Routes.Reload(modules, this, pluginEngine);
                pluginStyleRegistry.Reload(pluginEngine);
                pluginScriptRegistry.Reload(pluginEngine);

                transaction.Complete();
            }
        }

        public void UninstallPlugin(Plugin plugin)
        {
            if (plugin == null) return;

            using (TransactionScope transaction = new TransactionScope())
            {
                repository.Remove(plugin);

                PluginContainer pluginContainer = pluginEngine.GetPlugins(p => p.Tag != null && p.Tag is Plugin && ((Plugin)p.Tag).ID == plugin.ID).FirstOrDefault();

                if (!System.IO.File.Exists(context.HttpContext.Server.MapPath(pluginContainer.VirtualPath)))
                    pluginEngine.RemovePlugin(pluginContainer);

                plugin.Container = null;
                pluginContainer.Tag = null;

                pluginTemplateRegistry.Reload(pluginEngine);
                context.Routes.Reload(modules, this, pluginEngine);
                pluginStyleRegistry.Reload(pluginEngine);
                pluginScriptRegistry.Reload(pluginEngine);

                transaction.Complete();
            }
        }

        public void ReloadPlugin(Plugin plugin)
        {
            if (plugin == null) return;

            PluginContainer pluginContainer = pluginEngine.ReloadPlugin(p => p.Tag != null && p.Tag is Plugin && ((Plugin)p.Tag).ID == plugin.ID, p => plugin.VirtualPath);

            if (pluginContainer.IsLoaded)
            {
                plugin = repository.Save(plugin.ApplyProperties(pluginContainer.GetPropertiesUsingDefaultValues()));

                pluginContainer.ApplyProperties(plugin.ExtendedProperties);
            }

            plugin.Container = pluginContainer;
            pluginContainer.Tag = plugin;

            pluginTemplateRegistry.Reload(pluginEngine);
            context.Routes.Reload(modules, this, pluginEngine);
            pluginStyleRegistry.Reload(pluginEngine);
            pluginScriptRegistry.Reload(pluginEngine);
        }

        public void ReorderPluginOperation(Plugin plugin, string operationType, string operation, int newSequenceNumber)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
