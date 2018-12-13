//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Tests.Fakes
{
    public class FakeUserService : IUserService
    {
        public bool Authenticate { get; set; }
        public bool Authorize { get; set; }

        public FakeUserService()
        {
            this.Authenticate = true;
            this.Authorize = true;
        }

        #region IUserService Members

        public User GetUser(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return new User() { Name = name };
            else
                return null;
        }

        public User GetUser(string name, string password)
        {
            return this.Authenticate ? new User() { Name = name, Password = password } : null;
        }

        public IList<User> FindUsers(UserSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public void EnsureAnonymousUser(Language languageDefault)
        {
            throw new NotImplementedException();
        }

        public void AddUser(UserInputAdd userInput, out ValidationStateDictionary validationState, out User newUser)
        {
            throw new NotImplementedException();
        }

        public void EditUser(User user, UserInputEdit userInput, out ValidationStateDictionary validationState)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(UserAddress userAddress)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(UserAddress userAddress, UserChangePasswordInput userInput, out ValidationStateDictionary validationState)
        {
            throw new NotImplementedException();
        }

        public IPageOfItems<User> GetUsersInRole(int pageIndex, int pageSize, string roleName)
        {
            throw new NotImplementedException();
        }

        //public bool VerifyAccess(string name, Area area)
        //{
        //    return Authorize;
        //}

        //public bool VerifyAccess(string name, Page page)
        //{
        //    return Authorize;
        //}

        #endregion
    }
}
