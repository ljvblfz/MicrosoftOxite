//---------------------------------------------------------------------
// <copyright file="LoadModules.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
using Oxite.Configuration;

namespace Oxite.BootStrapperTasks
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;
    using Oxite.Extensions;
    using Oxite.Infrastructure;
    using Oxite.Models;
    using Oxite.Services;

    public class LoadModules : IBootStrapperTask
    {
        private readonly IUnityContainer container;

        /// <summary>
        /// Initializes a new instance of the LoadModules class.
        /// </summary>
        /// <param name="container">IUnityContainer to use when resolving needed objects.</param>
        public LoadModules(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            OxiteConfigurationSection config = container.Resolve<OxiteConfigurationSection>();
            IModulesLoaded modulesLoaded = this.container.Resolve<IModulesLoaded>();
            RouteCollection routes = this.container.Resolve<RouteCollection>();
            IFilterRegistry filterRegistry = this.container.Resolve<FilterRegistry>();
            ModelBinderDictionary modelBinders = this.container.Resolve<ModelBinderDictionary>();

            filterRegistry.Clear();

            modelBinders.Clear();

            //todo: (nheskew) get plugin routes registered on load in the right order instead of just clearing the routes before module init
            routes.Clear();

            foreach (OxiteModuleConfigurationElement module in config.Modules)
            {
                IOxiteModule moduleInstance = modulesLoaded.Load(config, module);

                if (moduleInstance != null)
                {
                    moduleInstance.RegisterWithContainer();
                    moduleInstance.Initialize();
                    moduleInstance.RegisterFilters(filterRegistry);
                    moduleInstance.RegisterModelBinders(modelBinders);

                    this.container.RegisterInstance(modulesLoaded);
                }
            }

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LoadFromModules(modulesLoaded);

            routes.LoadCatchAllFromModules(modulesLoaded);

            container.RegisterInstance(filterRegistry);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
            container.Resolve<IModulesLoaded>().UnloadModules();

            //TODO: (erikpo) Loop through all background services running in the background and stop them
        }

        #endregion
    }
}
