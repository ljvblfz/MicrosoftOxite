//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Services;

namespace Oxite.Modules.Blogs.Services
{
    public interface IBlogService
    {
        bool GetBlogExists(string blogName);
        Blog GetBlog(BlogAddress blogAddress);
        IPageOfItems<Blog> GetBlogs(int pageIndex, int pageSize);
        IEnumerable<Blog> FindBlogs(BlogSearchCriteria criteria);
        ModelResult<Blog> AddBlog(BlogInput blogInput);
        ModelResult<Blog> EditBlog(BlogAddress blogAddress, BlogInput blogInput);
        ModelResult<Blog> EditBlog(BlogAddress blogAddress, BlogInputForImport blogInput);
        void RemoveBlog(BlogAddress blogAddress);
    }
}
