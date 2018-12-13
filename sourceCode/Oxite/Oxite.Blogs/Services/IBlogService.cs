//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Services
{
    public interface IBlogService
    {
        bool GetBlogExists(string blogName);
        Blog GetBlog(string blogName);
        IPageOfItems<Blog> GetBlogs(PagingInfo pagingInfo);
        IEnumerable<Blog> FindBlogs(BlogSearchCriteria criteria);
        ModelResult<Blog> AddBlog(BlogInput blogInput);
        ModelResult<Blog> EditBlog(Blog blog, BlogInput blogInput);
        ModelResult<Blog> EditBlog(Blog blog, BlogInputForImport blogInput);
        void RemoveBlog(Blog blog);
    }
}
