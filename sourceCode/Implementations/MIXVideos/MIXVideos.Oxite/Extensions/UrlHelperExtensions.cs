//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Models;

namespace MIXVideos.Oxite.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string PostViewBug(this UrlHelper urlHelper, Post post, string viewType)
        {
            return urlHelper.RouteUrl("PostView", new { areaName = post.Area.Name, slug = post.Slug, viewType = viewType });
        }
    }
}
