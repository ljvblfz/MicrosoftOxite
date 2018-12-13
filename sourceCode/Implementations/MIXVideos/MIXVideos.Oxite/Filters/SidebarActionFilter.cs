//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using System.Web.Mvc;
using MIXVideos.Oxite.ViewModels;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;
using System.Collections.Generic;

namespace MIXVideos.Oxite.Filters
{
    public class SidebarActionFilter : IActionFilter
    {
        private readonly IAreaService areaService;
        private readonly IPostService postService;

        public SidebarActionFilter(IAreaService areaService, IPostService postService)
        {
            this.areaService = areaService;
            this.postService = postService;
        }

        #region IActionFilter Members

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OxiteModel model = filterContext.Controller.ViewData.Model as OxiteModel;

            if (model != null)
            {
                string areaName = filterContext.RouteData.Values["areaName"] as string;
                Area area = !string.IsNullOrEmpty(areaName) ? areaService.GetArea(areaName) : null;
                Post post = null;
                List<Post> posts = new List<Post>();

                if (area != null && string.Compare(area.Name, "IE8", true) == 0)
                    posts.AddRange(postService.GetPosts(0, 50, area, (DateTime?)null));

                if (area != null && string.Compare(area.Name, "MIX09", true) == 0)
                    post = postService.GetPosts(0, 1, area, (DateTime?)null).FirstOrDefault();
                else
                    post = postService.GetRandomPost();

                model.AddModelItem(new SidebarViewModel(post, posts));
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) { }

        #endregion
    }
}
