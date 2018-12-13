//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Blogs.Models
{
    public class PostSmall
    {
        public PostSmall(Guid id, string blogName, string slug, string title)
        {
            ID = id;
            BlogName = blogName;
            Slug = slug;
            Title = title;
        }

        public Guid ID { get; private set; }
        public string BlogName { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
    }
}
