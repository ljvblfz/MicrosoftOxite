//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;

namespace Oxite.Modules.Blogs.Filters
{
    public class FeedPageSizeActionFilter : IActionFilter
    {
        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("pagingInfo"))
            {
                string dataFormat = filterContext.RouteData.Values["dataFormat"] as string;

                if (
                    !string.IsNullOrEmpty(dataFormat) &&
                    (
                        string.Compare(dataFormat, "RSS", true) == 0 ||
                        string.Compare(dataFormat, "ATOM", true) == 0 ||
                        string.Compare(dataFormat, "JSON", true) == 0
                    )
                )
                    ((PagingInfo)filterContext.ActionParameters["pagingInfo"]).Size = 50;
            }
        }

        #endregion
    }
}