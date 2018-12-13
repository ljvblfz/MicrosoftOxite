//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Validation;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItems<Blog> Find()
        {
            //TODO: (erikpo) Check permissions

            return new OxiteViewModelItems<Blog>();
        }

        [ActionName("Find"), AcceptVerbs(HttpVerbs.Post)]
        public OxiteViewModelItems<Blog> FindQuery(BlogSearchCriteria searchCriteria)
        {
            //TODO: (erikpo) Check permissions

            IEnumerable<Blog> foundBlogs = blogService.FindBlogs(searchCriteria);
            OxiteViewModelItems<Blog> model = new OxiteViewModelItems<Blog>(foundBlogs);

            model.AddModelItem(searchCriteria);

            return model;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public OxiteViewModelItem<Blog> ItemEdit(Blog blog)
        {
            //TODO: (erikpo) Check permissions

            if (blog == null) return null;
            
            return new OxiteViewModelItem<Blog>(blog);
        }

        [ActionName("ItemEdit"), AcceptVerbs(HttpVerbs.Post)]
        public object ItemSave(Blog blog, BlogInput blogInput)
        {
            //TODO: (erikpo) Check permissions

            ValidationStateDictionary validationState;

            if (blog == null)
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
                ModelResult<Blog> results = blogService.EditBlog(blog, blogInput);

                validationState = results.ValidationState;
            }

            if (!validationState.IsValid)
            {
                ModelState.AddModelErrors(validationState);

                return ItemEdit(blog);
            }

            return Redirect(Url.AppPath(Url.Admin()));
        }
    }
}