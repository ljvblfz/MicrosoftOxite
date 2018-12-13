//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Tags.Models;
using Oxite.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class PostTag : Tag, INamedEntity
    {
        public PostTag(Guid id, string displayName)
            : base(id)
        {
            DisplayName = displayName;
        }

        public PostTag(string name, string displayName)
            : base(name)
        {
            DisplayName = displayName;
        }

        public PostTag(Guid id, string name, string displayName, DateTime created)
            : base(id, name, created)
        {
            DisplayName = displayName;
        }
    }
}
