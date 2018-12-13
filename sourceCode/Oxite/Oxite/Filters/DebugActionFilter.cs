//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Filters
{
    public class DebugActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool debug = false;

#if DEBUG
            debug = true;
#endif

            string queryStringDebugValue = filterContext.HttpContext.Request.QueryString["Debug"];
            if (!string.IsNullOrEmpty(queryStringDebugValue))
            {
                bool queryStringDebug;
                if (bool.TryParse(queryStringDebugValue, out queryStringDebug))
                    debug = queryStringDebug;
            }

            filterContext.Controller.ViewData["Debug"] = debug;
        }

        #endregion
    }
}
