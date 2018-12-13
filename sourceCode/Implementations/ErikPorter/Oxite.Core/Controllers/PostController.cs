//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ITagService tagService;
        private readonly IAreaService areaService;
        private readonly Site site;

        public PostController(IPostService postService, ITagService tagService, IAreaService areaService, Site site)
        {
            this.postService = postService;
            this.tagService = tagService;
            this.areaService = areaService;
            this.site = site;
            ValidateRequest = false;
        }

        public virtual OxiteModelList<Post> List(int? pageNumber, int pageSize, DateTime? ifModifiedSince)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;

            return GetPostList(new HomePageContainer(), () => postService.GetPosts(pageIndex, pageSize, ifModifiedSince));
        }

        public virtual OxiteModelList<Post> ListByArea(int? pageNumber, int pageSize, Area areaInput, DateTime? ifModifiedSince)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;

            Area area = areaService.GetArea(areaInput.Name);

            if (area == null)
                return null;

            return GetPostList(area, () => postService.GetPosts(pageIndex, pageSize, area, ifModifiedSince));
        }

        public virtual OxiteModelList<Post> ListByTag(int? pageNumber, int pageSize, Tag tagInput, DateTime? ifModifiedSince)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;

            Tag tag = tagService.GetTag(tagInput.Name);

            if (tag == null)
                return null;

            return GetPostList(tag, () => postService.GetPosts(pageIndex, pageSize, tag, ifModifiedSince));
        }

        public virtual OxiteModelList<Post> ListByArchive(int pageSize, ArchiveData archiveData)
        {
            int pageIndex = archiveData.Page - 1;

            return GetPostList(new ArchiveContainer(archiveData), () => postService.GetPosts(pageIndex, pageSize, archiveData));
        }

        public virtual OxiteModelList<Post> ListBySearch(int? pageNumber, int pageSize, SearchCriteria criteria, DateTime? ifModifiedSince)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;

            if (!criteria.HasCriteria())
                return List(pageNumber, pageSize, ifModifiedSince);

            return GetPostList(new SearchPageContainer(), () => postService.GetPosts(pageIndex, pageSize, criteria, ifModifiedSince));
        }

        public virtual OxiteModelList<Post> ListWithDrafts(int? pageNumber, int pageSize)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;

            return GetPostList(new HomePageContainer(), () => postService.GetPostsWithDrafts(pageIndex, pageSize));
        }

        public virtual OxiteModelItem<Post> Item(PostAddress postAddress)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null)
                return null;

            return new OxiteModelItem<Post>
            {
                Container = post.Area,
                Item = post
            };
        }

        [ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        public virtual object AddComment(PostAddress postAddress, Comment commentInput, UserBase userBaseInput, UserBase currentUser, bool? remember, bool? subscribe)
        {
            if (site.CommentingDisabled) return null;

            Post post = postService.GetPost(postAddress);

            if (post == null || post.CommentingDisabled) { return null; }

            ValidationStateDictionary validationState;
            Comment newComment;

            postService.AddComment(post.Area, post, commentInput, currentUser ?? userBaseInput, subscribe.HasValue && subscribe.Value, out validationState, out newComment);

            if (!validationState.IsValid)
            {
                ModelState.AddModelErrors(validationState);

                return Item(postAddress);
            }

            //todo: (nheskew) move into an action filter?
            if (remember != null && (bool)remember)
            {
                Response.Cookies.SetAnonymousUser(userBaseInput);
            }
            else if (currentUser == null && Request.Cookies.GetAnonymousUser() != null)
            {
                Response.Cookies.ClearAnonymousUser();
            }

            return new RedirectResult(newComment.State != EntityState.PendingApproval ? Url.Comment(post, newComment) : Url.CommentPending(post, newComment));
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Get)]
        public virtual OxiteModelItem<Post> Add(Area areaInput, Post post)
        {
            Area area = areaService.GetArea(areaInput.Name);

            return new OxiteModelItem<Post>
            {
                Container = area,
                Item = post
            };
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Post)]
        public virtual object SaveAdd(Area areaInput, Post postInput, User currentUser)
        {
            Area area = areaService.GetArea(areaInput.Name);

            ValidationStateDictionary validationState;
            Post newPost;

            postService.AddPost(postInput, currentUser, true, out validationState, out newPost);

            if (!validationState.IsValid)
            {
                ModelState.AddModelErrors(validationState);

                return Add(areaInput, postInput);
            }

            return Redirect(Url.Post(postInput));
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Get)]
        public virtual OxiteModelItem<Post> Edit(PostAddress postAddress)
        {
            Post existingPost = postService.GetPost(postAddress);

            if (existingPost == null) { return null; }

            return new OxiteModelItem<Post>
            {
                Container = existingPost.Area,
                Item = existingPost
            };
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public virtual object SaveEdit(PostAddress postAddress, Post postInput)
        {
            Post post = postService.GetPost(postAddress);

            ValidationStateDictionary validationState;

            postService.EditPost(post, postInput, out validationState);

            if (!validationState.IsValid)
            {
                ModelState.AddModelErrors(validationState);

                return Edit(postAddress);
            }

            return Redirect(Url.Post(postInput));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult Remove(PostAddress postAddress, string returnUri)
        {
            Post post = postService.GetPost(postAddress);

            postService.RemovePost(post);

            return Redirect(returnUri);
        }

        private static OxiteModelList<Post> GetPostList(INamedEntity container, Func<IPageOfList<Post>> serviceCall)
        {
            OxiteModelList<Post> result = new OxiteModelList<Post>
            {
                Container = container,
                List = serviceCall()
            };

            return result;
        }
    }
}
