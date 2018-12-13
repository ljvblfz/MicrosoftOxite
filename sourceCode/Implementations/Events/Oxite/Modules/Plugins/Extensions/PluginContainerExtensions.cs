//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Models;
using Oxite.Plugins;
using Oxite.Plugins.Extensions;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class PluginContainerExtensions
    {
        public static PluginContainer FillContainer(this PluginContainer pluginContainer, IPluginEngine pluginEngine)
        {
            Plugin plugin = pluginContainer.Tag as Plugin;

            if (plugin != null)
                plugin.Container = pluginEngine.GetPlugin(plugin);

            return pluginContainer;
        }

        public static IEnumerable<PluginContainer> FillContainer(this IEnumerable<PluginContainer> pluginContainers, IPluginEngine pluginEngine)
        {
            foreach (PluginContainer pluginContainer in pluginContainers)
            {
                Plugin plugin = pluginContainer.Tag as Plugin;

                if (plugin != null)
                    plugin.Container = pluginEngine.GetPlugin(plugin);
            }

            return pluginContainers;
        }

        public static void RegisterTemplates(this PluginContainer pluginContainer, PluginTemplateRegistry registry)
        {
            TemplateList templates = new TemplateList();

            pluginContainer.ExecuteMethod("RegisterTemplates", templates);

            templates.ForEach(t => registry.Add(pluginContainer.Tag as Plugin, t.TemplateName, t.Selector, (PluginTemplateSelectorType)(int)t.SelectorType, ptc => t.PageCondition(new TemplateContext(ptc)), t.ModelTarget));
        }

        public static string GetRouteName(this PluginContainer pluginContainer, string methodName)
        {
            string className = pluginContainer.GetClassName();

            if (className.EndsWith("Plugin"))
                className = className.Remove(className.Length - 6);

            return string.Format("Oxite.Plugins.{0}.{1}", className, methodName);
        }

        public static void RegisterRoutes(this PluginContainer pluginContainer, RouteCollection routes)
        {
            RouteList pluginRoutes = new RouteList();

            pluginContainer.ExecuteMethod("RegisterRoutes", pluginRoutes);

            pluginRoutes.ForEach(r => routes.Add(pluginContainer.GetRouteName(r.MethodName), new PluginRoute(((Plugin)pluginContainer.Tag).ID, r.MethodName, r.Url, r.MergeWithDefaults(new { controller = "Plugin", action = "CallMethod" }), r.MergeWithConstraints(null))));
        }

        public static void RegisterScripts(this PluginContainer pluginContainer, PluginScriptRegistry registry)
        {
            ScriptList scripts = new ScriptList();

            pluginContainer.ExecuteMethod("RegisterScripts", scripts);

            scripts.ForEach(s => registry.Add(pluginContainer.Tag as Plugin, s.Url, psc => s.PageCondition(new ScriptContext(psc))));
        }

        public static void RegisterStyles(this PluginContainer pluginContainer, PluginStyleRegistry registry)
        {
            StyleList styles = new StyleList();

            pluginContainer.ExecuteMethod("RegisterStyles", styles);

            styles.ForEach(s => registry.Add(pluginContainer.Tag as Plugin, s.Url, psc => s.PageCondition(new StyleContext(psc))));
        }
    }
}
