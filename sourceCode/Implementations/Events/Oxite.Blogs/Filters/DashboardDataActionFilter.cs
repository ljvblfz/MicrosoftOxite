//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Blogs.ViewModels;
using Oxite.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Filters
{
    public class DashboardDataActionFilter : IActionFilter
    {
        private readonly IBlogService blogService;
        private readonly IPostService postService;
        private readonly IBlogsCommentService commentService;

        public DashboardDataActionFilter(IBlogService blogService, IPostService postService, IBlogsCommentService commentService)
        {
            this.blogService = blogService;
            this.postService = postService;
            this.commentService = commentService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
            {
                //recent posts - all up
                IEnumerable<Post> posts = postService.GetPostsWithDrafts(0, 5);

                //recent comments - all up
                IEnumerable<PostComment> comments = commentService.GetComments(0, 10, true, true);

                model.AddModelItem(new BlogAdminDataViewModel(posts, comments));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
