//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Results;
using Oxite.Services;

namespace Oxite.Controllers
{
    public class ViewTrackingController : Controller
    {
        private readonly IPostService postService;
        private readonly IViewTrackingService viewTrackingService;

        public ViewTrackingController(IPostService postService, IViewTrackingService viewTrackingService)
        {
            this.postService = postService;
            this.viewTrackingService = viewTrackingService;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            //viewTrackingService.AddView(post.ID, viewType);

            return new ViewTrackingResult();
        }
    }
}
