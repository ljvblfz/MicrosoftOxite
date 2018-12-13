//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Plugins.Extensions;
using Oxite.Modules.Plugins.Models;
using Oxite.Modules.Plugins.Services;
using Oxite.Plugins;
using Oxite.Plugins.Extensions;
using Oxite.Results;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Plugins.Controllers
{
    public class PluginController : Controller
    {
        private readonly IPluginService pluginService;
        private readonly IPluginEngine pluginEngine;
        private readonly OxiteContext context;

        public PluginController(IPluginService pluginService, IPluginEngine pluginEngine, OxiteContext context)
        {
            this.pluginService = pluginService;
            this.pluginEngine = pluginEngine;
            this.context = context;
            ValidateRequest = false;
        }

        public OxiteViewModelItems<PluginContainer> List(bool? installed)
        {
            return new OxiteViewModelItems<PluginContainer>(installed != null ? pluginEngine.GetPlugins(p => (bool)installed ? p.Tag is Plugin : p.Tag == null || !(p.Tag is Plugin)).FillContainer(pluginEngine) : pluginEngine.GetPlugins().FillContainer(pluginEngine));
        }

        public ActionResult RefreshListNotInstalled()
        {
            pluginEngine.ReloadPlugins("~" + context.Site.PluginsPath, p => p.Tag == null || !(p.Tag is Plugin));

            return RedirectToAction("List", RouteData.Values);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditNotInstalled(PluginNotInstalledAddress pluginNotInstalledAddress)
        {
            string code = pluginNotInstalledAddress.VirtualPath.GetFileText(HttpContext);

            OxiteViewModelItem<PluginEditInput> model = new OxiteViewModelItem<PluginEditInput>(new PluginEditInput(pluginNotInstalledAddress.VirtualPath, code, false, null));

            model.AddModelItem(pluginEngine.GetPlugin(new Plugin(Guid.Empty, Guid.Empty, pluginNotInstalledAddress.VirtualPath, false)));

            return View("ItemEdit", model);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult ReloadNotInstalled(PluginNotInstalledAddress pluginNotInstalledAddress)
        {
            if (!string.IsNullOrEmpty(pluginNotInstalledAddress.VirtualPath))
                pluginEngine.ReloadPlugin(p => p.VirtualPath == pluginNotInstalledAddress.VirtualPath, p => p.VirtualPath);

            return Redirect(Url.Plugins());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Install(PluginInstallInput pluginInstallInput, DialogSelection dialogSelection)
        {
            if (dialogSelection != null && dialogSelection.Equals(DialogButton.Cancel))
                return new DialogSelectionResult(dialogSelection.ReturnUrl, true);

            PluginContainer pluginContainer = pluginEngine.LoadPlugin(pluginInstallInput.VirtualPath);

            if (pluginContainer == null) return null;

            ValidationStateDictionary validationState = pluginService.ValidatePlugin(pluginContainer);

            if (dialogSelection == null)
            {
                if (validationState.IsValid)
                    return this.Dialog("What would you like to do next?", DialogFormat.Question, new DialogButton("no", "Enable this plugin", true), new DialogButton("yes", "Edit plugin settings", true), new DialogButton("cancel", "Cancel install", false));

                return this.Dialog("This plugin requires you to fill in some required settings before it can be enabled. Would you like to do that now?", DialogFormat.Question, DialogButton.Yes, new DialogButton("no", "No", true), DialogButton.Cancel);
            }

            bool? overrideEnabled = validationState.IsValid
                ? (bool?)(dialogSelection.Equals(DialogButton.No) /* to editing settings */
                    ? true
                    : false)
                : null;

            Plugin plugin = pluginService.InstallPlugin(pluginInstallInput, overrideEnabled);

            if (dialogSelection.Equals(DialogButton.Yes)) /* to editing settings */
                return new DialogSelectionResult(Url.PluginEdit(plugin)) { IsClientRedirect = true };

            return Redirect(dialogSelection.ReturnUrl);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Uninstall(PluginAddress pluginAddress, DialogSelection dialogSelection)
        {
            if (dialogSelection != null && dialogSelection.Equals(DialogButton.Cancel))
                return new DialogSelectionResult(dialogSelection.ReturnUrl, true);

            if (dialogSelection == null)
                return this.Dialog("Are you sure you want to uninstall this plugin?", DialogFormat.Question, DialogButton.Yes, new DialogButton("cancel", "No", true));

            pluginService.UninstallPlugin(pluginAddress);

            return Redirect(dialogSelection.ReturnUrl);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Reload(PluginAddress pluginAddress)
        {
            Plugin plugin = pluginService.GetPlugin(pluginAddress);

            if (plugin == null) return null;

            pluginService.ReloadPlugin(pluginAddress);

            return Redirect(Url.Plugin(plugin));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<PluginEditInput> ItemEdit(PluginAddress pluginAddress, PluginEditInput pluginEditInput)
        {
            Plugin plugin = pluginService.GetPlugin(pluginAddress);

            if (plugin == null) return null;

            OxiteViewModelItem<PluginEditInput> model = new OxiteViewModelItem<PluginEditInput>(pluginEditInput == null ? new PluginEditInput(plugin.GetFileText(), plugin.Enabled, plugin.ExtendedProperties) : new PluginEditInput(null, pluginEditInput.Code ?? plugin.GetFileText(), pluginEditInput.Enabled, pluginEditInput.PropertyValues));

            model.AddModelItem(pluginEngine.GetPlugin(plugin));

            return model;
        }

        [ActionName("ItemEdit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public object ItemEditSave(PluginAddress pluginAddress, PluginEditInput pluginEditInput, bool isEditingCode)
        {
            if (isEditingCode && !string.IsNullOrEmpty(pluginEditInput.VirtualPath))
            {
                pluginEditInput.VirtualPath.SaveFileText(pluginEditInput.Code, HttpContext);

                pluginEngine.ReloadPlugin(p => p.Tag == null && !(p.Tag is Plugin) && string.Compare(p.VirtualPath, pluginEditInput.VirtualPath, true) == 0, p => pluginEditInput.VirtualPath);

                return EditNotInstalled(new PluginNotInstalledAddress(pluginEditInput.VirtualPath));
            }

            ModelResult<Plugin> results = null;
            Plugin plugin = pluginService.GetPlugin(pluginAddress);

            if (isEditingCode && !string.IsNullOrEmpty(pluginEditInput.Code) && pluginEditInput.Code != plugin.GetFileText())
                results = pluginService.EditPlugin(pluginAddress, pluginEditInput, true);
            else if (!isEditingCode)
                results = pluginService.EditPlugin(pluginAddress, pluginEditInput, false);

            if (results != null && !results.IsValid)
                ViewData["ValidationState"] = results.ValidationState;

            if(isEditingCode || ViewData.ContainsKey("ValidationState"))
                return ItemEdit(pluginAddress, pluginEditInput);

            return Redirect(Url.Plugin(plugin));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SetEnabled(PluginAddress pluginAddress, bool enabled, string returnUrl, DialogSelection dialogSelection)
        {
            if (dialogSelection != null && dialogSelection.Equals(DialogButton.Cancel))
                    return new DialogSelectionResult(dialogSelection.ReturnUrl, true);

            Plugin plugin = pluginService.GetPlugin(pluginAddress);

            if (plugin == null) return null;

            plugin.FillContainer(pluginEngine);

            if (dialogSelection == null && !plugin.Enabled && !plugin.Container.IsValid)
                return this.Dialog("You can not enable a plugin that does not have all its required settings filled in. What would you like to do next?", DialogFormat.Info, new DialogButton("yes", "Edit settings", true), new DialogButton("cancel", "Cancel", false));

            if (dialogSelection != null)
                    return new DialogSelectionResult(Url.PluginEdit(plugin)) { IsClientRedirect = true };

            if (plugin.Container.IsValid || (!plugin.Container.IsValid && !enabled))
                pluginService.SetPluginEnabled(pluginAddress, enabled);

            return Redirect(returnUrl);
        }

        //TODO: (erikpo) In the long term, we should get rid of this and build our own implementation of IRouteHandler to just call their method on the plugin directly
        public object CallMethod(FormCollection form)
        {
            PluginRoute route = ControllerContext.RouteData.Route as PluginRoute;

            if (route == null) return null;

            Plugin plugin = pluginService.GetPlugin(new PluginAddress(route.PluginID));
            
            plugin.FillContainer(pluginEngine);

            plugin.Container.ExecuteMethod(route.MethodName, ControllerContext.RequestContext, form);

            string redirectUri = form["redirectUri"];

            if (string.IsNullOrEmpty(redirectUri)) throw new ArgumentException("No redirectUri was specified in the form data");

            return Redirect(redirectUri);
        }
    }
}
