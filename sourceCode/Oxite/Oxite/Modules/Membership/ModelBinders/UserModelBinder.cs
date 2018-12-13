//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Membership.Services;

namespace Oxite.Modules.Membership.ModelBinders
{
    public class UserModelBinder : IModelBinder
    {
        private readonly IUserService userService;

        public UserModelBinder(IUserService userService)
        {
            this.userService = userService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string username = (string)bindingContext.ValueProvider["username"].RawValue;

            return !string.IsNullOrEmpty(username) ? userService.GetUser(username) : null;
        }
    }
}
