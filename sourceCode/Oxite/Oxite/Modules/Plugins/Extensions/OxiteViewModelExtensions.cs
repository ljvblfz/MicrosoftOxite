//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Plugins.ViewModels;
using Oxite.Plugins;
using Oxite.ViewModels;

namespace Oxite.Modules.Plugins.Extensions
{
    public static class OxiteViewModelExtensions
    {
        public static IEnumerable<PluginTemplate> GetPluginTemplates(this OxiteViewModel model)
        {
            PluginTemplatesViewModel viewModel = model.GetModelItem<PluginTemplatesViewModel>();

            if (viewModel != null)
                return viewModel.PluginTemplates;

            return Enumerable.Empty<PluginTemplate>();
        }
    }
}