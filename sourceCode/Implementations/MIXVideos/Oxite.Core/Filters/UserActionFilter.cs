//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class UserActionFilter : IActionFilter
    {
        private readonly IUserService userService;

        public UserActionFilter(IUserService userService)
        {
            this.userService = userService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteModel model = filterContext.Controller.ViewData.Model as OxiteModel;

            if (model != null)
            {
                UserBase user = filterContext.HttpContext.User.Identity.IsAuthenticated
                    ? userService.GetUser(filterContext.HttpContext.User.Identity.Name)
                    : filterContext.HttpContext.Request.Cookies.GetAnonymousUser();

                if (user != null)
                    model.User = new UserViewModel(user);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("currentUser"))
            {
                User user = null;

                if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                    user = userService.GetUser(filterContext.HttpContext.User.Identity.Name);

                filterContext.ActionParameters["currentUser"] = user;
            }
        }

        #endregion
    }
}
