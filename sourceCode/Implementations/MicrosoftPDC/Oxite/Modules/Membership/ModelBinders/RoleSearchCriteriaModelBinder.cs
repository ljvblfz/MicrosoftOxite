//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Specialized;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.ModelBinders
{
    public class RoleSearchCriteriaModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection form = controllerContext.HttpContext.Request.Form;
            byte roleType = 0;

            if (form.IsTrue("roleTypeSite"))
                roleType += (byte)RoleType.Site;
            if (form.IsTrue("roleTypeBlog"))
                roleType += (byte)RoleType.Blog;
            if (form.IsTrue("roleTypePost"))
                roleType += (byte)RoleType.Post;
            if (form.IsTrue("roleTypePage"))
                roleType += (byte)RoleType.Page;

            return new RoleSearchCriteria(form.Get("roleNameSearch"), (RoleType)roleType);
        }
    }
}
