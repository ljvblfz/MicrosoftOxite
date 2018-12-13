//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Extensions
{
    public static class RoleExtensions
    {
        public static Role Apply(this Role role, RoleInput input)
        {
            return new Role(new Role(input.RoleGroupID), input.RoleType, role.ID, input.RoleName);
        }
    }
}
