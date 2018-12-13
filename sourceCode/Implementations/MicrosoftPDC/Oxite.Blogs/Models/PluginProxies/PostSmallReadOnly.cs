//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Modules.Blogs.Models;

namespace Oxite.Plugins.Models
{
    public class PostSmallReadOnly
    {
        public PostSmallReadOnly(PostSmall postSmall)
        {
            BlogName = postSmall.BlogName;
            Title = postSmall.Title;
            Slug = postSmall.Slug;
        }

        public string BlogName { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
    }
}
