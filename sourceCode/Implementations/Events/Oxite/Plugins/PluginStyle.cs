//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Plugins
{
    public class PluginStyle
    {
        internal PluginStyle(Plugin plugin, string url, Func<PluginStyleContext, bool> forCurrentRequest)
        {
            Plugin = plugin;
            Url = url;
            ForCurrentRequest = forCurrentRequest;
        }

        public Plugin Plugin { get; set; }
        public string Url { get; private set; }
        public Func<PluginStyleContext, bool> ForCurrentRequest { get; private set; }
    }
}
