//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Specialized;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.ModelsBinders
{
    public class PluginModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection form = controllerContext.HttpContext.Request.Form;
            Guid pluginID = Guid.Empty;
            string pluginName;
            string pluginCategory;

            string pluginIDValue = form["pluginID"];
            if (!string.IsNullOrEmpty(pluginIDValue))
                pluginIDValue.GuidTryParse(out pluginID);

            pluginName = form["pluginName"];
            pluginCategory = form["pluginCategory"];

            string[] settingNames = form.GetValues("settingName");
            string[] settingValues = form.GetValues("settingValue");
            NameValueCollection settings = new NameValueCollection();

            if (settingNames != null && settingValues != null && settingNames.Length > 0 && settingValues.Length > 0)
            {
                for (int i = 0; i < settingNames.Length; i++)
                {
                    settings[settingNames[i]] = settingValues[i];
                }
            }

            Plugin plugin = new Plugin(pluginID, pluginCategory, pluginName, settings);

            bool enabled;
            if (form.GetValues("enabled") != null && bool.TryParse(form.GetValues("enabled")[0], out enabled))
            {
                plugin.Enabled = enabled;
            }

            return plugin;
        }
    }
}
