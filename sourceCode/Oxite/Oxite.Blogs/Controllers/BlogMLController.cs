//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using System.Xml;
using BlogML.Xml;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.BlogML.Extensions;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class BlogMLController : Controller
    {
        private readonly ISiteService siteService;
        private readonly ILanguageService languageService;
        private readonly IBlogService blogService;
        private readonly IPostService postService;
        private readonly IBlogsCommentService commentService;
        private readonly OxiteContext context;

        public BlogMLController(ISiteService siteService, ILanguageService languageService, IBlogService blogService, IPostService postService, IBlogsCommentService commentService, OxiteContext context)
        {
            this.siteService = siteService;
            this.languageService = languageService;
            this.blogService = blogService;
            this.postService = postService;
            this.commentService = commentService;
            this.context = context;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual OxiteViewModel Import(Blog blog)
        {
            if (blog == null) return null;

            return new OxiteViewModel { Container = blog };
        }

        [ActionName("Import"), AcceptVerbs(HttpVerbs.Post)]
        public virtual OxiteViewModel ImportSave(Blog blog, string slugPattern)
        {
            if (blog == null) return null;

            ValidationStateDictionary validationState = new ValidationStateDictionary();
            XmlTextReader reader = null;
            bool modifiedSite = false;

            try
            {
                reader = new XmlTextReader(Request.Files[0].InputStream);

                BlogMLBlog blogMLBlog = BlogMLSerializer.Deserialize(reader);
                Language language = languageService.GetLanguage(context.Site.LanguageDefault);
                BlogInputForImport blogInput = new BlogInputForImport(blogMLBlog.SubTitle, blogMLBlog.SubTitle, blogMLBlog.DateCreated);
                ModelResult<Blog> results = blogService.EditBlog(blog, blogInput);

                if (!results.IsValid)
                {
                    ModelState.AddModelErrors(results.ValidationState);

                    return Import(blog);
                }

                if (!context.Site.HasMultipleBlogs)
                {
                    Site site = siteService.GetSite();

                    site.DisplayName = blog.DisplayName;
                    site.Description = blog.Description;

                    siteService.EditSite(site, out validationState);

                    if (!validationState.IsValid) throw new Exception();

                    modifiedSite = true;
                }

                postService.RemoveAll(blog);

                foreach (BlogMLPost blogMLPost in blogMLBlog.Posts)
                {
                    if (string.IsNullOrEmpty(blogMLPost.Title) || string.IsNullOrEmpty(blogMLPost.Content.Text))
                        continue;

                    PostInputForImport postInput = blogMLPost.ToImportPostInput(blogMLBlog, context.Site.CommentingDisabled | blog.CommentingDisabled, slugPattern, blogMLPost.Approved ? EntityState.Normal : EntityState.PendingApproval, context.User.Cast<User>());
                    ModelResult<Post> addPostResults = postService.AddPost(blog, postInput);

                    if (!addPostResults.IsValid)
                    {
                        ModelState.AddModelErrors(addPostResults.ValidationState);

                        return Import(blog);
                    }

                    foreach (BlogMLComment blogMLComment in blogMLPost.Comments)
                    {
                        CommentInputForImport commentInput = blogMLComment.ToImportCommentInput(blogMLBlog, context.User.Cast<User>(), language);
                        ModelResult<PostComment> addCommentResults = commentService.AddComment(addPostResults.Item, commentInput);

                        if (!addCommentResults.IsValid)
                        {
                            ModelState.AddModelErrors(addCommentResults.ValidationState);

                            return Import(blog);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelErrors(validationState);

                if (!string.IsNullOrEmpty(ex.Message))
                    ModelState.AddModelError("ModelName", ex);

                return Import(blog);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            if (modifiedSite)
                OxiteApplication.Load(ControllerContext.HttpContext);

            return new OxiteViewModel { Container = blog };
        }

        //TODO: (erikpo) Need to add Export actions
    }
}
