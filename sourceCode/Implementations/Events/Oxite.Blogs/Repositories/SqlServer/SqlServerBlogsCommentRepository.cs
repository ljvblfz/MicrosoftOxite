//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Modules.Blogs.Models;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Blogs.Repositories.SqlServer
{
    public class SqlServerBlogsCommentRepository : IBlogsCommentRepository
    {
        private readonly OxiteBlogsDataContext context;

        public SqlServerBlogsCommentRepository(OxiteBlogsDataContext context)
        {
            this.context = context;
        }

        public PostCommentShell GetComment(Guid siteID, string blogName, string postSlug, string commentSlug)
        {
            return (
                from pcr in context.oxite_Blogs_PostCommentRelationships
                join p in context.oxite_Blogs_Posts on pcr.PostID equals p.PostID
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0 && p.Slug == postSlug && pcr.Slug == commentSlug
                select new PostCommentShell(new PostSmall(p.PostID, b.BlogName, p.Slug, p.Title), pcr.CommentID, pcr.Slug)
                ).FirstOrDefault();
        }

        public IQueryable<PostCommentShell> GetComments(Guid siteID, bool includePending, bool sortDescending)
        {
            return null;
            //var query =
            //    from b in context.oxite_Blogs
            //    join p in context.oxite_Posts on b.BlogID equals p.BlogID
            //    join c in context.oxite_Comments on p.PostID equals c.PostID
            //    where b.SiteID == siteID && p.PublishedDate != null && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal
            //    select c;

            //query = includePending
            //    ? query.Where(c => c.State == (byte)EntityState.Normal || c.State == (byte)EntityState.PendingApproval)
            //    : query.Where(c => c.State == (byte)EntityState.Normal);

            //query = sortDescending
            //    ? query.OrderByDescending(c => c.CreatedDate)
            //    : query.OrderBy(c => c.CreatedDate);

            //return projectComments(query);
        }

        public IQueryable<PostCommentShell> GetCommentsByBlog(Guid blogID)
        {
            return null;
            //IQueryable<oxite_Comment> commentsByBlog =
            //    from c in context.oxite_Comments
            //    join p in context.oxite_Posts on c.PostID equals p.BlogID
            //    where p.BlogID == blogID && c.State == (byte)EntityState.Normal && p.PublishedDate != null && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal
            //    orderby c.CreatedDate descending
            //    select c;

            //return projectComments(commentsByBlog);
        }

        public IQueryable<PostCommentShell> GetCommentsByTag(Guid siteID, Guid tagID)
        {
            return
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                join ptr in context.oxite_Blogs_PostTagRelationships on p.PostID equals ptr.PostID
                join pcr in context.oxite_Blogs_PostCommentRelationships on p.PostID equals pcr.PostID
                where b.SiteID == siteID && ptr.TagID == tagID && p.PublishedDate != null && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal
                select new PostCommentShell(new PostSmall(p.PostID, b.BlogName, p.Slug, p.Title), pcr.CommentID, pcr.Slug);
        }

        public IQueryable<PostCommentShell> GetCommentsByPost(Guid postID, bool includeUnapproved)
        {
            return null;
            //IQueryable<oxite_Comment> commentsByPost =
            //    from c in context.oxite_Comments
            //    where c.PostID == postID
            //    orderby c.CreatedDate ascending
            //    select c;

            //if (includeUnapproved)
            //    commentsByPost = commentsByPost.Where(c => c.State == (byte)EntityState.Normal || c.State == (byte)EntityState.PendingApproval);
            //else
            //    commentsByPost = commentsByPost.Where(c => c.State == (byte)EntityState.Normal);

            //return projectComments(commentsByPost);
        }

        public PostComment Save(PostComment comment, Guid siteID, string blogName, string postSlug)
        {
            oxite_Comment commentToSave = null;

            if (comment.ID != Guid.Empty)
                commentToSave = context.oxite_Comments.FirstOrDefault(c => c.CommentID == comment.ID);

            if (commentToSave == null)
            {
                commentToSave = new oxite_Comment();

                commentToSave.CommentID = comment.ID != Guid.Empty ? comment.ID : Guid.NewGuid();
                commentToSave.CreatedDate = commentToSave.ModifiedDate = DateTime.UtcNow;

                context.oxite_Comments.InsertOnSubmit(commentToSave);
            }
            else
                commentToSave.ModifiedDate = DateTime.UtcNow;

            oxite_Blogs_Post post = (from p in context.oxite_Blogs_Posts join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0 && string.Compare(p.Slug,  postSlug, true) == 0 select p).FirstOrDefault();
            if (post == null) throw new InvalidOperationException(string.Format("Post in blog {0} at slug {1} could not be found to add the comment to", blogName, postSlug));
            context.oxite_Blogs_PostCommentRelationships.InsertOnSubmit(
                new oxite_Blogs_PostCommentRelationship
                {
                    CommentID = commentToSave.CommentID,
                    PostID = post.PostID,
                    Slug = comment.Slug
                }
                );

            commentToSave.ParentCommentID = comment.Parent != null && comment.Parent.ID != Guid.Empty ? comment.Parent.ID : commentToSave.CommentID;
            commentToSave.Body = comment.Body;
            commentToSave.CreatorIP = comment.CreatorIP;
            commentToSave.State = (byte)comment.State;
            commentToSave.UserAgent = comment.CreatorUserAgent;
            commentToSave.oxite_Language = context.oxite_Languages.Where(l => l.LanguageName == comment.Language.Name).FirstOrDefault();

            if (comment.CreatorUserID != Guid.Empty)
                commentToSave.CreatorUserID = comment.CreatorUserID;
            else
            {
                oxite_User anonymousUser = context.oxite_Users.FirstOrDefault(u => u.Username == "Anonymous");
                if (anonymousUser == null) throw new InvalidOperationException("Could not find anonymous user");
                commentToSave.CreatorUserID = anonymousUser.UserID;

                commentToSave.CreatorName = comment.CreatorName;
                commentToSave.CreatorEmail = comment.CreatorEmail;
                commentToSave.CreatorHashedEmail = comment.CreatorEmailHash;
                commentToSave.CreatorUrl = comment.CreatorUrl;
            }

            context.SubmitChanges();

            return (
                from c in context.oxite_Comments
                join pcr in context.oxite_Blogs_PostCommentRelationships on c.CommentID equals pcr.CommentID
                join p in context.oxite_Blogs_Posts on pcr.PostID equals p.PostID
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                join u in context.oxite_Users on c.CreatorUserID equals u.UserID
                where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0 && string.Compare(p.Slug, postSlug, true) == 0 && string.Compare(pcr.Slug, comment.Slug, true) == 0
                select projectComment(c, pcr, p, b, u)
                ).FirstOrDefault();
        }

        private PostComment projectComment(oxite_Comment comment, oxite_Blogs_PostCommentRelationship pcr, oxite_Blogs_Post p, oxite_Blogs_Blog b, oxite_User user)
        {
            PostCommentSmall parent = comment.ParentCommentID != comment.CommentID ? getParentComment(comment.ParentCommentID) : null;
            Language language = new Language(comment.oxite_Language.LanguageID)
            {
                DisplayName = comment.oxite_Language.LanguageDisplayName,
                Name = comment.oxite_Language.LanguageName
            };

            if (user.Username != "Anonymous")
                return new PostComment(comment.Body, comment.CreatedDate, getUserAuthenticated(comment, user), comment.CreatorIP, comment.UserAgent, comment.CommentID, language, comment.ModifiedDate, parent, new PostSmall(p.PostID, b.BlogName, p.Slug, p.Title), pcr.Slug, (EntityState)comment.State);
            else
                return new PostComment(comment.Body, comment.CreatedDate, getUserAnonymous(comment, user), comment.CreatorIP, comment.UserAgent, comment.CommentID, language, comment.ModifiedDate, parent, new PostSmall(p.PostID, b.BlogName, p.Slug, p.Title), pcr.Slug, (EntityState)comment.State);
        }

        private PostCommentSmall getParentComment(Guid commentID)
        {
            return (
                from c in context.oxite_Comments
                join pcr in context.oxite_Blogs_PostCommentRelationships on c.CommentID equals pcr.CommentID
                join u in context.oxite_Users on c.CreatorUserID equals u.UserID
                where c.State != (byte)EntityState.Removed && c.CommentID == commentID
                select projectPostCommentSmall(c, pcr, u)
                ).FirstOrDefault();
        }

        private static UserAuthenticated getUserAuthenticated(oxite_Comment comment, oxite_User user)
        {
            return new UserAuthenticated(user.UserID, user.Username, user.DisplayName, user.Email, user.HashedEmail, (EntityState)user.Status);
        }

        private static UserAnonymous getUserAnonymous(oxite_Comment comment, oxite_User user)
        {
            return new UserAnonymous(comment.CreatorName, comment.CreatorEmail, comment.CreatorHashedEmail, comment.CreatorUrl);
        }

        private static PostCommentSmall projectPostCommentSmall(oxite_Comment comment, oxite_Blogs_PostCommentRelationship pcr, oxite_User user)
        {
            if (user.Username != "Anonymous")
                return new PostCommentSmall(new CommentSmall(comment.CommentID, comment.CreatedDate, getUserAuthenticated(comment, user)), pcr.Slug);
            else
                return new PostCommentSmall(new CommentSmall(comment.CommentID, comment.CreatedDate, getUserAnonymous(comment, user)), pcr.Slug);
        }
    }
}
