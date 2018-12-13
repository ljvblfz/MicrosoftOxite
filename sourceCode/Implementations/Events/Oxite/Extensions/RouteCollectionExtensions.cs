//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Oxite.Infrastructure;

namespace Oxite.Extensions
{
    public static class RouteCollectionExtensions
    {
        public static void LoadFromModules(this RouteCollection routes, IModulesLoaded modulesLoaded)
        {
            IEnumerable<IOxiteModule> modules = modulesLoaded.GetModules().Reverse();

            foreach (IOxiteModule module in modules)
                module.RegisterRoutes(routes);
        }

        public static void LoadCatchAllFromModules(this RouteCollection routes, IModulesLoaded modulesLoaded)
        {
            IEnumerable<IOxiteModule> modules = modulesLoaded.GetModules().Reverse();

            foreach (IOxiteModule module in modules)
                module.RegisterCatchAllRoutes(routes);
        }
    }
}
