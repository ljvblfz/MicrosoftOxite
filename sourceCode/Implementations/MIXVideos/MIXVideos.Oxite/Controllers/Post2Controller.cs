//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.ViewModels;
using Oxite.Models;
using Oxite.Services;

namespace MIXVideos.Oxite.Controllers
{
    public class Post2Controller : Controller
    {
        private readonly IAreaService areaService;
        private readonly IPostService postService;

        public Post2Controller(IAreaService areaService, IPostService postService)
        {
            this.areaService = areaService;
            this.postService = postService;
        }

        public OxiteModelList<Post> ListAll()
        {
            Area area = areaService.GetArea("MIX09");
            if (area == null) return null;

            return new OxiteModelList<Post>
            {
                Container = area,
                List = postService.GetPosts(0, 500, area, null)
            };
        }
    }
}
