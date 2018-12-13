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
using Oxite.Modules.Tags.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class PostController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IPostService postService;
        private readonly IBlogsCommentService commentService;
        private readonly ITagService tagService;
        private readonly OxiteContext context;

        public PostController(IBlogService blogService, IPostService postService, IBlogsCommentService commentService, ITagService tagService, OxiteContext context)
        {
            this.blogService = blogService;
            this.postService = postService;
            this.commentService = commentService;
            this.tagService = tagService;
            this.context = context;
            ValidateRequest = false;
        }

        public OxiteViewModelItems<Post> List(int? pageNumber, int pageSize)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            IPageOfItems<Post> posts = postService.GetPosts(pageIndex, pageSize);

            return new OxiteViewModelItems<Post>(posts) { Container = new BlogHomePageContainer() };
        }

        public OxiteViewModelItems<Post> ListByBlog(int? pageNumber, int pageSize, BlogAddress blogAddress)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            Blog blog = blogService.GetBlog(blogAddress);

            if (blog == null) return null;

            IPageOfItems<Post> posts = postService.GetPosts(pageIndex, pageSize, blogAddress);

            return new OxiteViewModelItems<Post>(posts) { Container = blog };
        }

        public OxiteViewModelItems<Post> ListByTag(int? pageNumber, int pageSize, TagAddress tagAddress)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            Tag tag = tagService.GetTag(tagAddress);
            IPageOfItems<Post> posts = postService.GetPosts(pageIndex, pageSize, tagAddress);

            if (tag == null || posts.TotalItemCount == 0) return null;

            return new OxiteViewModelItems<Post>(posts) { Container = tag };
        }

        public OxiteViewModelItems<Post> ListByArchive(int pageSize, ArchiveData archiveData)
        {
            int pageIndex = archiveData.Page - 1;
            IPageOfItems<Post> posts = postService.GetPosts(pageIndex, pageSize, archiveData);

            return new OxiteViewModelItems<Post>(posts) { Container = new ArchiveContainer(archiveData) };
        }

        public OxiteViewModelItems<Post> ListWithDrafts(int? pageNumber, int pageSize)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            IPageOfItems<Post> posts = postService.GetPostsWithDrafts(pageIndex, pageSize);

            return new OxiteViewModelItems<Post>(posts) { Container = new BlogHomePageContainer() };
        }

        public OxiteViewModelItems<Post> ListByFileType(int pageSize, string typeName)
        {
            return new OxiteViewModelItems<Post>(postService.GetPostsByFileType(0, pageSize, typeName));
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Head)]
        public OxiteViewModelItem<Post> Item(PostAddress postAddress)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            bool includeUnapproved = context.User.IsAuthenticated && context.User.IsInRole("Admin");

            return new OxiteViewModelItem<Post>(post) { Container = post.Blog };
        }

        [ActionName("Item"), AcceptVerbs(HttpVerbs.Post)]
        public object AddComment(PostAddress postAddress, CommentInput commentInput)
        {
            if (context.Site.CommentingDisabled) return null;

            Blog blog = blogService.GetBlog(postAddress.ToBlogAddress());

            if (blog == null || blog.CommentingDisabled) return null;

            Post post = postService.GetPost(postAddress);

            if (post == null || post.CommentingDisabled) return null;

            ModelResult<PostComment> addCommentResults = commentService.AddComment(postAddress, commentInput);

            if (!addCommentResults.IsValid)
            {
                ModelState.AddModelErrors(addCommentResults.ValidationState);

                return Item(postAddress);
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
        public OxiteViewModelItem<PostInput> Add(BlogAddress blogAddress, PostInput postInput)
        {
            //TODO: (erikpo) Check permissions

            Blog blog = blogAddress != null ? blogService.GetBlog(blogAddress) : null;

            return new OxiteViewModelItem<PostInput>(new PostInput(blog, postInput));
        }

        [ActionName("ItemAdd"), AcceptVerbs(HttpVerbs.Post)]
        public object AddSave(BlogAddress blogAddress, PostInput postInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<Post> results = postService.AddPost(postInput, EntityState.Normal);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return Add(blogAddress, postInput);
            }

            return Redirect(Url.Post(results.Item));
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<PostInput> Edit(PostAddress postAddress, PostInput postInput)
        {
            //TODO: (erikpo) Check permissions

            Post post = postService.GetPost(postAddress);

            if (post == null) { return null; }

            return new OxiteViewModelItem<PostInput>(new PostInput(post, postInput));
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public object EditSave(PostAddress postAddress, PostInput postInput)
        {
            //TODO: (erikpo) Check permissions

            ModelResult<Post> results = postService.EditPost(postAddress, postInput, EntityState.Normal);

            if (!results.IsValid)
            {
                ModelState.AddModelErrors(results.ValidationState);

                return Edit(postAddress, postInput);
            }

            return Redirect(Url.Post(results.Item));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Remove(PostAddress postAddress, string returnUri)
        {
            //TODO: (erikpo) Check permissions

            postService.RemovePost(postAddress);

            return Redirect(returnUri);
        }
    }
}
