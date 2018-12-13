//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class BlogExtensions
    {
        public static Blog Apply(this Blog blog, BlogInput input)
        {
            return new Blog(blog.Site.ID, input.CommentingDisabled, blog.Created, input.Description, input.DisplayName, blog.ID, blog.Modified, input.Name);
        }

        public static Blog Apply(this Blog blog, BlogInputForImport input)
        {
            return new Blog(blog.Site.ID, blog.CommentingDisabled, input.Created, input.Description, input.DisplayName, blog.ID, input.Created, blog.Name);
        }

        public static BlogAddress ToBlogAddress(this Blog blog)
        {
            return new BlogAddress(blog.Name);
        }
    }
}
