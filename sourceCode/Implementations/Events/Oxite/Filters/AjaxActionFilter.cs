//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Results;

namespace Oxite.Filters
{
    public class AjaxActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsJQueryAjaxRequest())
                return;

            if (filterContext.Result is DialogResult)
                filterContext.HttpContext.Response.AppendHeader("X-Oxite-Dialog", "1");

            if (filterContext.Result is RedirectResult)
                filterContext.Result = new RedirectResult(string.Format("{0}?X-Requested-With=XMLHttpRequest", ((RedirectResult)filterContext.Result).Url));
            else if (filterContext.Result is ViewResult)
                filterContext.Result = getPartialResultFromContext(filterContext);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsJQueryAjaxRequest())
                filterContext.RouteData.Values.Add("X-Requested-With", "XMLHttpRequest");
        }

        #endregion

        private static PartialViewResult getPartialResultFromContext(ActionExecutedContext actionExecutedContext)
        {
            ViewResult viewResult = (ViewResult)actionExecutedContext.Result;

            return new PartialViewResult
            {
                TempData = actionExecutedContext.Controller.TempData,
                View = viewResult.View,
                ViewData = actionExecutedContext.Controller.ViewData,
                ViewEngineCollection = viewResult.ViewEngineCollection,
                ViewName = viewResult.ViewName
            };
        }
    }
}
