//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GlobalContentEdit(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("GlobalContentEdit");
        }

        public static string Pages(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Pages");
        }

        public static string Page(this UrlHelper urlHelper, Page page)
        {
            return urlHelper.Page(page.Slug);
        }

        public static string Page(this UrlHelper urlHelper, string slug)
        {
            return urlHelper.RouteUrl("Page", new { pagePath = slug });
        }

        public static string ValidatePageInput(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("ValidatePageInput");
        }

        public static string PageAdd(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PageAddToSite");
        }

        public static string PageEdit(this UrlHelper urlHelper, Page page)
        {
            return urlHelper.PageEdit(page.Slug);
        }

        public static string PageEdit(this UrlHelper urlHelper, string slug)
        {
            return urlHelper.RouteUrl("PageEdit", new { pagePath = slug + "/Edit" });
        }

        public static string PageEditContent(this UrlHelper urlHelper, Page page)
        {
            return urlHelper.PageEditContent(page.Slug);
        }

        public static string PageEditContent(this UrlHelper urlHelper, string slug)
        {
            return urlHelper.RouteUrl("PageEditContent", new {pagePath = slug + "/EditContent"});
        }

        public static string PageRemove(this UrlHelper urlHelper, Page page)
        {
            return urlHelper.PageRemove(page.Slug);
        }

        public static string PageRemove(this UrlHelper urlHelper, string slug)
        {
            return urlHelper.RouteUrl("PageRemove", new { pagePath = slug + "/Remove" });
        }

        public static string PageContentEdit(this UrlHelper urlHelper, Page page, string name)
        {
            return urlHelper.PageContentEdit(page.Slug, name);
        }

        public static string PageContentEdit(this UrlHelper urlHelper, string slug, string name)
        {
            return string.Format("{0}#{1}", urlHelper.RouteUrl("PageEdit", new { pagePath = slug + "/Edit" }), name);
        }

        public static string PageGlobalContentEdit(this UrlHelper urlHelper, string name)
        {
            return urlHelper.RouteUrl("GlobalContentEditAnchor", new { contentItemName = name });
        }

        public static string PagesSiteMap(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PageSiteMap");
        }


    }
}
