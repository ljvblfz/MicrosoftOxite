//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Membership.Models
{
    public class UserAddress
    {
        public UserAddress(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; private set; }
    }
}
