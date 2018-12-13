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
    public interface IUserRepository
    {
        UserAuthenticated GetUser(Guid siteID, Guid id);
        UserAuthenticated GetUser(Guid siteID, string name);
        UserAuthenticated GetUserByModuleData(Guid siteID, string moduleName, string data);
        IQueryable<UserAuthenticated> FindUsers(UserSearchCriteria criteria);
        UserAuthenticated Save(UserAuthenticated user, Guid siteID);
        bool Remove(Guid userID);
        string GetModuleData(Guid siteID, string userName, string moduleName);
        string GetModuleData(Guid siteID, Guid userID, string moduleName);
        void SetModuleData(Guid siteID, string userName, string moduleName, string data);
        void SetModuleData(Guid siteID, Guid userID, string moduleName, string data);
    }
}
