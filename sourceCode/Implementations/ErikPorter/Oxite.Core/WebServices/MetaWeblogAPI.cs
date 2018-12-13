//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Services;

namespace Oxite.WebServices
{
    public class MetaWeblogAPI : IMetaWeblog
    {
        private readonly IPostService postService;
        private readonly IAreaService areaService;
        private readonly IUserService userService;
        private readonly ITagService tagService;
        private readonly IRegularExpressions expressions;

        public MetaWeblogAPI(IPostService postService, IAreaService areaService, IUserService userService, ITagService tagService, IRegularExpressions expressions)
        {
            this.postService = postService;
            this.areaService = areaService;
            this.userService = userService;
            this.tagService = tagService;
            this.expressions = expressions;
        }

        #region IMetaWeblog Members

        public string NewPost(string blogId, string username, string password, Post post, bool publish)
        {
            Oxite.Models.User user = getUser(username, password);

            Oxite.Models.Area area = areaService.GetArea(new Guid(blogId));

            Oxite.Models.Post newPost = new Oxite.Models.Post
            {
                Title = post.title,
                Body = post.description,
                Created = post.dateCreated == default(DateTime) ? DateTime.Now : post.dateCreated,
                Slug = string.IsNullOrEmpty(post.mt_basename) ? expressions.Slugify(post.title) : post.mt_basename,
                BodyShort = post.mt_excerpt,
                Creator = user,
                State = Oxite.Models.EntityState.Normal
            };

            if (publish)
                newPost.Published = DateTime.Now;

            if(post.categories != null)
                newPost.Tags = new List<Oxite.Models.Tag>(post.categories.Select(s => new Oxite.Models.Tag() { Name = s }));

            postService.AddPost(newPost);

            Oxite.Models.Post createdPost = postService.GetPost(new Oxite.Models.PostAddress(area.Name, newPost.Slug));

            return createdPost.ID.ToString();
        }

        

        public bool EditPost(string postId, string username, string password, Post post, bool publish)
        {
            if (string.IsNullOrEmpty(postId))
                throw new ArgumentException();

            getUser(username, password);

            Oxite.Models.Post existingPost = postService.GetPost(new Guid(postId));

            existingPost.Title = post.title;
            existingPost.Body = post.description;
            existingPost.BodyShort = post.mt_excerpt;
            existingPost.Slug = post.mt_basename;

            string[] postTags = post.categories ?? new string[] { };

            existingPost.Tags = postTags.Select(t => new Oxite.Models.Tag() { Name = t }).ToList();

            if (publish && !existingPost.Published.HasValue)
                existingPost.Published = DateTime.Now;

            postService.EditPost(existingPost);

            return true;
        }

        public Post GetPost(string postId, string username, string password)
        {
            getUser(username, password);

            Oxite.Models.Post post = postService.GetPost(new Guid(postId));

            if (post == null)
                throw new ArgumentOutOfRangeException();

            return ModelPostToServicePost(post);
        }

        public UrlData NewMediaObject(string blogId, string username, string password, FileData file)
        {
            throw new NotImplementedException();
        }

        public CategoryInfo[] GetCategories(string blogId, string username, string password)
        {
            getUser(username, password);

            return tagService.GetTags().Select(t => new CategoryInfo { description = t.Name }).ToArray();
        }

        public Post[] GetRecentPosts(string blogId, string username, string password, int numberOfPosts)
        {
            getUser(username, password);

            return postService.GetPosts(0, numberOfPosts, new Oxite.Models.Area() { ID = new Guid(blogId) }, null).Select(p => ModelPostToServicePost(p)).ToArray();
        }

        public BlogInfo[] GetUsersBlogs(string apikey, string username, string password)
        {
            getUser(username, password);

            return areaService.GetAreas().Select(a => new BlogInfo() { blogid = a.ID.ToString(), blogName = a.DisplayName, url = "" }).ToArray();
        }

        public bool DeletePost(string appkey, string postid, string username, string password, bool publish)
        {
            getUser(username, password);

            Oxite.Models.Post post = postService.GetPost(new Guid(postid));

            postService.RemovePost(post);

            return true;
        }

        #endregion

        private Oxite.Models.User getUser(string username, string password)
        {
            if (username == null || password == null)
                throw new ArgumentException("Invalid login");

            Oxite.Models.User user = userService.GetUser(username, password);

            if (user == null)
                throw new InvalidCredentialException();
            return user;
        }

        private static Post ModelPostToServicePost(Oxite.Models.Post post)
        {
            return new Post
            {
                categories = post.Tags.Select(t => t.Name).ToArray(),
                dateCreated = post.Created.Value,
                description = post.Body,
                mt_basename = post.Slug,
                mt_excerpt = post.BodyShort,
                postid = post.ID.ToString(),
                title = post.Title,
                userid = post.Creator.ID.ToString()
            };
        }
    }
}
