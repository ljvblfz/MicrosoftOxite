//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.Services;
using Oxite.Modules.CMS.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.CMS.Filters
{
    public class ContentItemFilter : IActionFilter
    {
        private readonly IPageService pageService;
        private readonly IContentItemService contentItemService;

        public ContentItemFilter(IPageService pageService, IContentItemService contentItemService)
        {
            this.pageService = pageService;
            this.contentItemService = contentItemService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                List<ContentItem> contentItems = new List<ContentItem>(50);
                Page page = null;
                string pagePath = filterContext.RouteData.Values.ContainsKey("pagePath") ? filterContext.RouteData.Values["pagePath"] as string : null;

                if (!string.IsNullOrEmpty(pagePath))
                    page = pageService.GetPage(new PageAddress(getRealPagePath(filterContext, pagePath)));

                if (page != null)
                {
                    //TODO: (erikpo) Could the following be done just in LINQ?

                    contentItems.AddRange(pageService.GetContentItems(page));

                    foreach (ContentItem contentItem in contentItemService.GetContentItems())
                        if (!contentItems.Any(ci => string.Compare(ci.Name, contentItem.Name, true) == 0))
                            contentItems.Add(contentItem);
                }
                else
                    contentItems.AddRange(contentItemService.GetContentItems());

                model.AddModelItem(new ContentItemViewModel(contentItems));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion

        //todo: (nheskew) use the page address model binder or refactor so they both use the same code to strip off the page mode
        private static string getRealPagePath(ActionExecutedContext filterContext, string pagePath)
        {
            string actionName = filterContext.RouteData.Values["action"] as string;

            if (actionName == "ItemAdd")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Add.ToString().Length));
            if (actionName == "ItemEdit")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Edit.ToString().Length));
            if (actionName == "ItemEditContent")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.EditContent.ToString().Length));
            if (actionName == "Remove")
                pagePath = pagePath.Substring(0, pagePath.Length - (1 + PageMode.Remove.ToString().Length));

            pagePath = pagePath.TrimEnd('/');

            return pagePath;
        }
    }
}
