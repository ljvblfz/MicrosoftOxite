//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Repositories;

namespace Oxite.Modules.Blogs.Services
{
    public class BlogsTagService : IBlogsTagService
    {
        private readonly IBlogsTagRepository repository;
        private readonly OxiteContext context;

        public BlogsTagService(IBlogsTagRepository repository, OxiteContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        #region IBlogsTagService Members

        public IEnumerable<KeyValuePair<PostTag, int>> GetTagsWithPostCount()
        {
            //TODO: (erikpo) Add caching

            return repository.GetTagsWithPostCount();
        }

        public IEnumerable<KeyValuePair<PostTag, int>> GetTagsUsedIn(Blog blog)
        {
            //TODO: (erikpo) Add caching

            return repository.GetTagsUsedInBlog(blog);
        }

        #endregion
    }
}
