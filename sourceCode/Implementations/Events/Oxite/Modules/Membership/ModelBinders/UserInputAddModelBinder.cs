//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Specialized;
using System.Web.Mvc;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.ModelBinders
{
    public class UserInputAddModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection form = controllerContext.HttpContext.Request.Form;

            string userName = form["userName"];
            string displayName = form["userDisplayName"];
            string email = form["userEmail"];
            string password = form["userPassword"];
            string passwordConfirm = form["userPasswordConfirm"];

            return new UserInputAdd(userName, displayName, email, password, passwordConfirm);
        }
    }
}
