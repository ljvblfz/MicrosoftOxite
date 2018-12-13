//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;
using OxiteSite.App_Code.Modules.OxiteSite.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.Services
{
    public interface IPDC09Service
    {
        UserRegistration GetUserRegistration(UserAuthenticated user);
        void SetUserRegistration(UserAuthenticated user, bool isRegistered);
    }
}
