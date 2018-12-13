//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Repositories;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Services
{
    public class BlogsTagService : IBlogsTagService
    {
        private readonly IBlogsTagRepository repository;

        public BlogsTagService(IBlogsTagRepository repository)
        {
            this.repository = repository;
        }

        #region IBlogsTagService Members

        public IEnumerable<KeyValuePair<PostTag, int>> GetTagsWithPostCount()
        {
            return repository.GetTagsWithPostCount().ToArray();
        }

        public IEnumerable<KeyValuePair<PostTag, int>> GetTagsUsedIn(BlogAddress blogAddress)
        {
            return repository.GetTagsUsedInBlog(blogAddress.BlogName).ToArray();
        }

        #endregion
    }
}
