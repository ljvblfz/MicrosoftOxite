//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Services;
using Oxite.ViewModels;
using System.Collections.Generic;

namespace Oxite.Modules.Membership.Filters
{
    public class UserActionFilter : IActionFilter
    {
        private readonly IUnityContainer container;
        private readonly IUserService userService;
        private readonly IModulesLoaded modules;

        public UserActionFilter(IUnityContainer container, IUserService userService, IModulesLoaded modules)
        {
            this.container = container;
            this.userService = userService;
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
                    model.User = new UserViewModel(user);
                    model.SignInUrl = null;
                    model.SignOutUrl = authenticationModules.First().GetSignOutUrl(filterContext.RequestContext);
                }
                else
                {
                    model.User = new UserViewModel(filterContext.HttpContext.Request.Cookies.GetAnonymousUser());
                    model.SignInUrl = authenticationModules.First().GetSignInUrl(filterContext.RequestContext);
                    model.SignOutUrl = null;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
