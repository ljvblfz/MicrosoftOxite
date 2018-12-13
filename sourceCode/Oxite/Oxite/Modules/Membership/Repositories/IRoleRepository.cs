//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Repositories
{
    public interface IRoleRepository
    {
        Role GetRole(string roleName);
        IEnumerable<Role> GetSiteRoles();
        IEnumerable<Role> FindRoles(RoleSearchCriteria criteria);
        Role Save(Role role);
        bool Remove(Role role);
    }
}
