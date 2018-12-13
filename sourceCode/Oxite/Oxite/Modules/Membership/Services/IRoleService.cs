//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Services
{
    public interface IRoleService
    {
        Role GetRole(string name);
        IEnumerable<Role> GetSiteRoles();
        IEnumerable<Role> FindRoles(RoleSearchCriteria criteria);
        ModelResult<Role> AddRole(RoleInput roleInput);
        ModelResult<Role> EditRole(Role role, RoleInput roleInput);
        void RemoveRole(Role role);
    }
}
