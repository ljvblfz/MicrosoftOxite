//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Extensions;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Tags.Models;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IBlogsCommentService commentService;
        private readonly OxiteContext context;

        public PostController(IPostService postService, IBlogsCommentService commentService, OxiteContext context)
        {
            this.postService = postService;
            this.commentService = commentService;
            this.context = context;
            ValidateRequest = false;
        }

        public OxiteViewModelItems<Post> List(PagingInfo pagingInfo)
        {
            IPageOfItems<Post> posts = postService.GetPosts(pagingInfo);

            return new OxiteViewModelItems<Post>(posts) { Container = new BlogHomePageContainer() };
        }

        public OxiteViewModelItems<Post> ListByBlog(PagingInfo pagingInfo, Blog blog)
        {
            if (blog == null) return null;

            IPageOfItems<Post> posts = postService.GetPosts(pagingInfo, blog);

            return new OxiteViewModelItems<Post>(posts) { Container = blog };
        }

        public OxiteViewModelItems<Post> ListByTag(PagingInfo pagingInfo, Tag tag)
        {
            IPageOfItems<Post> posts = postService.GetPosts(pagingInfo, tag);

            if (tag == null || posts.TotalItemCount == 0) return null;

            return new OxiteViewModelItems<Post>(posts) { Container = tag };
        }

        public OxiteViewModelItems<Post> ListByArchive(int pageSize, ArchiveData archiveData)
        {
            IPageOfItems<Post> posts = postService.GetPosts(new PagingInfo(archiveData.Page - 1, pageSize), archiveData);

            return new OxiteViewModelItems<Post>(posts) { Container = new ArchiveContainer(archiveData) };
        }

        public OxiteViewModelItems<Post> ListWithDrafts(PagingInfo pagingInfo)
        {
            IPageOfItems<Post> posts = postService.GetPostsWithDrafts(pagingInfo);

            return new OxiteViewModelItems<Post>(posts) { Container = new BlogHomePageContainer() };
        }

        public OxiteViewModelItems<Post> ListByFileType(PagingInfo pagingInfo, string typeName)
        {
            return new OxiteViewModelItems<Post>(postService.GetPostsByFileType(pagingInfo, typeName));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public OxiteViewModelItem<Post> Item(Post post)
        {
            if (post == null) return null;

            return new OxiteViewModelItem<Post>(post) { Container = post.Blog };
        }

        [ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        public object AddComment(Post post, CommentInput commentInput)
        {
            if (context.Site.CommentingDisabled) return null;
            if (post == null || post.CommentingDisabled) return null;
            if (post.Blog.CommentingDisabled) return null;

            ModelResult<PostComment> addCommentResults = commentService.AddComment(post, commentInput);

            if (!addCommentResults.IsValid)
            {
                ModelState.AddModelErrors(addCommentResults.ValidationState);

                return Item(post);
            }

            if (!context.User.IsAuthenticated)
            {
                if (commentInput.SaveAnonymousUser)
                    Response.Cookies.SetAnonymousUser(commentInput.Creator);
                else if (Request.Cookies.GetAnonymousUser() != null)
                    Response.Cookies.ClearAnonymousUser();
            }

            return new RedirectResult(
                addCommentResults.Item.State != EntityState.PendingApproval
                ? Url.Comment(addCommentResults.Item)
                : Url.CommentPending(addCommentResults.Item)
                );
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Validate(PostInput postInput)
        {
            ValidationStateDictionary validationState = postService.ValidatePostInput(postInput);

            if (validationState.IsValid) return Content("");

            return PartialView("ValidationErrors", new OxiteViewModelPartial<ValidationStateDictionary>(new OxiteViewModel(), validationState));
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<PostInput> Add(Blog blog, PostInput postInput)
        {
            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItem<PostInput>(new PostInput(blog, postInput));
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Post)]
        public object AddSave(Blog blog, PostInput postInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<Post> results = postService.AddPost(postInput, EntityState.Normal);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return Add(blog, postInput);
            }

            return Redirect(Url.Post(results.Item));
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<PostInput> Edit(Post post, PostInput postInput)
        {
            if (post == null) return null;

            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItem<PostInput>(new PostInput(post, postInput));
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public object EditSave(Post post, PostInput postInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<Post> results = postService.EditPost(post, postInput, EntityState.Normal);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return Edit(post, postInput);
            }

            return Redirect(Url.Post(results.Item));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove(Post post, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            if (post == null) return null;

            postService.Remove(post);

            return Redirect(returnUri);
        }
    }
}
