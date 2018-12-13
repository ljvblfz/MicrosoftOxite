//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Models
{
    public class Role : EntityBase, INamedEntity
    {
        private List<Role> roles;

        public Role(Guid id)
            : base(id)
        {
        }

        public Role(Role group)
        {
            Group = group;
        }

        public Role(Role group, RoleType type, string name)
        {
            Group = group;
            Type = type;
            Name = name;
        }

        public Role(Role group, RoleType type, Guid id, string name)
            : this(id)
        {
            Group = group;
            Type = type;
            Name = name;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public Role Group { get; private set; }
        public RoleType Type { get; private set; }
        public IEnumerable<Role> Roles
        {
            get
            {
                if (roles == null)
                    roles = new List<Role>(10);

                return roles;
            }
        }

        public Role AddRole(RoleType type, Guid id, string name)
        {
            if (roles == null)
                roles = new List<Role>(10);

            Role role = new Role(this, type, id, name);

            roles.Add(role);

            return role;
        }
    }
}
