// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Plugins.Extensions
{
    public static class PluginEngineExtensions
    {
        public static PluginContainer GetPlugin(this IPluginEngine pluginEngine, Plugin plugin)
        {
            PluginContainer pluginContainer = null;

            if (plugin.ID != Guid.Empty || !string.IsNullOrEmpty(plugin.VirtualPath))
                pluginContainer = pluginEngine.GetPlugins(p => (p.Tag != null && p.Tag is Plugin && ((Plugin)p.Tag).ID == plugin.ID) || string.Compare(p.VirtualPath, plugin.VirtualPath, true) == 0).FirstOrDefault();

            return pluginContainer;
        }

        public static IEnumerable<PluginContainer> GetInstalledAndEnabledPlugins(this IPluginEngine pluginEngine)
        {
            return pluginEngine.GetPlugins(getInstalledAndEnabled(pluginEngine));
        }

        public static T GetMethod<T>(this IPluginEngine pluginEngine, string operation) where T : class
        {
            return pluginEngine.GetMethod<T>(operation, getInstalledAndEnabled(pluginEngine));
        }

        public static TResult Execute<TResult>(this IPluginEngine pluginEngine, string pluginName, string operation, params object[] input)
        {
            return pluginEngine.Execute<TResult>(pluginName, operation, getInstalledAndEnabled(pluginEngine), input);
        }

        public static TResult ExecuteFirst<TResult>(this IPluginEngine pluginEngine, string operation, params object[] input)
        {
            return pluginEngine.ExecuteFirst<TResult>(operation, getInstalledAndEnabled(pluginEngine), input);
        }

        public static void ExecuteAll(this IPluginEngine pluginEngine, string operation, object parameters)
        {
            pluginEngine.ExecuteAll(operation, parameters, getInstalledAndEnabled(pluginEngine));
        }

        public static T Process<T>(this IPluginEngine pluginEngine, string operation, T input) where T : class
        {
            return pluginEngine.Process<T>(operation, input, getInstalledAndEnabled(pluginEngine));
        }

        public static bool AllTrue(this IPluginEngine pluginEngine, string operation, object parameters)
        {
            return pluginEngine.AllTrue(operation, parameters, getInstalledAndEnabled(pluginEngine));
        }

        public static bool AnyTrue(this IPluginEngine pluginEngine, string operation, object parameters)
        {
            return pluginEngine.AnyTrue(operation, parameters, getInstalledAndEnabled(pluginEngine));
        }

        private static Func<PluginContainer, bool> GetNotInstalled(IPluginEngine pluginEngine)
        {
            return p => p.Tag == null || !(p.Tag is Plugin);
        }

        private static Func<PluginContainer, bool> GetInstalled(IPluginEngine pluginEngine)
        {
            return p => p.Tag != null && p.Tag is Plugin;
        }

        private static Func<PluginContainer, bool> getInstalledAndEnabled(IPluginEngine pluginEngine)
        {
            return p => p.IsLoaded && p.Tag != null && p.Tag is Plugin && ((Plugin)p.Tag).Enabled;
        }
    }
}
