//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;
using Oxite.ViewModels;

namespace Oxite.Filters
{
    public class CommentingDisabledActionFilter : IActionFilter
    {
        private readonly Site site;

        public CommentingDisabledActionFilter(Site site)
        {
            this.site = site;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteModelItem<Post> postModel = filterContext.Controller.ViewData.Model as OxiteModelItem<Post>;

            if (postModel != null)
            {
                postModel.CommentingDisabled = site.CommentingDisabled || ((Area)postModel.Container).CommentingDisabled || postModel.Item.CommentingDisabled;
            }

            //TODO: (erikpo) Once comments are added to pages, add code similar to above to set allow comments for pages
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
