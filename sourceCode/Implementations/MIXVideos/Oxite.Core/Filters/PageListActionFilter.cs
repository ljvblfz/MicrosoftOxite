//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Filters
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
            OxiteModel model = filterContext.Controller.ViewData.Model as OxiteModel;

            if (model != null)
            {
                model.AddModelItem(new PageListViewModel(pageService.GetPages()));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
