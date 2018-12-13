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

namespace Oxite.Controllers
{
    public class PluginController : Controller
    {
        private readonly IBackgroundServiceService pluginService;

        public PluginController(IBackgroundServiceService pluginService)
        {
            this.pluginService = pluginService;
        }

        public OxiteModelList<Oxite.BackgroundServices.IBackgroundService> List()
        {
            return new OxiteModelList<Oxite.BackgroundServices.IBackgroundService> { List = pluginService.GetBackgroundServices() };
        }

        //[AcceptVerbs(HttpVerbs.Get)]
        //public OxiteModelList<Plugin> Install()
        //{
        //    return new OxiteModelList<Oxite.BackgroundServices.IBackgroundService> { List = pluginService.GetBackgroundServicesNotInstalled() };
        //}

        //[AcceptVerbs(HttpVerbs.Post)]
        //[ActionName("Install")]
        //public ActionResult InstallSave(Plugin pluginInput)
        //{
        //    pluginInput.Enabled = false;

        //    pluginService.Save(pluginInput);

        //    OxiteApplication.Load(ControllerContext.HttpContext);

        //    return Redirect(Url.Plugins());
        //}

        //[AcceptVerbs(HttpVerbs.Get)]
        //public OxiteModelItem<Plugin> Item(Guid pluginID)
        //{
        //    return new OxiteModelItem<Plugin> { Item = pluginService.GetPlugin(pluginID) };
        //}

        //[ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult SaveItem(Plugin pluginInput)
        //{
        //    //TODO: (erikpo) Validation instead of trycatch

        //    try
        //    {
        //        IPlugin plugin = pluginService.GetPlugin(pluginInput.ID);

        //        plugin.Enabled = pluginInput.Enabled;

        //        foreach (string name in pluginInput.Settings)
        //        {
        //            plugin.Settings[name] = pluginInput.Settings[name];
        //        }

        //        pluginService.Save(plugin);

        //        return Redirect(Url.Plugins());
        //    }
        //    catch
        //    {
        //        return Item(pluginInput.ID);
        //    }
        //}
    }
}
