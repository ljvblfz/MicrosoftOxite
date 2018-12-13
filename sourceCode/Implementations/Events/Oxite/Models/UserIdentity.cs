//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Security.Principal;

namespace Oxite.Models
{
    public class UserIdentity : IIdentity
    {
        public UserIdentity(string authenticationType, bool isAuthenticated, string name)
        {
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            Name = name;
        }

        #region IIdentity Members

        public string AuthenticationType { get; internal set; }
        public bool IsAuthenticated { get; private set; }
        public string Name { get; private set; }

        #endregion
    }
}
