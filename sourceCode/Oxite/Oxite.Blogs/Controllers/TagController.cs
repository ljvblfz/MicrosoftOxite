//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Tags.Models;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Controllers
{
    public class TagController : Controller
    {
        private readonly IBlogsTagService tagService;

        public TagController(IBlogsTagService tagService)
        {
            this.tagService = tagService;
        }

        public OxiteViewModelItems<KeyValuePair<PostTag, int>> Cloud()
        {
            return new OxiteViewModelItems<KeyValuePair<PostTag, int>>(tagService.GetTagsWithPostCount()) { Container = new TagCloudPageContainer() };
        }
    }
}
