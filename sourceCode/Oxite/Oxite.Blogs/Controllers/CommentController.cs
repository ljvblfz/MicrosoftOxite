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
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class CommentController : Controller
    {
        private readonly IBlogsCommentService commentService;
        private readonly OxiteContext context;

        public CommentController(IBlogsCommentService commentService, OxiteContext context)
        {
            this.commentService = commentService;
            this.context = context;
        }

        public OxiteViewModelItems<PostComment> List(PagingInfo pagingInfo)
        {
            IPageOfItems<PostComment> comments = commentService.GetComments(pagingInfo, false, false);

            return new OxiteViewModelItems<PostComment>(comments) { Container = new BlogHomePageContainer() };
        }

        public OxiteViewModelItems<PostComment> ListByTag(PagingInfo pagingInfo, Tag tag)
        {
            if (tag == null) return null;

            IPageOfItems<PostComment> comments = commentService.GetComments(pagingInfo, tag);

            return new OxiteViewModelItems<PostComment>(comments) { Container = tag };
        }

        public OxiteViewModelItems<PostComment> ListByBlog(PagingInfo pagingInfo, Blog blog)
        {
            if (blog == null) return null;

            IPageOfItems<PostComment> comments = commentService.GetComments(pagingInfo, blog);

            return new OxiteViewModelItems<PostComment>(comments) { Container = blog };
        }

        public OxiteViewModelItems<PostComment> ListByPost(PagingInfo pagingInfo, Post post)
        {
            if (post == null) return null;

            bool includeUnapproved = context.User.IsAuthenticated && context.User.IsInRole("Admin");
            IPageOfItems<PostComment> comments = commentService.GetComments(pagingInfo, post, includeUnapproved);

            return new OxiteViewModelItems<PostComment>(comments) { Container = post };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Validate(CommentInput commentInput)
        {
            ValidationStateDictionary validationState = commentService.ValidateCommentInput(commentInput);

            if (validationState.IsValid) return Content("");

            return PartialView("ValidationErrors", new OxiteViewModelPartial<ValidationStateDictionary>(new OxiteViewModel(), validationState));
        }

        public OxiteViewModelItems<PostComment> ListForAdmin(PagingInfo pagingInfo)
        {
            IPageOfItems<PostComment> comments = commentService.GetComments(pagingInfo, true, true);

            return new OxiteViewModelItems<PostComment>(comments) { Container = new BlogHomePageContainer() };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Approve(PostComment comment, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            if (commentService.ApproveComment(comment))
            {
                if (!string.IsNullOrEmpty(returnUri)) return new RedirectResult(returnUri);

                return new JsonResult { Data = true };
            }

            return new JsonResult { Data = false };
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove(PostComment comment, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            if (commentService.RemoveComment(comment))
            {
                if (!string.IsNullOrEmpty(returnUri)) return new RedirectResult(returnUri);

                return new JsonResult { Data = true };
            }

            return new JsonResult { Data = false };
        }

        public ActionResult CommentOnCommentPartial(PostComment comment/*Guid? id*/)
        {
            if (comment == null) return Content("");

            return PartialView("CommentOnComment", new OxiteViewModelPartial<PostComment>(new OxiteViewModel(), comment));
        }
    }
}
