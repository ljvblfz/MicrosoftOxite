//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.Blogs.Models;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Filters
{
    public class CommentingDisabledActionFilter : IActionFilter
    {
        private readonly OxiteContext context;

        public CommentingDisabledActionFilter(OxiteContext context)
        {
            this.context = context;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteViewModelItem<Post> postModel = filterContext.Controller.ViewData.Model as OxiteViewModelItem<Post>;

            if (postModel != null)
                postModel.CommentingDisabled = context.Site.CommentingDisabled || ((Blog)postModel.Container).CommentingDisabled || postModel.Item.CommentingDisabled;

            //TODO: (erikpo) Once comments are added to pages, add code similar to above to set allow comments for pages
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
