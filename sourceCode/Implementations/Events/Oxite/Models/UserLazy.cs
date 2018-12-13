//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Security.Principal;
using Oxite.Infrastructure;

namespace Oxite.Models
{
    public class UserLazy : IUser
    {
        private IUser user;
        private Func<IUser> getUser;

        public UserLazy(Func<IUser> getUser)
        {
            this.getUser = getUser;
        }

        private IUser internalUser
        {
            get
            {
                if (user == null)
                    user = getUser();

                return user;
            }
        }

        #region IUser Members

        public bool IsAuthenticated
        {
            get { return internalUser.IsAuthenticated; }
        }

        public string Name
        {
            get { return internalUser.Name; }
        }

        public IDictionary<string, object> AuthenticationValues
        {
            get { return internalUser.AuthenticationValues; }
        }

        public T Cast<T>() where T : class, IUser
        {
            return internalUser as T;
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return internalUser.Identity; }
        }

        public bool IsInRole(string role)
        {
            return internalUser.IsInRole(role);
        }

        #endregion
    }
}
