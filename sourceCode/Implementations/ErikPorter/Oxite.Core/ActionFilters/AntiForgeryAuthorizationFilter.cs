//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;

namespace Oxite.ActionFilters
{
    public class AntiForgeryAuthorizationFilter : IAuthorizationFilter
    {
        private readonly Site site;

        public AntiForgeryAuthorizationFilter(Site site)
        {
            this.site = site;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!(filterContext.RouteData.Values["validateAntiForgeryToken"] is bool
                && (bool)filterContext.RouteData.Values["validateAntiForgeryToken"]
                && filterContext.HttpContext.Request.HttpMethod == "POST"
                && filterContext.RequestContext.HttpContext.Request.IsAuthenticated))
            {
                return;
            }

            ValidateAntiForgeryTokenAttribute validator = new ValidateAntiForgeryTokenAttribute { Salt = site.ID.ToString() };

            validator.OnAuthorization(filterContext);
        }
    }
}
