//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Models;

namespace Oxite.Filters
{
    public class AreaSkinLayerResultFilter : IResultFilter
    {
        private readonly Site site;

        public AreaSkinLayerResultFilter(Site site)
        {
            this.site = site;
        }

        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext) { }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values.ContainsKey("areaName") && !string.IsNullOrEmpty(filterContext.RouteData.Values["areaName"] as string))
                filterContext.Controller.ViewData["Skin"] = string.Format("{0}/{1}", site.Skin, filterContext.RouteData.Values["areaName"]);
        }

        #endregion
    }
}
