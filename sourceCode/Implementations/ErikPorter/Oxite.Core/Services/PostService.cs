//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Repositories;
using Oxite.Validation;

namespace Oxite.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        private readonly ILocalizationRepository localizationRepository;
        private readonly Site site;
        private readonly IValidationService validator;
        private readonly IOxiteEvents events;

        public PostService(IPostRepository repository, ILocalizationRepository localizationRepository, Site site, IValidationService validator, IOxiteEvents events)
        {
            this.repository = repository;
            this.localizationRepository = localizationRepository;
            this.site = site;
            this.validator = validator;
            this.events = events;
        }

        #region IPostService Members

        public IPageOfList<Post> GetPosts(int pageIndex, int pageSize, DateTime? sinceDate)
        {
            return sinceDate.HasValue ? repository.GetPosts(sinceDate.Value.ToUniversalTime()).GetPage(pageIndex, pageSize) : repository.GetPosts(false).GetPage(pageIndex, pageSize);
        }

        public IPageOfList<Post> GetPosts(int pageIndex, int pageSize, Tag tag, DateTime? sinceDate)
        {
            return sinceDate.HasValue ? repository.GetPosts(tag, sinceDate.Value.ToUniversalTime()).GetPage(pageIndex, pageSize) : repository.GetPosts(tag).GetPage(pageIndex, pageSize);
        }

        public IPageOfList<Post> GetPosts(int pageIndex, int pageSize, Area area, DateTime? sinceDate)
        {
            return sinceDate.HasValue ? repository.GetPosts(area, sinceDate.Value.ToUniversalTime()).GetPage(pageIndex, pageSize) :  repository.GetPosts(area).GetPage(pageIndex, pageSize);
        }

        public IPageOfList<Post> GetPosts(int pageIndex, int pageSize, ArchiveData archive)
        {
            return repository.GetPosts(archive).GetPage(pageIndex, pageSize);
        }

        public IPageOfList<Post> GetPosts(int pageIndex, int pageSize, SearchCriteria criteria, DateTime? sinceDate)
        {
            return sinceDate.HasValue ? repository.GetPosts(criteria, sinceDate.Value.ToUniversalTime()).GetPage(pageIndex, pageSize) :  repository.GetPosts(criteria).GetPage(pageIndex, pageSize);
        }

        public IPageOfList<Post> GetPostsWithDrafts(int pageIndex, int pageSize)
        {
            return repository.GetPosts(true).GetPage(pageIndex, pageSize);
        }

        public IList<Post> GetPosts(DateTime startDate, DateTime endDate)
        {
            return repository.GetPosts(startDate, endDate);
        }

        public IList<DateTime> GetPostDateGroups()
        {
            return repository.GetPostDateGroups();
        }

        public Post GetPost(PostAddress postAddress)
        {
            return repository.GetPost(postAddress.AreaName, postAddress.Slug);
        }

        public Post GetPost(Guid id)
        {
            return repository.GetPost(id);
        }

        public void ValidatePost(Post post, out ValidationStateDictionary validationState)
        {
            validationState = new ValidationStateDictionary();

            validationState.Add(typeof(Post), validator.Validate(post));
        }

        public void AddPost(Post post, User creator, bool fireEvents, out ValidationStateDictionary validationState, out Post newPost)
        {
            validationState = new ValidationStateDictionary();

            post.Creator = creator;

            validationState.Add(typeof(Post), validator.Validate(post));

            if (!validationState.IsValid)
            {
                newPost = null;

                return;
            }

            if (fireEvents)
                events.FireEvent("PostSaving", post);

            repository.Save(post);

            newPost = repository.GetPost(post.ID);

            if (site.AuthorAutoSubscribe && !repository.GetSubscriptionExists(newPost, creator))
                repository.AddSubscription(newPost, creator);

            if (fireEvents)
                events.FireEvent("PostSaved", newPost);
        }

        //todo: (nheskew) need to consolidate
        public ValidationStateDictionary AddPost(Post post)
        {
            events.FireEvent("PostSaving", post);

            repository.Save(post);

            events.FireEvent("PostSaved", post);

            return null;
        }

        public void EditPost(Post post, Post postEdits, out ValidationStateDictionary validationState)
        {
            validationState = new ValidationStateDictionary();

            postEdits.ID = post.ID;
            postEdits.Creator = post.Creator;
            postEdits.Created = post.Created;
            postEdits.State = post.State;

            validationState.Add(typeof(Post), validator.Validate(postEdits));

            if (!validationState.IsValid)
            {
                return;
            }

            events.FireEvent("PostSaving", post);

            repository.Save(postEdits);

            events.FireEvent("PostSaved", post);
        }

        //todo: (nheskew) need to consolidate
        public ValidationStateDictionary EditPost(Post post)
        {
            events.FireEvent("PostSaving", post);

            repository.Save(post);

            events.FireEvent("PostSaved", post);

            return null;
        }

        public void RemovePost(Post post)
        {
            if (post != null)
                repository.Remove(post);
        }

        public void RemoveAll(Area area)
        {
            repository.RemoveAll(area);
        }

        public IList<KeyValuePair<ArchiveData, int>> GetArchives()
        {
            return repository.GetArchives().ToList();
        }

        public IList<KeyValuePair<ArchiveData, int>> GetArchives(Area area)
        {
            return repository.GetArchives(area).ToList();
        }

        public Comment GetComment(Guid commentID)
        {
            return repository.GetComment(commentID);
        }

        public IList<ParentAndChild<PostBase, Comment>> GetComments()
        {
            return repository.GetComments(false, false).ToList();
        }

        public IList<ParentAndChild<PostBase, Comment>> GetComments(Area area)
        {
            return repository.GetComments(area).ToList();
        }

        public IList<Comment> GetComments(Post post)
        {
            return repository.GetComments(post).ToList();
        }

        public IList<ParentAndChild<PostBase, Comment>> GetComments(Tag tag)
        {
            return repository.GetComments(tag).ToList();
        }

        public IPageOfList<ParentAndChild<PostBase, Comment>> GetComments(int pageIndex, int pageSize)
        {
            return GetComments(pageIndex, pageSize, false, false);
        }

        public IPageOfList<ParentAndChild<PostBase, Comment>> GetComments(int pageIndex, int pageSize, bool includePending, bool sortDescending)
        {
            return repository.GetComments(includePending, sortDescending).GetPage(pageIndex, pageSize);
        }

        public void ValidateComment(Comment comment, out ValidationStateDictionary validationState)
        {
            validationState = new ValidationStateDictionary();

            validationState.Add(typeof(Comment), validator.Validate(comment));

            if (!(comment.Creator is User))
            {
                validationState.Add(typeof(UserBase), validator.Validate(comment.Creator));

                //some rules change for an anonymous user
                if (!string.IsNullOrEmpty(comment.Creator.HashedEmail)
                    && validationState[typeof(UserBase)] != null
                    && validationState[typeof(UserBase)].Errors.Where(v => v.Name != "Email").FirstOrDefault() == null)
                {
                    validationState.Remove(typeof(UserBase));
                }
            }
        }

        public void AddComment(Area area, Post post, Comment comment, UserBase creator, bool subscribe, out ValidationStateDictionary validationState, out Comment newComment)
        {
            validationState = new ValidationStateDictionary();

            comment.Creator = creator;

            if (comment.State == EntityState.NotSet)
            {
                try
                {
                    comment.State = creator is User
                        ? EntityState.Normal
                        : (EntityState)Enum.Parse(typeof(EntityState), site.CommentStateDefault);
                }
                catch
                {
                    comment.State = EntityState.PendingApproval;
                }
            }

            if (comment.Language == null)
            {
                comment.Language = post.Creator.LanguageDefault;
            }

            validationState.Add(typeof(Comment), validator.Validate(comment));

            //validate anonymous users
            if (!(comment.Creator is User))
            {
                validationState.Add(typeof(UserBase), validator.Validate(comment.Creator));

                comment.Creator.HashedEmail = !string.IsNullOrEmpty(comment.Creator.Email) ? comment.Creator.Email.ComputeHash() : "";
            }

            //validation for subscription
            //todo: (nheskew) moooove and unhack a bit - _feels_ wrong here
            if (subscribe)
            {
                validationState.Add(typeof (PostSubscription), validator.Validate(new PostSubscription {Post = post, User = comment.Creator}));
            }

            if (!validationState.IsValid)
            {
                newComment = null;

                return;
            }

            repository.SaveComment(post, comment);

            if (subscribe && !repository.GetSubscriptionExists(post, creator))
            {
                repository.AddSubscription(post, creator);
            }

            newComment = repository.GetComment(comment.ID);

            if (comment.State == EntityState.Normal)
                events.FireEvent("CommentAdded", new ParentAndChild<Post, Comment>() { Parent = post, Child = newComment });
        }

        //TODO: (erikpo) This is lame and should be removed once we have "events" this can be avoided
        public void AddCommentWithoutMessages(Area area, Post post, Comment comment, UserBase creator, bool subscribe, out ValidationStateDictionary validationState, out Comment newComment)
        {
            validationState = new ValidationStateDictionary();

            comment.Creator = creator;

            if (comment.State == EntityState.NotSet)
            {
                try
                {
                    comment.State = creator is User
                        ? EntityState.Normal
                        : (EntityState)Enum.Parse(typeof(EntityState), site.CommentStateDefault);
                }
                catch
                {
                    comment.State = EntityState.PendingApproval;
                }
            }

            if (comment.Language == null)
            {
                comment.Language = post.Creator.LanguageDefault;
            }

            validationState.Add(typeof(Comment), validator.Validate(comment));

            //validate anonymous users
            if (!(comment.Creator is User))
            {
                validationState.Add(typeof(UserBase), validator.Validate(comment.Creator));

                comment.Creator.HashedEmail = !string.IsNullOrEmpty(comment.Creator.Email) ? comment.Creator.Email.ComputeHash() : "";
            }

            //validation for subscription
            //todo: (nheskew) moooove and unhack a bit - _feels_ wrong here
            if (subscribe)
            {
                validationState.Add(typeof(PostSubscription), validator.Validate(new PostSubscription { Post = post, User = comment.Creator }));
            }

            if (!validationState.IsValid)
            {
                newComment = null;

                return;
            }

            repository.SaveComment(post, comment);

            if (subscribe && !repository.GetSubscriptionExists(post, creator))
            {
                repository.AddSubscription(post, creator);
            }

            newComment = repository.GetComment(comment.ID);
        }

        public void EditComment(Area area, Post post, Comment comment, out ValidationStateDictionary validationState)
        {
            throw new NotImplementedException();
        }

        public void RemoveComment(Post post, Guid commentID)
        {
            Comment comment = GetComment(commentID);

            if (comment != null)
            {
                comment.State = EntityState.Removed;

                repository.SaveComment(post, comment);
            }
        }

        public void ApproveComment(Post post, Guid commentID)
        {
            Comment comment = GetComment(commentID);

            if (comment != null)
            {
                comment.State = EntityState.Normal;

                repository.SaveComment(post, comment);

                events.FireEvent("CommentAdded", new ParentAndChild<Post, Comment>() { Parent = post, Child = comment });
            }
        }

        public IList<PostSubscription> GetSubscriptions(Post post)
        {
            return repository.GetSubscriptions(post);
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

        #endregion
    }
}
