//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Repositories.SqlServer
{
    public class SqlServerTagRepository : IBlogsTagRepository
    {
        private readonly OxiteBlogsDataContext context;
        private readonly Guid siteID;

        public SqlServerTagRepository(OxiteBlogsDataContext context, Site site)
        {
            this.context = context;
            this.siteID = site.ID;
        }

        #region IBlogsTagRepository Members

        public IQueryable<KeyValuePair<PostTag, int>> GetTagsWithPostCount()
        {
            return from tt in
                       (from t in context.oxite_Tags
                        join ptr in context.oxite_Blogs_PostTagRelationships on t.TagID equals ptr.TagID
                        join p in context.oxite_Blogs_Posts on ptr.PostID equals p.PostID
                        where p.State == (byte)EntityState.Normal && p.PublishedDate <= DateTime.Now.ToUniversalTime()
                        select new { Tag = t.oxite_Tag1, DisplayName = ptr.TagDisplayName, Post = p })
                   group tt by new { Tag = tt.Tag, DisplayName = tt.DisplayName } into results
                   where results.Key.Tag.TagID == results.Key.Tag.ParentTagID
                   orderby results.Key.Tag.TagName
                   select new KeyValuePair<PostTag, int>(new PostTag(results.Key.Tag.TagID, results.Key.Tag.TagName, results.Key.DisplayName, results.Key.Tag.CreatedDate), results.Count());
        }

        public IQueryable<KeyValuePair<PostTag, int>> GetTagsUsedInBlog(string blogName)
        {
            return from tt in
                       (from t in context.oxite_Tags
                        join ptr in context.oxite_Blogs_PostTagRelationships on t.TagID equals ptr.TagID
                        join p in context.oxite_Blogs_Posts on ptr.PostID equals p.PostID
                        join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                        where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0 && p.State == (byte)EntityState.Normal && p.PublishedDate <= DateTime.Now.ToUniversalTime()
                        select new { Tag = t.oxite_Tag1, Post = p })
                   group tt by tt.Tag into results
                   where results.Key.TagID == results.Key.ParentTagID
                   orderby results.Key.TagName
                   select new KeyValuePair<PostTag, int>(new PostTag(results.Key.TagID, results.Key.TagName, results.Key.TagName, results.Key.CreatedDate), results.Count());
        }

        #endregion
    }
}
