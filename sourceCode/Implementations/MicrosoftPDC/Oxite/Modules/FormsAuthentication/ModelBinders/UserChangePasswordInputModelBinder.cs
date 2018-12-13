//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Specialized;
using System.Web.Mvc;
using Oxite.Modules.FormsAuthentication.Models;

namespace Oxite.Modules.FormsAuthentication.ModelBinders
{
    public class UserChangePasswordInputModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection form = controllerContext.HttpContext.Request.Form;

            string password = form["userPassword"];
            string passwordConfirm = form["userPasswordConfirm"];

            return new UserChangePasswordInput(password, passwordConfirm);
        }
    }
}
