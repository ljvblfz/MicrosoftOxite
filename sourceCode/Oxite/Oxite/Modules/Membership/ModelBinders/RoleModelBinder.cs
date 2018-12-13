//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Membership.Services;

namespace Oxite.Modules.Membership.ModelBinders
{
    public class RoleModelBinder : IModelBinder
    {
        private readonly IRoleService roleService;

        public RoleModelBinder(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string roleName = (string)bindingContext.ValueProvider["roleName"].RawValue;

            return !string.IsNullOrEmpty(roleName) ? roleService.GetRole(roleName) : null;
        }
    }
}
