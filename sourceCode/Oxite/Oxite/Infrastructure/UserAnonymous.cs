//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace Oxite.Infrastructure
{
    public class UserAnonymous : IUser
    {
        public UserAnonymous()
        {
            Identity = new UserIdentity(null, false, null);
        }

        public UserAnonymous(string name, string email, string emailHash, string url)
        {
            Identity = new UserIdentity(null, false, name);
            Email = email;
            EmailHash = emailHash;
            Url = url;
        }

        public string Email { get; private set; }
        public string EmailHash { get; private set; }
        public string Url { get; private set; }

        #region IUser Members

        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }
        public string Name { get { return Identity.Name; } }
        public IDictionary<string, object> AuthenticationValues { get { return new Dictionary<string, object>(); } }

        public T Cast<T>() where T : class, IUser
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion
    }
}