//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Services;
using Oxite.Infrastructure;

namespace Oxite.Filters
{
    public class BlogSkinLayerResultFilter : IResultFilter
    {
        private readonly OxiteContext context;

        public BlogSkinLayerResultFilter(OxiteContext context)
        {
            this.context = context;
        }

        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext) { }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values.ContainsKey("blogName") && !string.IsNullOrEmpty(filterContext.RouteData.Values["blogName"] as string))
                filterContext.Controller.ViewData["Skin"] = string.Format("{0}/{1}", context.Site.Skin, filterContext.RouteData.Values["blogName"]);
        }

        #endregion
    }
}
