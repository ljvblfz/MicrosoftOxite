//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.ViewModels;
using OxiteSite.App_Code.Modules.OxiteSite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Filters
{
    public class Last3HeadlinesActionFilter : IActionFilter
    {
        private readonly IPostService postService;
        private readonly AppSettingsHelper appSettings;


        public Last3HeadlinesActionFilter(IPostService postService, AppSettingsHelper appSettings)
        {
            this.postService = postService;
            this.appSettings = appSettings;
            
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string blogAddress = appSettings.GetString("Last3Blog");

            OxiteViewModel model = filterContext.Controller.ViewData.Model as OxiteViewModel;

            if (model != null)
                model.AddModelItem(new Last3HeadlinesViewModel(postService.GetPosts(0, 3, new BlogAddress(blogAddress))));
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        #endregion
    }
}
