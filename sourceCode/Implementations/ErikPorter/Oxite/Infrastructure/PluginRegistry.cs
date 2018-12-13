//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public class PluginRegistry : IPluginRegistry
    {
        private List<IPlugin> plugins;

        public PluginRegistry()
        {
            this.plugins = new List<IPlugin>(10);
        }

        #region IPluginRegistry Members

        public void Add(IPlugin plugin)
        {
            plugins.Add(plugin);
        }

        public void Clear()
        {
            plugins.Clear();
        }

        public IList<IPlugin> GetPlugins()
        {
            return plugins;
        }

        #endregion
    }
}
