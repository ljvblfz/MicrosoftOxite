//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Results;
using Oxite.Extensions;

namespace Oxite.Modules.Membership.Filters
{
    public class SiteAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IModulesLoaded modules;

        public SiteAuthorizationFilter(IModulesLoaded modules)
        {
            this.modules = modules;
        }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            string role = filterContext.RouteData.Values["role"] as string;
            bool isAuthorized = true;

            if (!string.IsNullOrEmpty(role))
            {
                IEnumerable<IOxiteAuthenticationModule> authenticationModules = modules.GetModules<IOxiteAuthenticationModule>().Reverse();
                IUser user = null;

                foreach (IOxiteAuthenticationModule module in authenticationModules)
                {
                    user = module.GetUser(filterContext.RequestContext);

                    if (user.IsAuthenticated)
                    {
                        if (!user.IsInRole(role))
                            isAuthorized = false;

                        break;
                    }
                }

                if (!user.IsAuthenticated)
                {
                    string signInUrl = authenticationModules.First().GetSignInUrl(filterContext.RequestContext);

                    if (!filterContext.HttpContext.Request.IsJQueryAjaxRequest())
                        filterContext.Result = new RedirectResult(signInUrl);
                    else
                        filterContext.Result = new JsonResult { Data = new AjaxRedirect(signInUrl) };
                }
            }

            if (!isAuthorized)
                filterContext.Result = new UnauthorizedResult();
        }

        #endregion
    }
}
