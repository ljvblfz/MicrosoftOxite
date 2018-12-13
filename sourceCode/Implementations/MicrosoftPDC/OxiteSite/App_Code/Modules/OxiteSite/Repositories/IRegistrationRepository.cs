//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using OxiteSite.App_Code.Modules.OxiteSite.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.Repositories
{
    public interface IRegistrationRepository
    {
        UserRegistration GetUserRegistration(Guid userID);
        void SetUserRegistration(Guid userID, bool isRegistered);
    }
}
