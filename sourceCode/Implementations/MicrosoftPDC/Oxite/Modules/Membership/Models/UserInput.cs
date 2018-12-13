//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Modules.Membership.Models
{
    public abstract class UserInput
    {
        public UserInput(string userName, string displayName, string email)
        {
            UserName = userName;
            DisplayName = displayName;
            Email = email;
        }

        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
    }
}
