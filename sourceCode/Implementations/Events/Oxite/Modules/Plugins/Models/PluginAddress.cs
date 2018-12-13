//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Plugins.Models
{
    public class PluginAddress
    {
        public PluginAddress(Guid pluginID)
        {
            PluginID = pluginID;
        }

        public Guid PluginID { get; private set; }
    }
}
