// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Plugins.Extensions;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Plugins(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Plugins");
        }

        public static string PluginsInstalled(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PluginsInstalled");
        }

        public static string PluginsNotInstalled(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PluginsNotInstalled");
        }

        public static string PluginsNotInstalledRefresh(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PluginsNotInstalledRefresh");
        }

        public static string Plugin(this UrlHelper urlHelper, Plugin plugin)
        {
            return urlHelper.Plugins();
        }

        public static string PluginInstall(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PluginInstall");
        }

        public static string PluginUninstall(this UrlHelper urlHelper, Plugin plugin)
        {
            return urlHelper.RouteUrl("PluginUninstall", new { pluginID = plugin.ID });
        }

        public static string PluginEnable(this UrlHelper urlHelper, Plugin plugin)
        {
            return urlHelper.RouteUrl("PluginEnable", new { pluginID = plugin.ID });
        }

        public static string PluginDisable(this UrlHelper urlHelper, Plugin plugin)
        {
            return urlHelper.RouteUrl("PluginDisable", new { pluginID = plugin.ID });
        }

        public static string PluginEdit(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PluginEditNotInstalled");
        }

        public static string PluginEdit(this UrlHelper urlHelper, Plugin plugin)
        {
            return plugin != null
                ? urlHelper.PluginEdit(plugin.ID)
                : urlHelper.PluginEdit();
        }

        public static string PluginEdit(this UrlHelper urlHelper, Guid pluginID)
        {
            return urlHelper.RouteUrl("PluginEdit", new { pluginID });
        }

        public static string PluginReload(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PluginReloadNotInstalled");
        }

        public static string PluginReload(this UrlHelper urlHelper, Plugin plugin)
        {
            return plugin != null
                ? urlHelper.RouteUrl("PluginReload", new { pluginID = plugin.ID })
                : urlHelper.PluginReload();
        }

        public static string PluginRouteUrl(this UrlHelper urlHelper, Plugin plugin, string methodName)
        {
            return urlHelper.PluginRouteUrl(plugin, methodName, null);
        }

        public static string PluginRouteUrl(this UrlHelper urlHelper, Plugin plugin, string methodName, object routeValues)
        {
            return urlHelper.RouteUrl(plugin.GetRouteName(methodName), routeValues);
        }

        public static string PluginTemplatesPath(this UrlHelper urlHelper, Plugin plugin, string path)
        {
            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
                path = "/" + path;

            return urlHelper.AppPath(plugin.Container.GetTemplatesPath() + path);
        }

        public static string PluginScriptsPath(this UrlHelper urlHelper, Plugin plugin, string path)
        {
            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
                path = "/" + path;

            return urlHelper.AppPath(plugin.Container.GetScriptsPath() + path);
        }

        public static string PluginStylesPath(this UrlHelper urlHelper, Plugin plugin, string path)
        {
            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
                path = "/" + path;

            return urlHelper.AppPath(plugin.Container.GetStylesPath() + path);
        }

        public static string PluginImagesPath(this UrlHelper urlHelper, Plugin plugin, string path)
        {
            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
                path = "/" + path;

            return urlHelper.AppPath(plugin.Container.GetImagesPath() + path);
        }
    }
}
