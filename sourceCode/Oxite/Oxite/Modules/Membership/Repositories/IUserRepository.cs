//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Repositories
{
    public interface IUserRepository
    {
        User GetUser(string name);
        User GetUserByModuleData(string moduleName, string data);
        IEnumerable<User> FindUsers(UserSearchCriteria criteria);
        User Save(User user);
        bool Remove(User user);
        string GetModuleData(User user, string moduleName);
        void SetModuleData(User user, string moduleName, string data);
    }
}
