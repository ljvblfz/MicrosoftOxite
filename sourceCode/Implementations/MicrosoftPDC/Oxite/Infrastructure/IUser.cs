//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Security.Principal;

namespace Oxite.Infrastructure
{
    public interface IUser : IPrincipal
    {
        bool IsAuthenticated { get; }
        string Name { get; }
        IDictionary<string, object> AuthenticationValues { get; }
        T Cast<T>() where T : class, IUser;
    }
}
