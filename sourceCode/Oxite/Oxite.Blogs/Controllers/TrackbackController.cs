//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Services;
using Oxite.Modules.Blogs.ViewModels;
using Oxite.Services;

namespace Oxite.Modules.Blogs.Controllers
{
    public class TrackbackController : Controller
    {
        private readonly IPostService postService;

        public TrackbackController(IPostService postService)
        {
            this.postService = postService;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public TrackbackViewModel Add(Post post, FormCollection form)
        {
            if (post == null)
                return new TrackbackViewModel(0, "ID is invalid or missing");

            string incomingUrl = getParameter(form, "url");
            string incomingTitle = getParameter(form, "title");
            string incomingBlogName = getParameter(form, "blog_name");
            string incomingExcerpt = getParameter(form, "excerpt");

            if (string.IsNullOrEmpty(incomingUrl))
                return new TrackbackViewModel(1, "no url parameter found, please try harder!");

            Trackback trackback = post.Trackbacks.Where(tb => string.Equals(tb.Url, incomingUrl, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            try
            {
                if (trackback == null)
                {
                    trackback = new Trackback
                    {
                        Title = incomingTitle,
                        Body = incomingExcerpt,
                        Url = incomingUrl,
                        BlogName = incomingBlogName,
                        Source = "",
                        Created = DateTime.Now.ToUniversalTime()
                    };

                    postService.AddTrackback(post, trackback);
                }
                else
                {
                    trackback.Title = incomingTitle;
                    trackback.Body = incomingExcerpt;
                    trackback.BlogName = incomingBlogName;
                    trackback.IsTargetInSource = null;

                    postService.EditTrackback(trackback);
                }

                return new TrackbackViewModel();
            }
            catch
            {
                return new TrackbackViewModel(2, "Failed to save Trackback.");
            }
        }

        private static string getParameter(NameValueCollection values, string parameterName)
        {
            if (values[parameterName] != null)
            {
                return HttpUtility.HtmlEncode(values[parameterName]);
            }

            return "";
        }
    }
}
