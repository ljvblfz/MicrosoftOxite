//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Membership.Models
{
    public class UserSearchCriteria
    {
        public UserSearchCriteria(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
