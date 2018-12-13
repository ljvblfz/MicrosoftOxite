// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Models;
using Oxite.Plugins;
using Oxite.Plugins.Attributes;
using Oxite.Plugins.Extensions;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class PluginExtensions
    {
        public static Plugin FillContainer(this Plugin plugin, IPluginEngine pluginEngine)
        {
            plugin.Container = pluginEngine.GetPlugin(plugin);

            return plugin;
        }

        public static IEnumerable<Plugin> FillContainer(this IEnumerable<Plugin> plugins, IPluginEngine pluginEngine)
        {
            plugins.ToList().ForEach(p => p.FillContainer(pluginEngine));

            return plugins;
        }

        public static string GetRouteName(this Plugin plugin, string methodName)
        {
            return plugin.Container != null ? plugin.Container.GetRouteName(methodName) : null;
        }

        public static string[] GetAuthors(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetAuthors() : new string[0];
        }

        public static string[] GetAuthorUrls(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetAuthorUrls() : new string[0];
        }

        public static string GetBackgroundImage(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetBackgroundImage() : null;
        }

        public static string GetCategory(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetCategory() : null;
        }

        public static string GetClassName(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetClassName() : null;
        }

        public static string GetDescription(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetDescription() : null;
        }

        public static string GetDisplayName(this Plugin plugin)
        {
            return plugin != null && plugin.Container != null ? plugin.Container.GetDisplayName() : null;
        }

        public static string GetIconLarge(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetIconLarge() : null;
        }

        public static string GetIconLargeError(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetIconLargeError() : null;
        }

        public static string GetIconLargeDisabled(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetIconLargeDisabled() : null;
        }

        public static string GetIconSmall(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetIconSmall() : null;
        }

        public static string GetIconSmallError(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetIconSmallError() : null;
        }

        public static string GetIconSmallDisabled(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetIconSmallDisabled() : null;
        }

        public static string[] GetTags(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetTags() : new string[0];
        }

        public static string GetHomePage(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetHomePage() : null;
        }

        public static Version GetVersion(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetVersion() : null;
        }

        public static Version GetOxiteMinVersion(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetOxiteMinVersion() : null;
        }

        public static Version GetOxiteMaxVersion(this Plugin plugin)
        {
            return plugin.Container != null ? plugin.Container.GetOxiteMaxVersion() : null;
        }

        public static IDictionary<string, object> GetPropertyDefinition(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetPropertyDefinitions(propertyName) : new Dictionary<string, object>();
        }

        public static object GetDefaultValue(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetDefaultValue(propertyName) : null;
        }

        public static PropertyGroup GetGroup(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetGroup(propertyName) : null;
        }

        public static string GetHelpText(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetHelpText(propertyName) : null;
        }

        public static string GetHelpUrl(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetHelpUrl(propertyName) : null;
        }

        public static string GetLabelText(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetLabelText(propertyName) : null;
        }

        public static PropertyAppearance GetAppearance(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetAppearance(propertyName) : null;
        }

        public static int GetOrder(this Plugin plugin, string propertyName)
        {
            return plugin.Container != null ? plugin.Container.GetOrder(propertyName) : int.MaxValue;
        }
    }
}
