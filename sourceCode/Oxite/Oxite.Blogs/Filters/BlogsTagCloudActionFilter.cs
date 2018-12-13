//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Blogs.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Filters
{
    public class BlogsTagCloudActionFilter : IActionFilter
    {
        private readonly IBlogsTagService tagService;
        private readonly IBlogService blogService;

        public BlogsTagCloudActionFilter(IBlogsTagService tagService, IBlogService blogService)
        {
            this.tagService = tagService;
            this.blogService = blogService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                Blog blog = blogService.GetBlog(filterContext.RouteData.Values["blogName"] as string);

                if (blog != null)
                    model.AddModelItem(new TagCloudViewModel(tagService.GetTagsUsedIn(blog)));
                else
                    model.AddModelItem(new TagCloudViewModel(tagService.GetTagsWithPostCount()));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
