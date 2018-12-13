//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IBlogRepository
    {
        Blog GetBlog(string blogName);
        IPageOfItems<Blog> GetBlogs(PagingInfo pagingInfo);
        Blog Save(Blog blog);
        bool Remove(Blog blog);
    }
}
