//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Plugins;

namespace Oxite.Modules.Plugins.ViewModels
{
    public class PluginTemplatesViewModel
    {
        public PluginTemplatesViewModel(IEnumerable<PluginTemplate> pluginTemplates)
        {
            PluginTemplates = pluginTemplates;
        }

        public IEnumerable<PluginTemplate> PluginTemplates { get; private set; }
    }
}