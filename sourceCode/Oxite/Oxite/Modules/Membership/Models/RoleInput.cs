//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Membership.Models
{
    public class RoleInput
    {
        public RoleInput(Guid roleGroupID, string roleName, RoleType roleType)
        {
            RoleGroupID = roleGroupID;
            RoleName = roleName;
            RoleType = roleType;
        }

        public Guid RoleGroupID { get; private set; }
        public string RoleName { get; private set; }
        public RoleType RoleType { get; private set; }

        public Role ToRole()
        {
            return new Role(new Role(RoleGroupID), RoleType, RoleName);
        }
    }
}
