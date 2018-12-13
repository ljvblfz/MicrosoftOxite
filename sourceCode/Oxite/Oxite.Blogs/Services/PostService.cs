//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Models.Extensions;
using Oxite.Modules.Blogs.Extensions;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Models.Extensions;
using Oxite.Modules.Blogs.Repositories;
using Oxite.Modules.Comments.Extensions;
using Oxite.Modules.Tags.Extensions;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Services;
using Oxite.Plugins.Extensions;
using Oxite.Plugins.Models;
using Oxite.Validation;
using Oxite.Modules.Comments.Services;

namespace Oxite.Modules.Blogs.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        private readonly ITrackbackOutboundRepository trackbackOutboundRepository;
        private readonly UrlHelper urlHelper;
        private readonly IRegularExpressions expressions;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly ITagService tagService;
        private readonly ICommentService commentService;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public PostService(IPostRepository repository, ITrackbackOutboundRepository trackbackOutboundRepository, UrlHelper urlHelper, IRegularExpressions expressions, IValidationService validator, IPluginEngine pluginEngine, ITagService tagService, ICommentService commentService, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.trackbackOutboundRepository = trackbackOutboundRepository;
            this.urlHelper = urlHelper;
            this.expressions = expressions;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.tagService = tagService;
            this.commentService = commentService;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IPostService Members

        public IPageOfItems<Post> GetPosts(PagingInfo pagingInfo)
        {
            bool includeDrafts = context.RequestDataFormat == RequestDataFormat.Web && context.User.IsAuthenticated && context.User.IsInRole("Admin");
            IPageOfItems<Post> posts =
                cache.GetItems<IPageOfItems<Post>, Post>(
                    string.Format("GetPosts-IncludeDrafts:{0}", includeDrafts),
                    pagingInfo.ToCachePartition(),
                    () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPosts(pagingInfo, includeDrafts).FillTags(tagService).FillComments(commentService)),
                    p => p.GetDependencies()
                    );

            if (!includeDrafts)
                posts = posts.Since(p => p.Published.Value, context.HttpContext.Request.IfModifiedSince());

            return posts;
        }

        public IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, Tag tag)
        {
            IPageOfItems<Post> posts =
                cache.GetItems<IPageOfItems<Post>, Post>(
                    string.Format("GetPosts-{0}", tag.GetCacheItemKey()),
                    pagingInfo.ToCachePartition(),
                    () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPosts(pagingInfo, tag).FillTags(tagService).FillComments(commentService)),
                    p => p.GetDependencies()
                    );

            if (context.RequestDataFormat.IsFeed())
                posts = posts.Since(p => p.Published.Value, context.HttpContext.Request.IfModifiedSince());

            return posts;
        }

        public IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, Blog blog)
        {
            IPageOfItems<Post> posts =
                cache.GetItems<IPageOfItems<Post>, Post>(
                    string.Format("GetPosts-{0}", blog.GetCacheItemKey()),
                    pagingInfo.ToCachePartition(),
                    () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPosts(pagingInfo, blog).FillTags(tagService).FillComments(commentService)),
                    p => p.GetDependencies()
                    );

            if (context.RequestDataFormat.IsFeed())
                posts = posts.Since(p => p.Published.Value, context.HttpContext.Request.IfModifiedSince());

            return posts;
        }

        public IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, ArchiveData archive)
        {
            return cache.GetItems<IPageOfItems<Post>, Post>(
                string.Format("GetPosts-Year:{0},Month:{1},Day:{2}", archive.Year, archive.Month, archive.Day),
                pagingInfo.ToCachePartition(),
                () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPostsByArchive(pagingInfo, archive.Year, archive.Month, archive.Day).FillTags(tagService).FillComments(commentService)),
                p => p.GetDependencies()
                );
        }

        public IPageOfItems<Post> GetPostsWithDrafts(PagingInfo pagingInfo)
        {
            return cache.GetItems<IPageOfItems<Post>, Post>(
                "GetPostsWithDrafts",
                pagingInfo.ToCachePartition(),
                () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPosts(pagingInfo, true).FillTags(tagService).FillComments(commentService)),
                p => p.GetDependencies()
                );
        }

        public IPageOfItems<Post> GetPostsWithDrafts(PagingInfo pagingInfo, Blog blog)
        {
            return cache.GetItems<IPageOfItems<Post>, Post>(
                string.Format("GetPostsWithDrafts-{0}", blog.GetCacheItemKey()),
                pagingInfo.ToCachePartition(),
                () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPostsWithDrafts(pagingInfo, blog).FillTags(tagService).FillComments(commentService)),
                p => p.GetDependencies()
                );
        }

        public IPageOfItems<Post> GetPostsByFileType(PagingInfo pagingInfo, string typeName)
        {
            return cache.GetItems<IPageOfItems<Post>, Post>(
                string.Format("GetPostsWithDrafts-FileType:{0}", typeName),
                pagingInfo.ToCachePartition(),
                () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPostsByFiles(pagingInfo, getFiles(typeName)).FillTags(tagService).FillComments(commentService)),
                p => p.GetDependencies()
                );
        }

        public IEnumerable<Post> GetPosts(DateRangeAddress dateRangeAddress)
        {
            return cache.GetItems<IEnumerable<Post>, Post>(
                string.Format("GetPostsWithDrafts-StartDate:{0},EndDate:{1}", dateRangeAddress.StartDate, dateRangeAddress.EndDate),
                () => pluginEngine.ProcessDisplayOfPosts(context, () => repository.GetPosts(dateRangeAddress).FillTags(tagService)),
                p => p.GetDependencies()
                );
        }

        //TODO: (erikpo) Need to change the query to return back data about the posts so they can be added as cache dependencies
        public IEnumerable<DateTime> GetPostDateGroups()
        {
            return cache.GetItems<IEnumerable<DateTime>, DateTime>(
                "GetPostDateGroups",
                () => repository.GetPostDateGroups(),
                null
                );
        }

        public Post GetPost(string blogName, string postSlug)
        {
            return cache.GetItem<Post>(
                string.Format("GetPost-Blog:{0},Post:{1}", blogName, postSlug),
                () => pluginEngine.ProcessDisplayOfPost(context, () => repository.GetPost(blogName, postSlug).FillTags(tagService).FillComments(commentService)),
                p => p.GetDependencies()
                );
        }

        public Post GetRandomPost()
        {
            //TODO: (erikpo) Add caching

            return repository.GetRandomPost().FillTags(tagService);
        }

        public ValidationStateDictionary ValidatePostInput(PostInput postInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(PostInput), validator.Validate(postInput));

            return validationState;
        }

        private void validatePost(Post newPost, ValidationStateDictionary validationState)
        {
            validatePost(newPost, newPost, validationState);
        }

        private void validatePost(Post newPost, Post originalPost, ValidationStateDictionary validationState)
        {
            ValidationState state = new ValidationState();
            Post foundPost;

            validationState.Add(typeof(Post), state);

            foundPost  = repository.GetPost(newPost.Blog.Name, newPost.Slug);

            if (foundPost != null && (newPost.Blog.ID != originalPost.Blog.ID || newPost.Slug != originalPost.Slug))
                state.Errors.Add("Post.SlugNotUnique", newPost.Slug, "A post already exists with the supplied blog and slug");
        }

        private ModelResult<Post> addPost<T>(T postInput, Func<Post> generatePost)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(T), validator.Validate(postInput));

            if (!validationState.IsValid) return new ModelResult<Post>(validationState);

            Post post;

            using (TransactionScope transaction = new TransactionScope())
            {
                post = generatePost();

                validatePost(post, validationState);

                if (!validationState.IsValid) return new ModelResult<Post>(validationState);

                post = repository.Save(post);

                //TODO: (eripo) Save a search result for this post

                invalidateCachedPostDependencies(post);

                transaction.Complete();
            }

            return new ModelResult<Post>(post, validationState);
        }

        private void addCreatorSubscription(Post post)
        {
            if (context.Site.AuthorAutoSubscribe)
                repository.AddSubscription(post, post.Creator.ID);
        }

        public ModelResult<Post> AddPost(PostInput postInput, EntityState postState)
        {
            postInput = pluginEngine.Process<PluginPostInput>("ProcessInputOfPost", new PluginPostInput(postInput)).ToPostInput();
            postInput = pluginEngine.Process<PluginPostInput>("ProcessInputOfPostOnAdd", new PluginPostInput(postInput)).ToPostInput();

            ModelResult<Post> results = addPost(postInput, () => postInput.ToPost(postState, context.User.Cast<User>(), t => expressions.Clean("TagReplace", t)));

            if (results.IsValid)
            {
                Post newPost = results.Item;
                string postUrl = urlHelper.AbsolutePath(urlHelper.Post(newPost));

                //TODO: (erikpo) Move this into a module
                addCreatorSubscription(newPost);

                //TODO: (erikpo) Move this into a module
                if (newPost.State == EntityState.Normal && newPost.Published.HasValue)
                {
                    IEnumerable<TrackbackOutbound> trackbacksToAdd = extractTrackbacks(newPost, postUrl, newPost.Blog.DisplayName);
                    IEnumerable<TrackbackOutbound> unsentTrackbacks = trackbackOutboundRepository.GetUnsent(newPost.ID);
                    IEnumerable<TrackbackOutbound> trackbacksToRemove = trackbacksToAdd.Where(tb => !unsentTrackbacks.Contains(tb) && !tb.Sent.HasValue);

                    trackbackOutboundRepository.Remove(trackbacksToRemove);
                    trackbackOutboundRepository.Save(trackbacksToAdd);
                }
                else
                {
                    //TODO: (erikpo) Remove all outbound trackbacks
                }

                pluginEngine.ExecuteAll("PostSaved", new { context, post = new PostReadOnly(newPost, postUrl) });
                pluginEngine.ExecuteAll("PostAdded", new { context, post = new PostReadOnly(newPost, postUrl) });

                if (newPost.State == EntityState.Normal && newPost.Published.HasValue && newPost.Published.Value <= DateTime.UtcNow)
                    pluginEngine.ExecuteAll("PostPublished", new { context, post = new PostReadOnly(newPost, postUrl) });
            }

            return results;
        }

        public ModelResult<Post> AddPost(Blog blog, PostInputForImport postInput)
        {
            ModelResult<Post> results = addPost(postInput, () => postInput.ToPost(blog));

            if (results.IsValid)
            {
                Post newPost = results.Item;

                pluginEngine.ExecuteAll("PostAddedFromImport", new { context, post = new PostReadOnly(newPost, urlHelper.AbsolutePath(urlHelper.Post(newPost))) });

                //TODO: (erikpo) Move this into a module
                addCreatorSubscription(newPost);
            }

            return results;
        }

        public ModelResult<Post> EditPost(Post post, PostInput postInput, EntityState postState)
        {
            postInput = pluginEngine.Process<PluginPostInput>("ProcessInputOfPost", new PluginPostInput(postInput)).ToPostInput();
            postInput = pluginEngine.Process<PluginPostInput>("ProcessInputOfPostOnEdit", new PluginPostInput(postInput)).ToPostInput();

            ValidationStateDictionary validationState = ValidatePostInput(postInput);

            if (!validationState.IsValid) return new ModelResult<Post>(validationState);

            Post newPost;
            Post originalPost = post;
            bool isPublished;

            using (TransactionScope transaction = new TransactionScope())
            {
                isPublished = originalPost.State == EntityState.Normal && originalPost.Published.HasValue && originalPost.Published.Value <= DateTime.UtcNow;
                newPost = originalPost.Apply(postInput, postState, context.User.Cast<User>(), t => expressions.Clean("TagReplace", t));

                validatePost(newPost, originalPost, validationState);

                if (!validationState.IsValid) return new ModelResult<Post>(validationState);

                newPost = repository.Save(newPost);

                invalidateCachedPostForEdit(newPost, originalPost);

                transaction.Complete();
            }

            string postUrl = urlHelper.AbsolutePath(urlHelper.Post(newPost));

            pluginEngine.ExecuteAll("PostSaved", new { context, post = new PostReadOnly(newPost, postUrl) });
            pluginEngine.ExecuteAll("PostEdited", new { context, post = new PostReadOnly(newPost, postUrl), postOriginal = new PostReadOnly(originalPost, urlHelper.AbsolutePath(urlHelper.Post(originalPost))) });

            bool isNowPublished = newPost.State == EntityState.Normal && newPost.Published.HasValue && newPost.Published.Value <= DateTime.UtcNow;
            if (!isPublished && isNowPublished)
                pluginEngine.ExecuteAll("PostPublished", new { context, post = new PostReadOnly(newPost, postUrl) });
            else if (isPublished && !isNowPublished)
                pluginEngine.ExecuteAll("PostUnpublished", new { context, post = new PostReadOnly(newPost, postUrl) });

            return new ModelResult<Post>(newPost, validationState);
        }

        public void Remove(Post post)
        {
            if (post == null) throw new ArgumentNullException("post");

            using (TransactionScope transaction = new TransactionScope())
            {
                if (repository.RemovePost(post))
                {
                    invalidateCachedPostForRemove(post);

                    transaction.Complete();
                }
            }

            pluginEngine.ExecuteAll("PostRemoved", new { context, post = new PostReadOnly(post, urlHelper.AbsolutePath(urlHelper.Post(post))) });

            return;
        }

        public void RemoveAll(Blog blog)
        {
            repository.RemoveAllPosts(blog);
        }

        //TODO: (erikpo) Need to change the query to return back data about the posts so they can be added as cache dependencies
        public IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives()
        {
            return cache.GetItems<IEnumerable<KeyValuePair<ArchiveData, int>>, KeyValuePair<ArchiveData, int>>(
                "GetArchives",
                () => repository.GetArchives(),
                null
                );
        }

        //TODO: (erikpo) Need to change the query to return back data about the posts so they can be added as cache dependencies
        public IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives(Blog blog)
        {
            return cache.GetItems<IEnumerable<KeyValuePair<ArchiveData, int>>, KeyValuePair<ArchiveData, int>>(
                string.Format("GetArchives-{0}", blog.GetCacheItemKey()),
                () => repository.GetArchives(blog),
                null
                );
        }

        public ValidationStateDictionary AddTrackback(Post post, Trackback trackback)
        {
            repository.SaveTrackback(post, trackback);

            return null;
        }

        public ValidationStateDictionary EditTrackback(Trackback trackback)
        {
            //post.Trackbacks.Add(trackback);

            //repository.Save(post);

            return null;
        }

        public Post GetPost(MetaWeblogPostAddress postAddress)
        {
            return cache.GetItem<Post>(
                string.Format("GetPost-Post:{0:N}", postAddress.PostID),
                () => pluginEngine.ProcessDisplayOfPost(context, () => repository.GetPost(postAddress.PostID).FillTags(tagService)),
                p => p.GetDependencies()
                );
        }

        public void RemovePost(MetaWeblogPostAddress postAddress)
        {
            if (postAddress == null) throw new ArgumentNullException("postAddress");
            if (postAddress.PostID == Guid.Empty) throw new ArgumentException("PostID must be set");

            using (TransactionScope transaction = new TransactionScope())
            {
                Post post = repository.GetPost(postAddress.PostID);

                if (post != null)
                {
                    if (repository.RemovePost(post))
                    {
                        invalidateCachedPostForRemove(post);

                        transaction.Complete();

                        pluginEngine.ExecuteAll("PostRemoved", new { context, post = new PostReadOnly(post, urlHelper.AbsolutePath(urlHelper.Post(post))) });

                        return;
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        private void invalidateCachedPostDependencies(Post post)
        {
            cache.InvalidateItem(post.Blog);

            post.Tags.ToList().ForEach(t => cache.InvalidateItem(t));
        }

        private void invalidateCachedPostForEdit(Post newPost, Post originalPost)
        {
            // if the blog has changed on the post then invalidate the original post are and the new post blog
            if (originalPost.Blog.ID != newPost.Blog.ID)
            {
                cache.InvalidateItem(originalPost.Blog);
                cache.InvalidateItem(newPost.Blog);
            }

            // invalidate any tags that have changed since the edit
            originalPost.Tags.Except(newPost.Tags).Union(newPost.Tags.Except(originalPost.Tags)).ToList().ForEach(t => cache.InvalidateItem(t));

            cache.InvalidateItem(newPost);
        }

        private void invalidateCachedPostForRemove(Post post)
        {
            invalidateCachedPostDependencies(post);

            cache.InvalidateItem(post);
        }

        private IEnumerable<Guid> getFiles(string typeName)
        {
            //TODO: (erikpo) Call out to the file service to get all the files (just the FileID field) of a certain type

            return Enumerable.Empty<Guid>();
        }

        private static IEnumerable<TrackbackOutbound> extractTrackbacks(Post post, string postUrl, string postBlogTitle)
        {
            //INFO: (erikpo) Trackback spec: http://www.sixapart.com/pronet/docs/trackback_spec
            Regex r =
                new Regex(
                    @"(?<HTML><a[^>]*href\s*=\s*[\""\']?(?<HRef>[^""'>\s]*)[\""\']?[^>]*>(?<Title>[^<]+|.*?)?</a>)",
                    RegexOptions.IgnoreCase | RegexOptions.Compiled
                    );
            MatchCollection m = r.Matches(post.Body);
            List<TrackbackOutbound> trackbacks = Enumerable.Empty<TrackbackOutbound>().ToList();

            if (m.Count > 0)
            {
                //TODO: (erikpo) Once the plugin model is done, get this from the plugin
                const int retryCount = 28;

                trackbacks = new List<TrackbackOutbound>(m.Count);

                foreach (Match match in m)
                {
                    trackbacks.Add(
                        new TrackbackOutbound
                        {
                            TargetUrl = match.Groups["HRef"].Value,
                            PostID = post.ID,
                            PostTitle = post.Title,
                            PostBody = post.GetBodyShort(),
                            PostBlogTitle = postBlogTitle,
                            PostUrl = postUrl,
                            RemainingRetryCount = retryCount
                        }
                        );
                }
            }

            return trackbacks;
        }

        #endregion
    }
}
