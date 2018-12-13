//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using System.Transactions;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Repositories.SqlServer
{
    public class SqlServerBlogRepository : IBlogRepository
    {
        private OxiteBlogsDataContext context;

        public SqlServerBlogRepository(OxiteBlogsDataContext context)
        {
            this.context = context;
        }

        #region IBlogRepository Members

        public Blog GetBlog(Guid siteID, Guid blogID)
        {
            return (from b in context.oxite_Blogs_Blogs
                    where b.SiteID == siteID && b.BlogID == blogID
                    select projectBlog(b)).FirstOrDefault();
        }

        public Blog GetBlog(Guid siteID, string blogName)
        {
            return (from b in context.oxite_Blogs_Blogs
                    where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0
                    select projectBlog(b)).FirstOrDefault();
        }

        public IQueryable<Blog> GetBlogs(Guid siteID)
        {
            return from b in context.oxite_Blogs_Blogs
                   where b.SiteID == siteID
                   select projectBlog(b);
        }

        public Blog Save(Blog blog)
        {
            oxite_Blogs_Blog blogToSave = null;

            if (blog.ID != Guid.Empty)
                blogToSave = context.oxite_Blogs_Blogs.FirstOrDefault(a => a.SiteID == blog.Site.ID && a.BlogID == blog.ID);

            if (blogToSave == null)
            {
                blogToSave = new oxite_Blogs_Blog();

                blogToSave.SiteID = blog.Site.ID;
                blogToSave.BlogID = blog.ID != Guid.Empty ? blog.ID : Guid.NewGuid();
                blogToSave.CreatedDate = blogToSave.ModifiedDate = DateTime.UtcNow;

                context.oxite_Blogs_Blogs.InsertOnSubmit(blogToSave);
            }
            else
                blogToSave.ModifiedDate = DateTime.UtcNow;

            blogToSave.CommentingDisabled = blog.CommentingDisabled;
            blogToSave.BlogName = blog.Name;
            blogToSave.Description = blog.Description ?? "";
            blogToSave.DisplayName = blog.DisplayName ?? "";

            context.SubmitChanges();

            return GetBlog(blogToSave.SiteID, blogToSave.BlogID);
        }

        public bool Remove(Guid siteID, string blogName)
        {
            bool removedBlog = false;

            using (TransactionScope transaction = new TransactionScope())
            {
                oxite_Blogs_Blog foundBlog = context.oxite_Blogs_Blogs.FirstOrDefault(b => b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0);

                if (foundBlog != null)
                {
                    context.oxite_Blogs_Blogs.DeleteOnSubmit(foundBlog);

                    context.SubmitChanges();

                    removedBlog = true;
                }

                transaction.Complete();
            }

            return removedBlog;
        }

        #endregion

        #region Private Methods

        private static Blog projectBlog(oxite_Blogs_Blog blog)
        {
            return new Blog(blog.SiteID, blog.CommentingDisabled, blog.CreatedDate, blog.Description, blog.DisplayName, blog.BlogID, blog.ModifiedDate, blog.BlogName);
        }

        #endregion
    }
}
