//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Results;

namespace Oxite.Filters
{
    public class DialogActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Result is DialogSelectionResult)
            {
                DialogSelectionResult dialogSelectionResult = (DialogSelectionResult)filterContext.Result;

                if (filterContext.HttpContext.Request.IsJQueryAjaxRequest())
                {
                    if (dialogSelectionResult.IsCancelled)
                        filterContext.Result = new JsonResult { Data = new { cancel = 1 } };
                    else if (dialogSelectionResult.IsClientRedirect)
                        filterContext.Result = new JsonResult { Data = new AjaxRedirect(dialogSelectionResult.ReturnUrl) };
                }
                
                if (!(filterContext.Result is JsonResult))
                    filterContext.Result = new RedirectResult(dialogSelectionResult.ReturnUrl);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
