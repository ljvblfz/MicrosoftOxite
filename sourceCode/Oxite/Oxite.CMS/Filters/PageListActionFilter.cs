//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.CMS.Services;
using Oxite.Modules.CMS.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.CMS.Filters
{
    public class PageListActionFilter : IActionFilter
    {
        private readonly IPageService pageService;

        public PageListActionFilter(IPageService pageService)
        {
            this.pageService = pageService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
                model.AddModelItem(new PageListViewModel(pageService.GetPages()));
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
