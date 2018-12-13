//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Membership.Models;

namespace Oxite.Modules.Membership.Services
{
    public interface IUserService
    {
        User GetUser(string name);
        User GetUserByModuleData(string moduleName, string data);
        IEnumerable<User> FindUsers(UserSearchCriteria criteria);
        ModelResult<User> AddUser(UserInputAdd userInput);
        ModelResult<User> EditUser(User user, UserInputEdit userInput);
        void RemoveUser(User user);
        string GetModuleData(User user, string moduleName);
        void SetModuleData(User user, string moduleName, string data);
        void EnsureAnonymousUser();
        bool SignIn(string name);
        bool SignIn(Func<User> getUser, Action<User> afterSignIn);
        void SignOut();
    }
}
