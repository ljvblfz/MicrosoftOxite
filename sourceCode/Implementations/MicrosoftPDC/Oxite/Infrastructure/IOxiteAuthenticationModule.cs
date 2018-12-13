//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Routing;
using Oxite.Models;

namespace Oxite.Infrastructure
{
    public interface IOxiteAuthenticationModule : IOxiteModule
    {
        IUser GetUser(RequestContext context);
        string GetRegistrationUrl(RequestContext context);
        string GetSignInUrl(RequestContext context);
        string GetSignOutUrl(RequestContext context);
    }
}
