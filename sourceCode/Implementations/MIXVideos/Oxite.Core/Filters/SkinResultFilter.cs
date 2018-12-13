//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Skinning;
using System.Web.Routing;

namespace Oxite.Filters
{
    public class SkinResultFilter : IResultFilter, IExceptionFilter
    {
        private readonly ISkinResolverRegistry skinResolvers;
        private readonly Site site;

        public SkinResultFilter(ISkinResolverRegistry skinResolvers, Site site)
        {
            this.skinResolvers = skinResolvers;
            this.site = site;
        }

        #region IResultFilter Members

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            setSkin(filterContext.RequestContext, filterContext.Controller.ViewData, filterContext.Result as ViewResult);
        }

        #endregion

        #region IExceptionFilter Members

        public void OnException(ExceptionContext filterContext)
        {
            setSkin(filterContext.RequestContext, filterContext.Controller.ViewData, filterContext.Result as ViewResult);
        }

        #endregion

        private void setSkin(RequestContext requestContext, ViewDataDictionary viewData, ViewResult result)
        {
            HttpRequestBase request = requestContext.HttpContext.Request;
            string queryStringSkinName = request.QueryString["skin"];
            string cookieSkinName = request.Cookies.GetSkinName();
            string skin = "";

            if (!string.IsNullOrEmpty(queryStringSkinName))
                skin = queryStringSkinName;
            else if (!string.IsNullOrEmpty(cookieSkinName))
                skin = cookieSkinName;

            if (skin == "" && request.Url.PathAndQuery.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
                skin = site.AdminSkin;

            if (skin == "")
                skin = !string.IsNullOrEmpty(viewData["Skin"] as string)
                    ? viewData["Skin"] as string
                    : site.Skin;

            IEnumerable<IOxiteViewEngine> viewEngines = skinResolvers.GenerateViewEngines(new SkinResolverContext(requestContext, skin), skin);

            if (result != null)
            {
                result.ViewEngineCollection = new ViewEngineCollection(viewEngines.Cast<IViewEngine>().ToList());

                result.ViewData["OxiteViewEngines"] = viewEngines;
            }

            viewData["OxiteViewEngines"] = viewEngines;
        }
    }
}
