//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Setup
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using Microsoft.Practices.Unity;
    using Oxite.Infrastructure;
    using Oxite.Modules.Setup.ModelBinders;
    using Oxite.Modules.Setup.Services;
    using Oxite.Modules.Setup.Models;

    /// <summary>
    /// Module to encapsulate workflow for setup of a new Oxite site.
    /// </summary>
    public class SetupModule : IOxiteModule
    {
        #region Fields
        private readonly IUnityContainer container;
        #endregion

        #region Constructor
        public SetupModule(IUnityContainer container)
        {
            this.container = container;
        }
        #endregion

        #region IOxiteModule Members

        public void Initialize()
        {
        }

        public void Unload()
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            string[] controllerNamespaces = new string[] { "Oxite.Modules.Core.Controllers" };

            // Admin
            routes.MapRoute(
                "AdminSettings",
                "Admin/Setup/AdminSettings/{siteId}",
                new { controller = "Setup", action = "AdminSettings", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "BasicSettings",
                "Admin/Setup/BasicSettings/{siteId}",
                new { controller = "Setup", action = "BasicSettings", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "Modules",
                "Admin/Setup/Modules/{siteId}",
                new { controller = "Setup", action = "Modules", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "SetupCompete",
                "Admin/Setup/SetupComplete/{siteId}",
                new { controller = "Setup", action = "SetupComplete", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "Storage",
                "Admin/Setup/Storage/{siteId}",
                new { controller = "Setup", action = "Storage", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            routes.MapRoute(
                "SiteType",
                "Admin/Setup/{siteId}",
                new { controller = "Setup", action = "SiteType", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(SetupInput)] = new SetupInputModelBinder();
        }

        public void RegisterWithContainer()
        {
            container.RegisterType<ISetupService, SetupService>();
        }

        #endregion
    }
}
