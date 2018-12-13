//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class SiteMapController : Controller
    {
        private readonly IPostService postService;

        public SiteMapController(IPostService postService)
        {
            this.postService = postService;
        }

        public OxiteViewModelItems<DateTime> SiteMapIndex()
        {
            IEnumerable<DateTime> postDateGroups = postService.GetPostDateGroups();

            return new OxiteViewModelItems<DateTime>(postDateGroups);
        }

        public OxiteViewModelItems<Post> SiteMap(OneMonthDateRangeAddress dateRangeAddress)
        {
            return new OxiteViewModelItems<Post>(postService.GetPosts(dateRangeAddress));
        }
    }
}
