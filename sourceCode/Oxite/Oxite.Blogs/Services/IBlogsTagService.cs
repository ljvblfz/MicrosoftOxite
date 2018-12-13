//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Services
{
    public interface IBlogsTagService
    {
        IEnumerable<KeyValuePair<PostTag, int>> GetTagsWithPostCount();
        IEnumerable<KeyValuePair<PostTag, int>> GetTagsUsedIn(Blog blog);
    }
}
