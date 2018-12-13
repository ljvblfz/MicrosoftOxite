//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;

namespace Oxite.Plugins
{
    public class PluginScriptRegistry : List<PluginScript>
    {
        //TODO: (erikpo) Need to add a method to obtain a lock so multiple methods can be called while the items are locked

        public void Add(Plugin plugin, string url)
        {
            Add(plugin, url, c => true);
        }

        public void Add(Plugin plugin, string url, string pageName)
        {
            Add(plugin, url, c => string.Compare(c.PageName, pageName, true) == 0);
        }

        public void Add(Plugin plugin, string url, Func<PluginScriptContext, bool> forCurrentRequest)
        {
            lock (this)
            {
                this.Add(new PluginScript(plugin, url, forCurrentRequest));
            }
        }

        public void UpdatePlugin(Plugin plugin)
        {
            lock (this)
            {
                this.Where(s => s.Plugin.ID == plugin.ID).ToList().ForEach(s => s.Plugin = plugin);
            }
        }
    }
}
