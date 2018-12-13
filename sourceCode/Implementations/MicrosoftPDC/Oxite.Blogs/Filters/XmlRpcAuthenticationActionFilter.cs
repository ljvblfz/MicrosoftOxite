//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Security.Authentication;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.FormsAuthentication.Extensions;
using Oxite.Modules.Membership.Services;

namespace Oxite.Modules.Blogs.Filters
{
    public class XmlRpcAuthenticationActionFilter : IActionFilter
    {
        private readonly IUserService userService;

        public XmlRpcAuthenticationActionFilter(IUserService userService)
        {
            this.userService = userService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string username = filterContext.ActionParameters["username"] as string;
            string password = filterContext.ActionParameters["password"] as string;

            UserAuthenticated user = userService.GetUser(username, password);

            if (user == null)
                throw new InvalidCredentialException();

            filterContext.HttpContext.Items[typeof(IUser).FullName] = user;
        }

        #endregion
    }
}
