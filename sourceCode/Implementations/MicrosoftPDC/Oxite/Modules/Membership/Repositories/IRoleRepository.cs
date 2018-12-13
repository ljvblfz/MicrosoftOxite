//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Repositories
{
    public interface IRoleRepository
    {
        Role GetRole(Guid roleID);
        Role GetRole(string roleName);
        IQueryable<Role> GetSiteRoles();
        IQueryable<Role> FindRoles(RoleSearchCriteria criteria);
        Role Save(Role role);
        bool Remove(string roleName);
    }
}
