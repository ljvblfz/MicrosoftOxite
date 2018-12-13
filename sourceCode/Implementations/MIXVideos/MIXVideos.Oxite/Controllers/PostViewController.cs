//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using MIXVideos.Oxite.Results;
using MIXVideos.Oxite.Services;
using Oxite.Models;
using Oxite.Services;

namespace MIXVideos.Oxite.Controllers
{
    public class PostViewController : Controller
    {
        private readonly IPostService postService;
        private readonly IPostViewService postViewService;

        public PostViewController(IPostService postService, IPostViewService postViewService)
        {
            this.postService = postService;
            this.postViewService = postViewService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Item(PostAddress postAddress, string viewType)
        {
            Post post = postService.GetPost(postAddress);

            if (post == null) return null;

            postViewService.AddView(post.ID, viewType);

            return new PostViewResult();
        }
    }
}
