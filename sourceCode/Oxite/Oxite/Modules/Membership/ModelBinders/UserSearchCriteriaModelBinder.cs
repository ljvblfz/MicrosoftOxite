//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.ModelBinders
{
    public class UserSearchCriteriaModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return new UserSearchCriteria(controllerContext.HttpContext.Request.Form.Get("userNameSearch"));
        }
    }
}
