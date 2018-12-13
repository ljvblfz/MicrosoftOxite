//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Blogs.ViewModels;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Filters
{
    public class UserBlogListActionFilter : IActionFilter
    {
        private readonly IBlogService blogService;

        public UserBlogListActionFilter(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            //TODO: (erikpo) Change the following call to get just the blogs the current user has explicit permission to
            if (model != null)
                model.AddModelItem(new BlogListViewModel(blogService.GetBlogs(0, 10000)));
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
