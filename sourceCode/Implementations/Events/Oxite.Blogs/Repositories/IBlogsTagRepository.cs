//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IBlogsTagRepository
    {
        IQueryable<KeyValuePair<PostTag, int>> GetTagsWithPostCount();
        IQueryable<KeyValuePair<PostTag, int>> GetTagsUsedInBlog(string blogName);
    }
}
