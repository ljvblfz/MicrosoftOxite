//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Microsoft.Practices.Unity;
using Oxite.Configuration;

namespace Oxite.Infrastructure
{
    public class ModulesLoaded : IModulesLoaded
    {
        private readonly IUnityContainer container;
        private readonly List<IOxiteModule> modules;

        public ModulesLoaded(IUnityContainer container)
        {
            this.container = container;
            modules = new List<IOxiteModule>(10);
        }

        #region IModulesLoaded Members

        public IOxiteModule Load(OxiteConfigurationSection config, OxiteModuleConfigurationElement module)
        {
            if (module == null || !module.Enabled) return null;

            foreach (OxiteDataProviderConfigurationElement dataProvider in config.Providers)
            {
                if (dataProvider.Name == module.DataProvider)
                {
                    Type dataProviderType = Type.GetType(dataProvider.Type);

                    if (dataProviderType == null)
                        throw new TypeLoadException(string.Format("Could not load type '{0}'.", dataProvider.Type));

                    IOxiteDataProvider dataProviderInstance = container.Resolve(dataProviderType) as IOxiteDataProvider;

                    if (dataProviderInstance != null)
                        dataProviderInstance.ConfigureProvider(config, dataProvider, container);

                    break;
                }
            }

            Type type = Type.GetType(module.Type);

            if (type == null)
                foreach (var assembly in BuildManager.CodeAssemblies)
                {
                    type = ((Assembly) assembly).GetExportedTypes().FirstOrDefault(t => t.FullName == module.Type);

                    if (type != null) break;
                }

            if (type == null)
                throw new TypeLoadException(string.Format("Could not load type '{0}'.", module.Type));

            IOxiteModule moduleInstance = container.Resolve(type) as IOxiteModule;

            modules.Add(moduleInstance);

            return moduleInstance;
        }

        public IEnumerable<IOxiteModule> GetModules()
        {
            return GetModules<IOxiteModule>();
        }

        public IEnumerable<T> GetModules<T>() where T : IOxiteModule
        {
            return modules.Where(m => m is T).Cast<T>();
        }

        public void UnloadModules()
        {
            foreach (IOxiteModule module in modules)
                module.Unload();

            modules.Clear();
        }

        #endregion
    }
}
