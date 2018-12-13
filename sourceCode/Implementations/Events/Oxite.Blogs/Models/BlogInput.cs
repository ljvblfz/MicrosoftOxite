//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class BlogInput
    {
        public BlogInput(string name, string displayName, string description, bool commentingDisabled)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            CommentingDisabled = commentingDisabled;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public bool CommentingDisabled { get; private set; }

        public Blog ToBlog(Guid siteID)
        {
            return new Blog(siteID, CommentingDisabled, Description, DisplayName, Name);
        }
    }
}
