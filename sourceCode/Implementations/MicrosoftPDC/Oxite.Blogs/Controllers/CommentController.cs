//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class CommentController : Controller
    {
        private readonly IPostService postService;
        private readonly IBlogsCommentService commentService;
        private readonly ITagService tagService;
        private readonly IBlogService blogService;
        private readonly OxiteContext context;

        public CommentController(IPostService postService, IBlogsCommentService commentService, ITagService tagService, IBlogService blogService, OxiteContext context)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.tagService = tagService;
            this.blogService = blogService;
            this.context = context;
        }

        public OxiteViewModelItems<PostComment> List(int? pageNumber, int pageSize)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            IPageOfItems<PostComment> comments = commentService.GetComments(pageIndex, pageSize, false, false);

            return new OxiteViewModelItems<PostComment>(comments) { Container = new BlogHomePageContainer() };
        }

        public OxiteViewModelItems<PostComment> ListByTag(int? pageNumber, int pageSize, TagAddress tagAddress)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            Tag tag = tagService.GetTag(tagAddress);

            if (tag == null) return null;

            IPageOfItems<PostComment> comments = commentService.GetComments(pageIndex, pageSize, tag);

            return new OxiteViewModelItems<PostComment>(comments) { Container = tag };
        }

        public OxiteViewModelItems<PostComment> ListByBlog(int? pageNumber, int pageSize, BlogAddress blogAddress)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            Blog blog = blogService.GetBlog(blogAddress);

            if (blog == null) return null;

            IPageOfItems<PostComment> comments = commentService.GetComments(pageIndex, pageSize, blog);

            return new OxiteViewModelItems<PostComment>(comments) { Container = blog };
        }

        public OxiteViewModelItems<PostComment> ListByPost(int? pageNumber, int pageSize, PostAddress postAddress)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            bool includeUnapproved = context.User.IsAuthenticated && context.User.IsInRole("Admin");
            IPageOfItems<PostComment> comments = commentService.GetComments(pageIndex, pageSize, post, includeUnapproved);

            return new OxiteViewModelItems<PostComment>(comments) { Container = post };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Validate(CommentInput commentInput)
        {
            ValidationStateDictionary validationState = commentService.ValidateCommentInput(commentInput);

            if (validationState.IsValid) return Content("");

            return PartialView("ValidationErrors", new OxiteViewModelPartial<ValidationStateDictionary>(new OxiteViewModel(), validationState));
        }

        public OxiteViewModelItems<PostComment> ListForAdmin(int? pageNumber, int pageSize)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            IPageOfItems<PostComment> comments = commentService.GetComments(pageIndex, pageSize, true, true);

            return new OxiteViewModelItems<PostComment>(comments) { Container = new BlogHomePageContainer() };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Approve(PostCommentAddress commentAddress, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            if (commentService.ApproveComment(commentAddress))
            {
                if (!string.IsNullOrEmpty(returnUri)) return new RedirectResult(returnUri);

                return new JsonResult { Data = true };
            }
            else
            {
                return new JsonResult { Data = false };
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove(PostCommentAddress commentAddress, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            if (commentService.RemoveComment(commentAddress))
            {
                if (!string.IsNullOrEmpty(returnUri)) return new RedirectResult(returnUri);

                return new JsonResult { Data = true };
            }
            else
            {
                return new JsonResult { Data = false };
            }
        }

        public ActionResult CommentOnCommentPartial(PostCommentAddress commentAddress/*Guid? id*/)
        {
            //TODO: (erikpo) Need to fix
            PostComment comment = null; //commentService.GetComment(id ?? Guid.Empty);

            if (comment == null) return Content("");

            return PartialView("CommentOnComment", new OxiteViewModelPartial<PostComment>(new OxiteViewModel(), comment));
        }
    }
}
