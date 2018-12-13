//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Infrastructure.XmlRpc;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Controllers
{
    public class PingbackController : Controller
    {
        private readonly IPostService postService;
        private readonly UrlHelper urlHelper;
        private readonly OxiteContext context;

        public PingbackController(IPostService postService, UrlHelper urlHelper, OxiteContext context)
        {
            this.postService = postService;
            this.urlHelper = urlHelper;
            this.context = context;
        }

        [ActionName("pingback.ping")]
        public ActionResult Ping(string sourceUri, string targetUri)
        {
            if (sourceUri == null || targetUri == null)
                throw new ArgumentNullException();

            RouteData postRouteData = urlHelper.GetPostRouteDataFromUri(new Uri(targetUri), context);
            Post post = postRouteData != null ? postService.GetPost(postRouteData.GetRequiredString("blogName"), postRouteData.GetRequiredString("postSlug")) : null;

            if (post == null)
                return new XmlRpcFaultResult(33, "Cannot find post");

            Trackback trackback = post.Trackbacks.Where(tb => string.Equals(tb.Url, sourceUri, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            if (trackback == null)
            {
                trackback = new Trackback()
                                {
                                    Url = sourceUri,
                                    Created = DateTime.Now.ToUniversalTime(),
                                    Title = string.Empty,
                                    BlogName = string.Empty,
                                    Body = string.Empty,
                                    Source = string.Empty
                                };
                postService.AddTrackback(post, trackback);
            }

            return new XmlRpcResult("Success");
        }
    }
}