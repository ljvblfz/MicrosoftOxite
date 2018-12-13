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
    public class UserUnregistered : IUser
    {
        public UserUnregistered(string name, string authenticationType, IDictionary<string, object> authenticationModuleValues)
        {
            Identity = new UserIdentity(authenticationType, true, name);
            AuthenticationValues = authenticationModuleValues;
        }

        #region IUser Members

        public bool IsAuthenticated { get { return Identity.IsAuthenticated; } }
        public string Name { get { return Identity.Name; } }
        public IDictionary<string, object> AuthenticationValues { get; private set; }

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
