//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;

namespace Oxite.ActionFilters
{
    public class SkinResultFilter : IResultFilter
    {
        private Site site;

        public SkinResultFilter(Site site)
        {
            this.site = site;
        }

        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ViewResult result = filterContext.Result as ViewResult;

            if (result != null && result.ViewData["skin"] == null)
            {
                result.ViewData["skin"] = site.SkinDefault;
            }
        }

        #endregion
    }
}
