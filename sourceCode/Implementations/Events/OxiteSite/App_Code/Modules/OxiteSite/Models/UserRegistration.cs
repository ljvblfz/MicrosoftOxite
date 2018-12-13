//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace OxiteSite.App_Code.Modules.OxiteSite.Models
{
    public class UserRegistration
    {
        public UserRegistration(bool isRegistered, DateTime? lastRegistrationCheck)
        {
            IsRegistered = isRegistered;
            LastRegistrationCheck = lastRegistrationCheck;
        }

        public bool IsRegistered { get; private set; }
        public DateTime? LastRegistrationCheck { get; private set; }
    }
}
