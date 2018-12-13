//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Modules.Plugins.Models;
using Oxite.Modules.Plugins.Services;

namespace Oxite.Modules.Plugins.ModelBinders
{
    public class PluginModelBinder : IModelBinder
    {
        private readonly IPluginService pluginService;

        public PluginModelBinder(IPluginService pluginService)
        {
            this.pluginService = pluginService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string pluginID = (string)bindingContext.ValueProvider["pluginID"].RawValue;

            return !string.IsNullOrEmpty(pluginID) ? pluginService.GetPlugin(new Guid(pluginID)) : null;
        }
    }
}
