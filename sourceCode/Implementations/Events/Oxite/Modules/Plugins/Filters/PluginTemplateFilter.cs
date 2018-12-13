//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Modules.Plugins.Models;
using Oxite.Modules.Plugins.Extensions;
using Oxite.Plugins;
using Oxite.Plugins.Extensions;
using Oxite.ViewModels;

namespace Oxite.Modules.Plugins.Filters
{
    public class PluginTemplateFilter : IActionFilter, IResultFilter
    {
        private readonly PluginTemplateRegistry pluginTemplateRegistry;
        private readonly PluginScriptRegistry pluginScriptRegistry;
        private readonly PluginStyleRegistry pluginStyleRegistry;

        public PluginTemplateFilter(PluginTemplateRegistry pluginTemplateRegistry, PluginScriptRegistry pluginScriptRegistry, PluginStyleRegistry pluginStyleRegistry)
        {
            this.pluginTemplateRegistry = pluginTemplateRegistry;
            this.pluginScriptRegistry = pluginScriptRegistry;
            this.pluginStyleRegistry = pluginStyleRegistry;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                model.PluginTemplates.Clear();

                pluginTemplateRegistry.ForEach(pt => model.PluginTemplates.Add(pt));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion

        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            ResponseFilter responseFilter = filterContext.HttpContext.Response.Filter as ResponseFilter;

            if (responseFilter != null)
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);

                foreach (PluginTemplate pluginTemplate in pluginTemplateRegistry)
                {
                    if (pluginTemplate.ForCurrentRequest(new PluginTemplateContext(filterContext)))
                    {
                        switch (pluginTemplate.SelectorType)
                        {
                            case PluginTemplateSelectorType.ReplaceWith:
                                responseFilter.Inserts.Add(new ResponseInsert(i => renderPluginTemplate(pluginTemplate, filterContext, pluginTemplate.ModelTarget, i), ResponseInsertMode.ReplaceWith, pluginTemplate.Selector));
                                break;
                            case PluginTemplateSelectorType.InsertBefore:
                                responseFilter.Inserts.Add(new ResponseInsert(i => renderPluginTemplate(pluginTemplate, filterContext, pluginTemplate.ModelTarget, i), ResponseInsertMode.InsertBefore, pluginTemplate.Selector));
                                break;
                            case PluginTemplateSelectorType.InsertAfter:
                                responseFilter.Inserts.Add(new ResponseInsert(i => renderPluginTemplate(pluginTemplate, filterContext, pluginTemplate.ModelTarget, i), ResponseInsertMode.InsertAfter, pluginTemplate.Selector));
                                break;
                            case PluginTemplateSelectorType.PrependTo:
                                responseFilter.Inserts.Add(new ResponseInsert(i => renderPluginTemplate(pluginTemplate, filterContext, pluginTemplate.ModelTarget, i), ResponseInsertMode.PrependTo, pluginTemplate.Selector));
                                break;
                            case PluginTemplateSelectorType.AppendTo:
                                responseFilter.Inserts.Add(new ResponseInsert(i => renderPluginTemplate(pluginTemplate, filterContext, pluginTemplate.ModelTarget, i), ResponseInsertMode.AppendTo, pluginTemplate.Selector));
                                break;
                        }
                    }
                }

                foreach (PluginStyle style in pluginStyleRegistry)
                    if (style.ForCurrentRequest(new PluginStyleContext(filterContext)))
                        responseFilter.Inserts.Add(new ResponseInsert(i => string.Format("<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />", urlHelper.PluginStylesPath(style.Plugin, style.Url)), ResponseInsertMode.AppendTo, "head"));

                foreach (PluginScript script in pluginScriptRegistry)
                    if (script.ForCurrentRequest(new PluginScriptContext(filterContext)))
                        responseFilter.Inserts.Add(new ResponseInsert(i => string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", urlHelper.PluginScriptsPath(script.Plugin, script.Url)), ResponseInsertMode.AppendTo, "head"));
            }
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        #endregion

        #region Private Methods

        private static string renderPluginTemplate(PluginTemplate pluginTemplate, ResultExecutedContext context, string partialView, int index)
        {
            ViewPage viewPage = new ViewPage() { ViewContext = new ViewContext(context, ((ViewResult)context.Result).View, context.Controller.ViewData, context.Controller.TempData), ViewData = context.Controller.ViewData };
            StringBuilder sb = new StringBuilder();

            viewPage.InitHelpers();

            //TODO: (erikpo) Change the current model to be wrapped by a PluginViewModel to strongly type things like the plugin and its extended properties
            viewPage.ViewData.Model = getModel(context, partialView, index);
            viewPage.ViewData["Plugin"] = pluginTemplate.Plugin;
            viewPage.ViewData["Index"] = index;
            viewPage.Controls.Add(viewPage.LoadControl(string.Format("{0}/{1}.ascx", pluginTemplate.Plugin.Container.GetTemplatesPath(), pluginTemplate.TemplateName)));

            using (StringWriter sw = new StringWriter(sb))
                using (HtmlTextWriter tw = new HtmlTextWriter(sw))
                    viewPage.RenderControl(tw);

            return sb.ToString();
        }

        private static object getModel(ResultExecutedContext context, string partialView, int index)
        {
            if (!string.IsNullOrEmpty(partialView))
            {
                List<PartialViewRegistration> partialViewRegistrations = context.HttpContext.Items["PartialViewRegistrations"] as List<PartialViewRegistration> ?? new List<PartialViewRegistration>();

                if (partialViewRegistrations.Count > 0)
                {
                    PartialViewRegistration partialViewRegistration = partialViewRegistrations.Where(pvr => string.Compare(pvr.PartialView, partialView, true) == 0).ElementAtOrDefault(index);

                    return partialViewRegistration != null
                               ? partialViewRegistration.Model
                               : null;
                }
            }

            return context.Controller.ViewData.Model;
        }

        #endregion
    }
}
