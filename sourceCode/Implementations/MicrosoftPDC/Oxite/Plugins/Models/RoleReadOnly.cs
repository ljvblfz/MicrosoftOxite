//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Plugins.Models
{
    public class RoleReadOnly
    {
        public RoleReadOnly(Oxite.Models.Role role)
        {
            Name = role.Name;
            DisplayName = role.DisplayName;
            Group = new RoleReadOnly(role.Group);
            Type = role.Type;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public RoleReadOnly Group { get; private set; }
        public Oxite.Models.RoleType Type { get; private set; }
    }
}
