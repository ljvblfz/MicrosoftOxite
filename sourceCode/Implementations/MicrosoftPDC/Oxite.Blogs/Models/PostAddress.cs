//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Blogs.Models
{
    public class PostAddress : BlogAddress
    {
        public PostAddress(string blogName, string postSlug)
            : base(blogName)
        {
            PostSlug = postSlug;
        }

        public PostAddress(BlogAddress blogAddress, string postSlug)
            : this(blogAddress.BlogName, postSlug)
        {
        }

        public string PostSlug { get; private set; }

        public BlogAddress ToBlogAddress()
        {
            return new BlogAddress(BlogName);
        }
    }
}
