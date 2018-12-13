//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IBlogRepository
    {
        Blog GetBlog(Guid siteID, Guid blogID);
        Blog GetBlog(Guid siteID, string blogName);
        IQueryable<Blog> GetBlogs(Guid siteID);
        Blog Save(Blog blog);
        bool Remove(Guid siteID, string blogName);
    }
}
