//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Extensions;

namespace Oxite.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly RouteCollection routes;

        public AuthorizationFilter(RouteCollection routes)
        {
            this.routes = routes;
        }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAuthenticated)
            {
                UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext, routes);

                filterContext.Result = new RedirectResult(urlHelper.SignIn(filterContext.HttpContext.Request.Url.AbsolutePath));
            }
        }

        #endregion
    }
}
