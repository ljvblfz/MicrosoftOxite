//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Services;
using Oxite.Plugins;
using Oxite.Plugins.Extensions;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class RouteCollectionExtensions
    {
        public static void Reload(this RouteCollection routes, IModulesLoaded modulesLoaded, IPluginService pluginService, IPluginEngine pluginEngine)
        {
            routes.Clear();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LoadFromModules(modulesLoaded);
            routes.LoadFromPlugins(pluginService, pluginEngine);
            routes.LoadCatchAllFromModules(modulesLoaded);
        }

        public static void LoadFromPlugins(this RouteCollection routes, IPluginService pluginService, IPluginEngine pluginEngine)
        {
            foreach (Plugin plugin in pluginService.GetPlugins())
            {
                if (plugin.Enabled)
                {
                    PluginContainer pluginContainer = pluginEngine.GetPlugin(plugin);

                    if (pluginContainer != null && pluginContainer.IsLoaded)
                        pluginContainer.RegisterRoutes(routes);
                }
            }
        }
    }
}
