//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Infrastructure;
using Oxite.Routing;

namespace Oxite.BootStrappers
{
    public class RegisterRoutes : IBootStrapperTask
    {
        private IUnityContainer container;

        public RegisterRoutes(IUnityContainer container)
        {
            this.container = container;
        }

        #region IBootStrapperTask Members

        public void Execute(IDictionary<string, object> state)
        {
            RouteCollection routes = container.Resolve<RouteCollection>();
            List<OxiteRoute> tempRoutes = new List<OxiteRoute>(50);

            foreach (IRegisterRoutes routeRegistry in container.ResolveAll<IRegisterRoutes>().Reverse())
                routeRegistry.RegisterRoutes(tempRoutes);

            container.Resolve<IRegisterRoutes>().RegisterRoutes(tempRoutes);

            routes.Clear();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //TODO: (erikpo) Check a ViewTrackingEnabled Site setting to add these or not
            foreach (OxiteRoute route in tempRoutes)
            {
                object viewTrackingValue = new RouteValueDictionary(route.Defaults)["viewTracking"];
                bool viewTracking = viewTrackingValue != null && viewTrackingValue is bool ? (bool)viewTrackingValue : true;

                if (viewTracking && !route.Url.StartsWith("Admin", System.StringComparison.OrdinalIgnoreCase) && !route.Url.Contains("{*"))
                {
                    string name = "ViewTracking-" + route.Name;
                    string url = (route.Url + "/_View_").TrimStart('/');
                    RouteValueDictionary defaults = route.Defaults != null ? new RouteValueDictionary(route.Defaults) : new RouteValueDictionary();

                    defaults["controller"] = "ViewTracking";
                    defaults["action"] = "Add";
                    defaults["viewTracking"] = false;

                    routes.MapRoute(name, url, defaults, route.Constraints, route.ControllerNamespaces);
                }
            }

            foreach (OxiteRoute route in tempRoutes)
                routes.MapRoute(route.Name, route.Url, route.Defaults, route.Constraints, route.ControllerNamespaces);
        }

        public void Cleanup(IDictionary<string, object> state)
        {
        }

        #endregion
    }
}
