//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Blogs.Models
{
    public class BlogInputForImport
    {
        public BlogInputForImport(string displayName, string description, DateTime created)
        {
            DisplayName = displayName;
            Description = description;
            Created = created;
        }

        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public DateTime Created { get; private set; }
    }
}
