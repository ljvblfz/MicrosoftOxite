//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Tags.Models;

namespace Oxite.Plugins.Models
{
    public class TagReadOnly
    {
        public TagReadOnly(Tag tag)
        {
            Name = tag.Name;
            DisplayName = tag.DisplayName;
            Created = tag.Created;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public DateTime Created { get; private set; }
    }
}
