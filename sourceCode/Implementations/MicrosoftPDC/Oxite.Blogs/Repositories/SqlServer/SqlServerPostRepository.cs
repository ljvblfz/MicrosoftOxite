//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Repositories.SqlServer
{
    public class SqlServerPostRepository : IPostRepository
    {
        private readonly OxiteBlogsDataContext context;

        public SqlServerPostRepository(OxiteBlogsDataContext context)
        {
            this.context = context;
        }

        #region IPostRepository Members

        public IQueryable<Post> GetPosts(Guid siteID, bool includeDrafts)
        {
            if (includeDrafts)
                return projectPosts(getPostsBySiteQuery(siteID));

            return projectPosts(excludeNotYetPublished(getPostsBySiteQuery(siteID)));
        }

        public IQueryable<Post> GetPostsByTag(Guid siteID, Guid tagID)
        {
            return projectPosts(excludeNotYetPublished(getPostsByTagQuery(siteID, tagID)));
        }

        private IQueryable<oxite_Blogs_Post> getPostsByTagQuery(Guid siteID, Guid tagID)
        {
            return
                from p in context.oxite_Blogs_Posts
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                join ptr in context.oxite_Blogs_PostTagRelationships on p.PostID equals ptr.PostID
                where b.SiteID == siteID && ptr.TagID == tagID
                select p;
        }

        public IQueryable<Post> GetPostsByBlogWithDrafts(Guid siteID, string blogName)
        {
            return projectPosts(getPostsByBlogQuery(siteID, blogName));
        }

        public IQueryable<Post> GetPostsByBlog(Guid siteID, string blogName)
        {
            return projectPosts(excludeNotYetPublished(getPostsByBlogQuery(siteID, blogName)));
        }

        private IQueryable<oxite_Blogs_Post> getPostsByBlogQuery(Guid siteID, string blogName)
        {
            return
                from p in context.oxite_Blogs_Posts
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0
                select p;
        }

        public IQueryable<Post> GetPostsByFiles(Guid siteID, IEnumerable<Guid> files)
        {
            var query =
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                join pfr in context.oxite_Blogs_PostFileRelationships on p.PostID equals pfr.PostID
                where b.SiteID == siteID && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal && files.Contains(pfr.FileID)
                orderby p.PublishedDate
                select p;

            return projectPosts(query);
        }

        public IEnumerable<Post> GetPosts(Guid siteID, DateTime startDate, DateTime endDate)
        {
            return projectPosts(
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID && p.PublishedDate != null && p.PublishedDate >= startDate && p.PublishedDate < endDate && p.State == (byte)EntityState.Normal
                orderby p.PublishedDate
                select p
                ).ToArray();
        }

        public IEnumerable<DateTime> GetPostDateGroups(Guid siteID)
        {
            return (
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID && p.PublishedDate != null && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal
                orderby p.PublishedDate
                group p by new DateTime(p.PublishedDate.Value.Year, p.PublishedDate.Value.Month, 1)
                    into results
                    select results.Key
                ).ToArray();
        }

        public IQueryable<KeyValuePair<ArchiveData, int>> GetArchives(Guid siteID)
        {
            return
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID && p.PublishedDate != null && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal
                let month = new DateTime(p.PublishedDate.Value.Year, p.PublishedDate.Value.Month, 1)
                group p by month into months
                orderby months.Key descending
                select new KeyValuePair<ArchiveData, int>(new ArchiveData(months.Key.Year + "/" + months.Key.Month), months.Count());
        }

        public IQueryable<KeyValuePair<ArchiveData, int>> GetArchivesByBlog(Guid siteID, string blogName)
        {
            return
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0 && p.PublishedDate != null && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal
                let month = new DateTime(p.PublishedDate.Value.Year, p.PublishedDate.Value.Month, 1)
                group p by month into months
                orderby months.Key descending
                select new KeyValuePair<ArchiveData, int>(new ArchiveData(months.Key.Year + "/" + months.Key.Month), months.Count());
        }

        public Post Save(Post post)
        {
            oxite_Blogs_Post postToSave = null;

            if (post.ID != Guid.Empty)
                postToSave = context.oxite_Blogs_Posts.FirstOrDefault(p => p.PostID == post.ID);

            if (postToSave == null)
            {
                postToSave = new oxite_Blogs_Post();

                postToSave.PostID = post.ID != Guid.Empty ? post.ID : Guid.NewGuid();
                postToSave.CreatedDate = postToSave.ModifiedDate = DateTime.UtcNow;

                context.oxite_Blogs_Posts.InsertOnSubmit(postToSave);
            }
            else
                postToSave.ModifiedDate = DateTime.UtcNow;

            postToSave.Body = post.Body;
            postToSave.BodyShort = post.BodyShort;
            postToSave.PublishedDate = post.Published;
            postToSave.Slug = post.Slug;
            postToSave.State = (byte)post.State;
            postToSave.Title = post.Title;
            postToSave.CommentingDisabled = post.CommentingDisabled;

            // Tags: Use existing, create new ones if needed. Don't edit old tags
            foreach (Tag tag in post.Tags)
            {
                oxite_Tag persistenceTag = context.oxite_Tags.Where(t => string.Compare(t.TagName, tag.Name, true) == 0).FirstOrDefault();

                if (persistenceTag == null)
                {
                    Guid newTagID = Guid.NewGuid();
                    persistenceTag = new oxite_Tag { TagName = tag.Name, CreatedDate = DateTime.UtcNow, TagID = newTagID, ParentTagID = newTagID };
                    context.oxite_Tags.InsertOnSubmit(persistenceTag);
                }

                if (!context.oxite_Blogs_PostTagRelationships.Any(pt => pt.PostID == postToSave.PostID && pt.TagID == persistenceTag.TagID))
                    context.oxite_Blogs_PostTagRelationships.InsertOnSubmit(new oxite_Blogs_PostTagRelationship { PostID = postToSave.PostID, TagID = persistenceTag.TagID, TagDisplayName = tag.DisplayName ?? tag.Name });
            }

            var tagsRemoved = from t in context.oxite_Tags
                              join pt in context.oxite_Blogs_PostTagRelationships on t.TagID equals pt.TagID
                              where pt.PostID == postToSave.PostID && !post.Tags.Select(tag => tag.Name.ToLower()).Contains(t.TagName.ToLower())
                              select pt;

            context.oxite_Blogs_PostTagRelationships.DeleteAllOnSubmit(tagsRemoved);

            if (post.Blog == null || string.IsNullOrEmpty(post.Blog.Name)) throw new InvalidOperationException("No blog was specified");
            oxite_Blogs_Blog blog = context.oxite_Blogs_Blogs.FirstOrDefault(b => string.Compare(b.BlogName, post.Blog.Name, true) == 0);
            if (blog == null) throw new InvalidOperationException(string.Format("Blog {0} could not be found", post.Blog.Name));
            postToSave.BlogID = blog.BlogID;

            oxite_User user = context.oxite_Users.FirstOrDefault(u => u.Username == post.Creator.Name);
            if (user == null) throw new InvalidOperationException(string.Format("User {0} could not be found", post.Creator.Name));
            postToSave.CreatorUserID = user.UserID;

            //TODO: (erikpo) Add an item to the search index
            //postToSave.SearchBody = postToSave.Title + string.Join("", post.Tags.Select(t => t.Name + t.DisplayName).ToArray()) + postToSave.Body + user.DisplayName + user.Username;

            context.SubmitChanges();

            return GetPost(postToSave.PostID);
        }

        public bool Remove(Guid postID)
        {
            oxite_Blogs_Post foundPost = context.oxite_Blogs_Posts.FirstOrDefault(p => p.PostID == postID);
            bool removedPost = false;

            if (foundPost != null)
            {
                foundPost.State = (byte)EntityState.Removed;

                context.SubmitChanges();

                removedPost = true;
            }

            return removedPost;
        }

        public IQueryable<Post> GetPostsByArchive(Guid siteID, int year, int month, int day)
        {
            var query =
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID && p.PublishedDate != null && p.PublishedDate <= DateTime.UtcNow && p.State == (byte)EntityState.Normal && p.PublishedDate.Value.Year == year
                select p;

            if (month > 0)
                query = query.Where(p => p.PublishedDate.Value.Month == month);

            if (day > 0)
                query = query.Where(p => p.PublishedDate.Value.Day == day);

            return projectPosts(query);
        }

        public Post GetPost(Guid siteID, string blogName, string slug)
        {
            IQueryable<oxite_Blogs_Post> post =
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0 && string.Compare(p.Slug, slug, true) == 0
                select p;

            return projectPosts(post).FirstOrDefault();
        }

        public Post GetPost(Guid id)
        {
            IQueryable<oxite_Blogs_Post> post =
                from p in context.oxite_Blogs_Posts
                where p.PostID == id
                select p;

            return projectPosts(post).FirstOrDefault();
        }

        //INFO: (erikpo) Not sure if this logic should exist here or in the database (as cascade delete on the relationships)
        public void RemoveAllByBlog(Guid siteID, string blogName)
        {
            var posts =
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0
                select p;

            var postTagRelationships =
                from p in posts
                join ptr in context.oxite_Blogs_PostTagRelationships on p.PostID equals ptr.PostID
                select ptr;

            var postCommentRelationship =
                from p in posts
                join pcr in context.oxite_Blogs_PostCommentRelationships on p.PostID equals pcr.PostID
                select pcr;

            var postSubscriptionRelationships =
                from p in posts
                join psr in context.oxite_Blogs_PostSubscriptionRelationships on p.PostID equals psr.PostID
                select psr;

            var trackbacks =
                from p in posts
                join t in context.oxite_Blogs_Trackbacks on p.PostID equals t.PostID
                select t;

            context.oxite_Blogs_Trackbacks.DeleteAllOnSubmit(trackbacks);
            context.oxite_Blogs_PostSubscriptionRelationships.DeleteAllOnSubmit(postSubscriptionRelationships);
            context.oxite_Blogs_PostCommentRelationships.DeleteAllOnSubmit(postCommentRelationship);
            context.oxite_Blogs_PostTagRelationships.DeleteAllOnSubmit(postTagRelationships);
            context.oxite_Blogs_Posts.DeleteAllOnSubmit(posts);

            context.SubmitChanges();
        }

        public IEnumerable<PostSubscription> GetSubscriptions(Guid siteID, string blogName, string postSlug)
        {
            var query =
                from s in context.oxite_Subscriptions
                join psr in context.oxite_Blogs_PostSubscriptionRelationships on s.SubscriptionID equals psr.SubscriptionID
                join p in context.oxite_Blogs_Posts on psr.PostID equals p.PostID
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                join u in context.oxite_Users on s.UserID equals u.UserID
                where b.SiteID == siteID && string.Compare(b.BlogName, blogName, true) == 0 && p.Slug == postSlug
                select new { Blog = b, Post = p, User = u, Subscription = s };

            //TOOD: (erikpo) Need to figure out why this query fails if not .ToArray()'d before selecting.  LINQ to SQL isn't liking something in the select, but not sure what.
            var temp = query.ToArray();

            return temp.Select(t => new PostSubscription(
                    t.Subscription.SubscriptionID,
                    new PostSmall(t.Post.PostID, t.Blog.BlogName, t.Post.Slug, t.Post.Title),
                    t.User.Username != "Anonymous" ? !string.IsNullOrEmpty(t.User.DisplayName) ? t.User.DisplayName : t.User.Username : t.Subscription.UserName,
                    t.User.Username != "Anonymous" ? t.User.Email : t.Subscription.UserEmail
                    )
                );
        }

        private bool getSubscriptionExists(Guid siteID, PostSmall post, Guid creatorUserID)
        {
            return (
                from s in context.oxite_Subscriptions
                join psr in context.oxite_Blogs_PostSubscriptionRelationships on s.SubscriptionID equals psr.SubscriptionID
                join p in context.oxite_Blogs_Posts on psr.PostID equals p.PostID
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                where b.SiteID == siteID && string.Compare(b.BlogName, post.BlogName, true) == 0 && p.Slug == post.Slug && s.UserID == creatorUserID
                select psr
                ).Any();
        }

        private bool getSubscriptionExists(Guid siteID, PostSmall post, string creatorEmail)
        {
            Guid userID = context.oxite_Users.Single(u => u.Username == "Anonymous").UserID;

            return (
                       from s in context.oxite_Subscriptions
                       join psr in context.oxite_Blogs_PostSubscriptionRelationships on s.SubscriptionID equals psr.SubscriptionID
                       join p in context.oxite_Blogs_Posts on psr.PostID equals p.PostID
                       join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                       where b.SiteID == siteID && string.Compare(b.BlogName, post.BlogName, true) == 0 && p.Slug == post.Slug && s.UserID == userID && s.UserEmail == creatorEmail
                       select s
                   ).Any();
        }

        public void AddSubscription(Guid siteID, PostSmall post, Guid creatorUserID)
        {
            if (getSubscriptionExists(siteID, post, creatorUserID)) return;

            oxite_Subscription subscription = new oxite_Subscription { SubscriptionID = Guid.NewGuid(), UserID = creatorUserID };

            context.oxite_Subscriptions.InsertOnSubmit(subscription);
            context.oxite_Blogs_PostSubscriptionRelationships.InsertOnSubmit(new oxite_Blogs_PostSubscriptionRelationship { SubscriptionID = subscription.SubscriptionID, PostID = GetPost(siteID, post.BlogName, post.Slug).ID });

            context.SubmitChanges();
        }

        public void AddSubscription(Guid siteID, PostComment comment)
        {
            if (getSubscriptionExists(siteID, comment.Post, comment.CreatorEmail)) return;

            oxite_Subscription subscription = new oxite_Subscription { SubscriptionID = Guid.NewGuid(), UserID = context.oxite_Users.Single(u => u.Username == "Anonymous").UserID, UserName = comment.CreatorName, UserEmail = comment.CreatorEmail };

            context.oxite_Subscriptions.InsertOnSubmit(subscription);
            context.oxite_Blogs_PostSubscriptionRelationships.InsertOnSubmit(new oxite_Blogs_PostSubscriptionRelationship { SubscriptionID = subscription.SubscriptionID, PostID = GetPost(siteID, comment.Post.BlogName, comment.Post.Slug).ID });

            context.SubmitChanges();
        }

        public void SaveTrackback(Post post, Trackback trackback)
        {
            oxite_Blogs_Trackback persistenceTrackback = new oxite_Blogs_Trackback
            {
                BlogName = trackback.BlogName,
                Body = trackback.Body,
                CreatedDate = trackback.Created ?? DateTime.UtcNow,
                IsTargetInSource = trackback.IsTargetInSource,
                ModifiedDate = DateTime.UtcNow,
                PostID = post.ID,
                Source = trackback.Source,
                Title = trackback.Title,
                TrackbackID = Guid.NewGuid(),
                Url = trackback.Url
            };

            context.oxite_Blogs_Trackbacks.InsertOnSubmit(persistenceTrackback);

            context.SubmitChanges();
        }

        public PostTag Save(Post post, PostTag tag)
        {
            oxite_Tag foundTag = context.oxite_Tags.Where(t => string.Compare(t.TagName, tag.Name, true) == 0).FirstOrDefault();
            Guid tagID;

            if (foundTag != null)
                tagID = foundTag.TagID;
            else
            {
                tagID = tag.ID == Guid.Empty ? Guid.NewGuid() : tag.ID;

                context.oxite_Tags.InsertOnSubmit(
                    new oxite_Tag
                    {
                        ParentTagID = tagID,
                        TagID = tagID,
                        TagName = tag.Name,
                        CreatedDate = tag.Created == default(DateTime) ? DateTime.UtcNow : tag.Created
                    }
                    );

                context.SubmitChanges();
            }

            return (
                from ptr in context.oxite_Blogs_PostTagRelationships
                join t in context.oxite_Tags on ptr.TagID equals t.TagID
                where ptr.PostID == post.ID && ptr.TagID == tagID
                select new PostTag(t.TagID, t.TagName, ptr.TagDisplayName, t.CreatedDate)
                ).FirstOrDefault();
        }

        #endregion

        #region Private Methods

        private IQueryable<Post> projectPosts(IQueryable<oxite_Blogs_Post> posts)
        {
            return
                from p in posts
                join u in context.oxite_Users on p.CreatorUserID equals u.UserID
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                let c = getCommentsQuery(p.PostID)
                let t = getTagsQuery(p.PostID)
                let tb = getTrackbacksQuery(p.PostID)
                let f = getFilesQuery(p.PostID)
                where p.State != (byte)EntityState.Removed
                orderby p.PublishedDate descending
                select projectPost(p, u, b, c, t, tb, f);
        }

        private static Post projectPost(oxite_Blogs_Post p, oxite_User u, oxite_Blogs_Blog b, IQueryable<PostComment> c, IQueryable<oxite_Blogs_PostTagRelationship> t, IQueryable<Trackback> tb, IQueryable<File> f)
        {
            Blog blog = new Blog(b.SiteID, b.CommentingDisabled, b.CreatedDate, b.Description, b.DisplayName, b.BlogID, b.ModifiedDate, b.BlogName);

            UserAuthenticated creator =
                u != null
                ? new UserAuthenticated(u.UserID, u.Username, u.DisplayName, u.Email, u.HashedEmail, new Language(u.oxite_Language.LanguageID) { Name = u.oxite_Language.LanguageName, DisplayName = u.oxite_Language.LanguageDisplayName }, (EntityState)u.Status)
                : null;

            return new Post(blog, p.Body, p.BodyShort, p.CommentingDisabled, p.CreatedDate, creator, p.PostID, p.ModifiedDate, p.PublishedDate, p.Slug, (EntityState)p.State, t.Select(tag => new PostTag(tag.TagID, tag.TagDisplayName)).ToList(), p.Title, c.ToList(), tb.ToList(), f.ToArray());
        }

        private IQueryable<Trackback> getTrackbacksQuery(Guid postID)
        {
            return
                from tb in context.oxite_Blogs_Trackbacks
                where tb.PostID == postID
                select new Trackback
                {
                    BlogName = tb.BlogName,
                    Body = tb.Body,
                    Created = tb.CreatedDate,
                    ID = tb.TrackbackID,
                    IsTargetInSource = tb.IsTargetInSource,
                    Modified = tb.ModifiedDate,
                    Source = tb.Source,
                    Title = tb.Title,
                    Url = tb.Url
                };
        }

        private IQueryable<File> getFilesQuery(Guid postID)
        {
            return
                from pfr in context.oxite_Blogs_PostFileRelationships
                join f in context.oxite_Files on pfr.FileID equals f.FileID
                where pfr.PostID == postID
                select new File(f.FileID, f.TypeName, f.MimeType, new Uri(f.Url), f.Length);
        }

        private IQueryable<oxite_Blogs_PostTagRelationship> getTagsQuery(Guid postID)
        {
            return
                from ptr in context.oxite_Blogs_PostTagRelationships
                where ptr.PostID == postID
                orderby ptr.TagDisplayName
                select ptr;
        }

        private IQueryable<PostComment> getCommentsQuery(Guid postID)
        {
            return
                from c in context.oxite_Comments
                join pcr in context.oxite_Blogs_PostCommentRelationships on c.CommentID equals pcr.CommentID
                join p in context.oxite_Blogs_Posts on pcr.PostID equals p.PostID
                join b in context.oxite_Blogs_Blogs on p.BlogID equals b.BlogID
                where pcr.PostID == postID && c.State != (byte)EntityState.Removed
                select new PostComment(pcr.CommentID, new PostSmall(p.PostID, b.BlogName, p.Slug, p.Title), pcr.Slug);
        }

        private IQueryable<oxite_Blogs_Post> getPostsBySiteQuery(Guid siteID)
        {
            return
                from b in context.oxite_Blogs_Blogs
                join p in context.oxite_Blogs_Posts on b.BlogID equals p.BlogID
                where b.SiteID == siteID
                select p;
        }

        private static IQueryable<oxite_Blogs_Post> excludeNotYetPublished(IQueryable<oxite_Blogs_Post> query)
        {
            return query.Where(p => p.PublishedDate != null && p.PublishedDate < DateTime.UtcNow);
        }

        #endregion
    }
}
