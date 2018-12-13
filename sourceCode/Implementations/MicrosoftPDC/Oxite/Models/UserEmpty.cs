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
    public class UserEmpty : IUser
    {
        #region IUser Members

        public bool IsAuthenticated { get { return false; } }
        public string Name { get { return null; } }
        public IDictionary<string, object> AuthenticationValues { get { return new Dictionary<string, object>(); } }
        
        public T Cast<T>() where T : class, IUser
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPrincipal Members

        public IIdentity Identity { get { return null; } }

        public bool IsInRole(string role)
        {
            return false;
        }

        #endregion
    }
}
