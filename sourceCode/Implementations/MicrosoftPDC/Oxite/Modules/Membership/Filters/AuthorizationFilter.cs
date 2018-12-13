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

namespace Oxite.Modules.Membership.Filters
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly IModulesLoaded modules;

        public AuthorizationFilter(IModulesLoaded modules)
        {
            this.modules = modules;
        }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            IEnumerable<IOxiteAuthenticationModule> authenticationModules = modules.GetModules<IOxiteAuthenticationModule>().Reverse();
            IUser user = null;

            foreach (IOxiteAuthenticationModule module in authenticationModules)
            {
                user = module.GetUser(filterContext.RequestContext);

                if (user.IsAuthenticated)
                    break;
            }

            if (user == null)
                filterContext.Result = new RedirectResult(authenticationModules.First().GetSignInUrl(filterContext.RequestContext));
        }

        #endregion
    }
}
