//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Membership.Models;
using Oxite.Services;

namespace Oxite.Modules.Membership.Services
{
    public interface IRoleService
    {
        Role GetRole(RoleAddress roleAddress);
        IEnumerable<Role> GetSiteRoles();
        IEnumerable<Role> FindRoles(RoleSearchCriteria criteria);
        ModelResult<Role> AddRole(RoleInput roleInput);
        ModelResult<Role> EditRole(RoleAddress roleAddress, RoleInput roleInput);
        void RemoveRole(RoleAddress roleAddress);
    }
}
