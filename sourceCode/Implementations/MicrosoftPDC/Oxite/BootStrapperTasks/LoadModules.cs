//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Services;

namespace Oxite.BootStrapperTasks
{
    public class LoadModules : IBootStrapperTask
    {
        private readonly IUnityContainer container;

        public LoadModules(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            IModulesLoaded modulesLoaded = container.Resolve<IModulesLoaded>();
            RouteCollection routes = container.Resolve<RouteCollection>();
            IFilterRegistry filterRegistry = container.Resolve<FilterRegistry>();
            ModelBinderDictionary modelBinders = container.Resolve<ModelBinderDictionary>();

            filterRegistry.Clear();

            modelBinders.Clear();

            //todo: (nheskew) get plugin routes registered on load in the right order instead of just clearing the routes before module init
            routes.Clear();

            foreach (Module module in container.Resolve<IModuleService>().GetModules())
            {
                IOxiteModule moduleInstance = modulesLoaded.Load(module);

                if (moduleInstance != null)
                {
                    moduleInstance.RegisterWithContainer();
                    moduleInstance.Initialize();
                    moduleInstance.RegisterFilters(filterRegistry);
                    moduleInstance.RegisterModelBinders(modelBinders);

                    container.RegisterInstance(modulesLoaded);
                }
            }

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LoadFromModules(modulesLoaded);

            routes.LoadCatchAllFromModules(modulesLoaded);

            container.RegisterInstance(filterRegistry);

            ////TODO: (erikpo) Rename IOxiteModule to IOxiteBackgroundService
            //IModuleRegistry moduleRegistry = container.Resolve<ModuleRegistry>();

            //moduleRegistry.Clear();

            //container.Resolve<IRegisterModules>().RegisterModules(moduleRegistry);

            //foreach (IOxiteModule module in moduleRegistry.GetModules())
            //    module.Start();

            //container.RegisterInstance(moduleRegistry);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
            container.Resolve<IModulesLoaded>().UnloadModules();

            ////TODO: (erikpo) Rename IOxiteModule to IOxiteBackgroundService
            //foreach (IOxiteModule module in container.Resolve<ModuleRegistry>().GetModules())
            //    module.Stop();
        }

        #endregion
    }
}
