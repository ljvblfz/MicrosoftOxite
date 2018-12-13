//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Routing
{
    public static class OxiteRouteListExtensions
    {
        public static void AddRoute(this IList<OxiteRoute> routes, IRouteModifier routeModifier, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            routes.Add(new OxiteRoute(name, routeModifier.ModifyUrl(url), defaults, constraints, namespaces));
        }

        public static System.Web.Routing.Route MapRoute(this System.Web.Routing.RouteCollection routes, string name, string url, System.Web.Routing.RouteValueDictionary defaults, object constraints, string[] namespaces)
        {
            System.Web.Routing.Route route = new System.Web.Routing.Route(url, new System.Web.Mvc.MvcRouteHandler())
            {
                Defaults = defaults,
                Constraints = new System.Web.Routing.RouteValueDictionary(constraints)
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens = new System.Web.Routing.RouteValueDictionary();
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }
    }
}
