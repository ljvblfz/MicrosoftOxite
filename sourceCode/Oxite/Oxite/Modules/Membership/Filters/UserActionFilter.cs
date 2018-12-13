//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Modules.Membership.ViewModels;
using Oxite.ViewModels;
using System.Collections.Generic;

namespace Oxite.Modules.Membership.Filters
{
    public class UserActionFilter : IActionFilter
    {
        private readonly IModulesLoaded modules;

        public UserActionFilter(IModulesLoaded modules)
        {
            this.modules = modules;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                IEnumerable<IOxiteAuthenticationModule> authenticationModules = modules.GetModules<IOxiteAuthenticationModule>().Reverse();
                IUser user = null;

                foreach (IOxiteAuthenticationModule module in authenticationModules)
                {
                    user = module.GetUser(filterContext.RequestContext);

                    if (user.IsAuthenticated)
                        break;
                }

                if (user.IsAuthenticated)
                {
                    model.AddModelItem(new UserViewModel(user));
                    //TODO: (erikpo) Change this to call the sign out url of the auth module that retrieved the user
                    model.AddModelItem(new MembershipUrlViewModel(null, authenticationModules.First().GetSignOutUrl(filterContext.RequestContext)));
                }
                else
                {
                    model.AddModelItem(new UserViewModel(filterContext.HttpContext.Request.Cookies.GetAnonymousUser()));
                    //TODO: (erikpo) Change the following so if there's more than one auth module show a generic sign in page, if there's only one auth module, then use its sign in url
                    model.AddModelItem(new MembershipUrlViewModel(authenticationModules.First().GetSignInUrl(filterContext.RequestContext), null));
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
