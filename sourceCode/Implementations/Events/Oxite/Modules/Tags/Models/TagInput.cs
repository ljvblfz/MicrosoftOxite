//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Tags.Models
{
    public class TagInput
    {
        public TagInput(string name)
        {
            Name = name;
        }

        public TagInput(string name, DateTime created)
            : this(name)
        {
            Created = created;
        }

        public string Name { get; private set; }
        public DateTime Created { get; private set; }

        public Tag ToTag()
        {
            return new Tag(Guid.Empty, Name, Created);
        }
    }
}
