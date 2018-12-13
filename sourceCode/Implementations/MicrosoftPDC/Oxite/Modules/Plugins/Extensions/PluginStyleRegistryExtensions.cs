//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Plugins;
using Oxite.Plugins.Extensions;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class PluginStyleRegistryExtensions
    {
        public static void Reload(this PluginStyleRegistry registry, IPluginEngine pluginEngine)
        {
            registry.Clear();

            foreach (PluginContainer pluginContainer in pluginEngine.GetInstalledAndEnabledPlugins())
                pluginContainer.RegisterStyles(registry);
        }
    }
}
