//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;
using Oxite.Infrastructure;

namespace Oxite.Controllers
{
    public class PluginController : Controller
    {
        private readonly IPluginService pluginService;

        public PluginController(IPluginService pluginService)
        {
            this.pluginService = pluginService;
        }

        public OxiteModelList<IPlugin> List()
        {
            return new OxiteModelList<IPlugin> { List = pluginService.GetPlugins() };
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteModelList<IPlugin> Install()
        {
            return new OxiteModelList<IPlugin> { List = pluginService.GetPluginsNotInstalled() };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ActionName("Install")]
        public ActionResult InstallSave(Plugin pluginInput)
        {
            pluginService.Save(pluginInput);

            OxiteApplication.Load(ControllerContext.HttpContext);

            return Redirect(Url.Plugins());
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteModelItem<IPlugin> Item(Guid pluginID)
        {
            return new OxiteModelItem<IPlugin> { Item = pluginService.GetPlugin(pluginID) };
        }

        [ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        public object SaveItem(Plugin pluginInput)
        {
            //TODO: (erikpo) Validation instead of trycatch

            try
            {
                IPlugin plugin = pluginService.GetPlugin(pluginInput.ID);

                plugin.Enabled = pluginInput.Enabled;

                foreach (string name in pluginInput.Settings)
                    plugin.Settings[name] = pluginInput.Settings[name];

                pluginService.Save(plugin);

                return Redirect(Url.Plugins());
            }
            catch
            {
                return Item(pluginInput.ID);
            }
        }
    }
}
