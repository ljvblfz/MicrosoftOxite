//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using MIXVideos.Oxite.Extensions;
using MIXVideos.Oxite.Models;
using Oxite.Models;
using Oxite.Services;
using Oxite.ViewModels;

namespace MIXVideos.Oxite.Controllers
{
    public class FeedController : Controller
    {
        private IPostService postService;

        public FeedController(IPostService postService)
        {
            this.postService = postService;
        }

        public OxiteModelList<Post> List(string typeName)
        {
            return new OxiteModelList<Post>()
            {
                Container = new FeedPageContainer(typeName, typeName.GetFileTypeDisplayName()),
                List = postService.GetPostsByFileType(0, 50, typeName)
            };
        }
    }
}
