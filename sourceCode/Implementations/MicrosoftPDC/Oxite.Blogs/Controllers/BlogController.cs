//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Infrastructure;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;
        private readonly IPostService postService;
        private readonly IBlogsCommentService commentService;
        private readonly ILanguageService languageService;
        private readonly AbsolutePathHelper absolutePathHelper;

        public BlogController(IBlogService blogService, IPostService postService, IBlogsCommentService commentService, ILanguageService languageService, AbsolutePathHelper absolutePathHelper)
        {
            this.blogService = blogService;
            this.postService = postService;
            this.commentService = commentService;
            this.languageService = languageService;
            this.absolutePathHelper = absolutePathHelper;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual OxiteViewModelItems<Blog> Find()
        {
            return new OxiteViewModelItems<Blog>();
        }

        [ActionName("Find"), AcceptVerbs(HttpVerbs.Post)]
        public virtual OxiteViewModelItems<Blog> FindQuery(BlogSearchCriteria searchCriteria)
        {
            IEnumerable<Blog> foundBlogs = blogService.FindBlogs(searchCriteria);
            OxiteViewModelItems<Blog> model = new OxiteViewModelItems<Blog>(foundBlogs);

            model.AddModelItem(searchCriteria);

            return model;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public virtual OxiteViewModelItem<Blog> ItemEdit(BlogAddress blogAddress)
        {
            Blog blog = blogAddress != null ? blogService.GetBlog(blogAddress) : null;
            
            return new OxiteViewModelItem<Blog>(blog);
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public virtual object ItemSave(BlogAddress blogAddress, BlogInput blogInput)
        {
            ValidationStateDictionary validationState;

            if (blogAddress == null)
            {
                ModelResult<Blog> results = blogService.AddBlog(blogInput);

                validationState = results.ValidationState;

                if (results.IsValid)
                {
                    ////TODO: (erikpo) Get rid of HasMultipleBlogs and make it a calculated field so the following isn't necessary
                    //Site siteToEdit = siteService.GetSite(site.Name);

                    //siteToEdit.HasMultipleBlogs = true;

                    //siteService.EditSite(siteToEdit, out validationState);

                    if (validationState.IsValid)
                        OxiteApplication.Load(ControllerContext.HttpContext);
                }
            }
            else
            {
                ModelResult<Blog> results = blogService.EditBlog(blogAddress, blogInput);

                validationState = results.ValidationState;
            }

            if (!validationState.IsValid)
            {
                ModelState.AddModelErrors(validationState);

                return ItemEdit(blogAddress);
            }

            return Redirect(Url.AppPath(Url.Admin()));
        }
    }
}