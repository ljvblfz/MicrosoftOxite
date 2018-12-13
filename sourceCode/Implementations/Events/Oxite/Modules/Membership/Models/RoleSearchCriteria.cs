//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;

namespace Oxite.Modules.Membership.Models
{
    public class RoleSearchCriteria
    {
        public RoleSearchCriteria(string roleName, RoleType roleType)
        {
            RoleName = roleName;
            RoleType = roleType;
        }

        public string RoleName { get; private set; }
        public RoleType RoleType { get; private set; }
    }
}
