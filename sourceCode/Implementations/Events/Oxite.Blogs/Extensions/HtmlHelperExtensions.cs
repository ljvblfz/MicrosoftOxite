// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Models.Extensions;
using Oxite.ViewModels;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region Excerpt

        public static string Excerpt(this HtmlHelper htmlHelper, Post post, Func<string, string, string> localize)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string excerpt = post.GetBodyShort();

            excerpt = excerpt.EndsWith("</p>", StringComparison.OrdinalIgnoreCase)
                ? excerpt.Substring(0, excerpt.Length - "</p>".Length)
                : string.Format("<p>{0}", excerpt);

            return string.Format("{0}&nbsp;{1}</p>", excerpt, htmlHelper.Link(localize("Post.More", "More&#0187;"), urlHelper.Post(post)));
        }

        public static string ExcerptWithoutLink(this HtmlHelper htmlHelper, Post post, Func<string, string, string> localize)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            string excerpt = post.GetBodyShort();

            excerpt = excerpt.EndsWith("</p>", StringComparison.OrdinalIgnoreCase)
                ? excerpt.Substring(0, excerpt.Length - "</p>".Length)
                : string.Format("<p>{0}", excerpt);

            return string.Format("{0}&nbsp</p>", excerpt);
        }


        #endregion

        #region GenerateRsd

        public static void RenderRsd(this HtmlHelper htmlHelper)
        {
            htmlHelper.RenderRsd((Blog)null);
        }

        public static void RenderRsd(this HtmlHelper htmlHelper, Blog blog)
        {
            if (blog != null)
            {
                htmlHelper.RenderRsd(blog.Name);
            }
            else
            {
                htmlHelper.RenderRsd((string)null);
            }
        }

        public static void RenderRsd(this HtmlHelper htmlHelper, string blogName)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            htmlHelper.ViewContext.HttpContext.Response.Write(
                htmlHelper.HeadLink(
                    "EditURI",
                    urlHelper.AbsolutePath(urlHelper.Rsd(blogName)),
                    "application/rsd+xml",
                    "RSD"
                    )
                );
        }

        #endregion

        #region Gravatar

        public static string Gravatar<TModel>(this HtmlHelper<TModel> htmlHelper, Post post, string size) where TModel : OxiteViewModel
        {
            return htmlHelper.Gravatar(
                post.Creator.EmailHash.CleanAttribute(),
                post.Creator.DisplayName.CleanAttribute(),
                size,
                htmlHelper.ViewData.Model.Site.GravatarDefault
                );
        }

        public static string Gravatar<TModel>(this HtmlHelper<TModel> htmlHelper, OxiteViewModel model, PostComment comment, string size) where TModel : OxiteViewModel
        {
            return htmlHelper.Gravatar(
                comment.CreatorEmailHash.CleanAttribute(),
                comment.CreatorName.CleanAttribute(),
                size,
                htmlHelper.ViewData.Model.Site.GravatarDefault
                );
        }

        #endregion

        #region Pager

        public static string PostListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return PostListPager(htmlHelper, pageOfAList, localize, null, localize("NewerPager", "&laquo; Newer"), localize("OlderPager", "Older &raquo;"), false);
        }

        public static string PostListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            return htmlHelper.SimplePager(pageOfAList, "PageOfPosts", values, previousText, nextText, alwaysShowPreviousAndNext);
        }

        public static string PostListByBlogPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, string blogName)
        {
            return PostListByBlogPager(htmlHelper, pageOfAList, localize, new { blogName }, localize("NewerPager", "&laquo; Newer"), localize("OlderPager", "Older &raquo;"), false);
        }

        public static string PostListByBlogPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            return htmlHelper.SimplePager(pageOfAList, "PageOfPostsByBlog", values, previousText, nextText, alwaysShowPreviousAndNext);
        }

        public static string PostListByTagPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, string tagName)
        {
            return PostListByTagPager(htmlHelper, pageOfAList, localize, new { tagName }, localize("NewerPager", "&laquo; Newer"), localize("OlderPager", "Older &raquo;"), false);
        }

        public static string PostListByTagPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            return htmlHelper.SimplePager(pageOfAList, "PageOfPostsByTag", values, previousText, nextText, alwaysShowPreviousAndNext);
        }

        public static string CommentListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return CommentListPager(htmlHelper, pageOfAList, localize, null, localize("NewerPager", "&laquo; Newer"), localize("OlderPager", "Older &raquo;"), false);
        }

        public static string CommentListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            return htmlHelper.SimplePager(pageOfAList, "PageOfAllComments", values, previousText, nextText, alwaysShowPreviousAndNext);
        }

        public static string PostArchiveListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return PostArchiveListPager(htmlHelper, pageOfAList, localize, null, localize("NewerPager", "&laquo; Newer"), localize("OlderPager", "Older &raquo;"), false);
        }

        public static string PostArchiveListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            return SimpleArchivePager(htmlHelper, pageOfAList, "PostsByArchive", values, previousText, nextText, alwaysShowPreviousAndNext);
        }

        public static string SimpleArchivePager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, string routeName, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            if (pageOfAList.TotalPageCount < 2) return "";

            StringBuilder sb = new StringBuilder(50);
            ViewContext viewContext = htmlHelper.ViewContext;
            RouteValueDictionary rvd = new RouteValueDictionary();

            foreach (KeyValuePair<string, object> item in viewContext.RouteData.Values)
            {
                rvd.Add(item.Key, item.Value);
            }

            UrlHelper urlHelper = new UrlHelper(viewContext.RequestContext);

            rvd.Remove("controller");
            rvd.Remove("action");
            rvd.Remove("id");

            if (values != null)
            {
                RouteValueDictionary rvd2 = new RouteValueDictionary(values);

                foreach (KeyValuePair<string, object> item in rvd2)
                {
                    rvd[item.Key] = item.Value;
                }
            }

            ArchiveData archiveData = new ArchiveData(rvd["archiveData"] as string);

            sb.Append("<div class=\"pager\">");

            if (pageOfAList.PageIndex < pageOfAList.TotalPageCount - 1 || alwaysShowPreviousAndNext)
            {
                archiveData.Page = pageOfAList.PageIndex + 2;
                rvd["archiveData"] = archiveData.ToString();

                sb.AppendFormat("<a href=\"{1}{2}\" class=\"next\">{0}</a>", nextText,
                                urlHelper.RouteUrl(routeName, rvd),
                                viewContext.HttpContext.Request.QueryString.ToQueryString());
            }

            if (pageOfAList.PageIndex > 0 || alwaysShowPreviousAndNext)
            {
                archiveData.Page = pageOfAList.PageIndex;
                rvd["archiveData"] = archiveData.ToString();

                sb.AppendFormat("<a href=\"{1}{2}\" class=\"previous\">{0}</a>", previousText,
                                urlHelper.RouteUrl(routeName, rvd),
                                viewContext.HttpContext.Request.QueryString.ToQueryString());
            }

            sb.Append("</div>");

            return sb.ToString();
        }

        #endregion

        #region PingbackDiscovery

        public static string PingbackDiscovery(this HtmlHelper htmlHelper, Post post)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return htmlHelper.HeadLink("pingback", urlHelper.AbsolutePath(urlHelper.Pingback(post)), "", "");
        }

        #endregion

        #region Published

        public static string Published<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModelItem<Post>
        {
            return htmlHelper.Published(htmlHelper.ViewData.Model.Item);
        }

        public static string Published<TModel>(this HtmlHelper<TModel> htmlHelper, Post post) where TModel : OxiteViewModel
        {
            OxiteViewModel model = htmlHelper.ViewData.Model;

            if (post.State == EntityState.Removed)
                return model.Localize("Removed");

            if (post.Published.HasValue)
                return htmlHelper.ConvertToLocalTime(post.Published.Value, htmlHelper.ViewData.Model).ToString("MMM d, yyyy");

            return model.Localize("Draft");
        }

        #endregion

        #region RenderLiveWriterManifest

        public static void RenderLiveWriterManifest(this HtmlHelper htmlHelper)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            htmlHelper.RenderLiveWriterManifest(urlHelper.LiveWriterManifest());
        }

        public static void RenderLiveWriterManifest(this HtmlHelper htmlHelper, string path)
        {
            htmlHelper.ViewContext.HttpContext.Response.Write(
                htmlHelper.HeadLink(
                    "wlwmanifest",
                    path,
                    "application/wlwmanifest+xml",
                    ""
                    )
                );
        }

        #endregion

        #region TrackbackBlock

        public static string TrackbackBlock<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : OxiteViewModel
        {
            OxiteViewModelItem<Post> model = htmlHelper.ViewData.Model as OxiteViewModelItem<Post>;

            if (model != null)
            {
                Post post = model.Item;

                if (post != null)
                {
                    UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                    return string.Format("<!--<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:trackback=\"http://madskills.com/public/xml/rss/module/trackback/\"><rdf:Description rdf:about=\"{2}\" dc:identifier=\"{2}\" dc:title=\"{0}\" trackback:ping=\"{1}\" /></rdf:RDF>-->", htmlHelper.Encode(post.Title), urlHelper.AbsolutePath(urlHelper.Trackback(post)), urlHelper.AbsolutePath(urlHelper.Post(post)));
                }
            }

            return "";
        }

        #endregion
    }
}
