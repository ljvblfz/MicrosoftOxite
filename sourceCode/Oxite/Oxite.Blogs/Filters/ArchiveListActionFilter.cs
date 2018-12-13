//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Blogs.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Filters
{
    public class ArchiveListActionFilter : IActionFilter
    {
        private readonly IPostService postService;
        private readonly IBlogService blogService;

        public ArchiveListActionFilter(IPostService postService, IBlogService blogService)
        {
            this.postService = postService;
            this.blogService = blogService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                IEnumerable<KeyValuePair<ArchiveData, int>> archives;
                INamedEntity container;
                Blog blog = blogService.GetBlog(filterContext.RouteData.Values["blogName"] as string);

                if (blog != null)
                {
                    archives = postService.GetArchives(blog);
                    container = blog;
                }
                else
                {
                    archives = postService.GetArchives();
                    container = new BlogHomePageContainer();
                }

                model.AddModelItem(new ArchiveViewModel(archives, container));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionParameters.ContainsKey("pageSize"))
                filterContext.ActionParameters["pageSize"] = 20;
        }

        #endregion
    }
}
