//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Oxite.Models;
using Oxite.Extensions;
using Oxite.ViewModels;

namespace MIXVideos.Oxite.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static void RenderPlayer<TModel>(this HtmlHelper<TModel> htmlHelper, string viewName) where TModel : OxiteModelItem<Post>
        {
            htmlHelper.RenderPlayer(viewName, "");
        }

        public static void RenderPlayer<TModel>(this HtmlHelper<TModel> htmlHelper, string viewName, string advertisementUrl) where TModel : OxiteModelItem<Post>
        {
            OxiteModelItem<Post> model = htmlHelper.ViewData.Model;

            if (model.Item.Files.Count > 0)
            {
                File preview = model.Item.Files.Where(f => f.TypeName == "Preview Image (Large)").FirstOrDefault();
                File media = null;

                if (preview == null)
                {
                    UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

                    preview = new File() { Url = new Uri(urlHelper.CssPath("/images/DefaultPlayerBackground.png", htmlHelper.ViewContext), UriKind.Relative) };
                }

                if (string.Compare(model.Container.Name, "IE8", true) == 0)
                {
                    media = model.Item.Files.Where(f => f.TypeName == "WMVStreaming").FirstOrDefault();
                }
                else
                {
                    if (media == null)
                        media = model.Item.Files.Where(f => f.TypeName == "WMVStreamingOnly").FirstOrDefault();

                    if (media == null)
                        media = model.Item.Files.Where(f => f.TypeName == "WMVHigh").FirstOrDefault();

                    if (media == null)
                        media = model.Item.Files.Where(f => f.TypeName == "WMVStreaming").FirstOrDefault();

                    if (media == null)
                        media = model.Item.Files.Where(f => f.TypeName == "WMV640x360").FirstOrDefault();

                    if (media == null)
                        media = model.Item.Files.Where(f => f.TypeName == "WMV").FirstOrDefault();
                }

                if (media != null)
                    htmlHelper.RenderPartialFromSkin(viewName, new OxiteModelPartial<PlayerViewModel>(model, new PlayerViewModel(media, preview, advertisementUrl)), htmlHelper.ViewData);
            }
        }

        public static string PostViewBug(this HtmlHelper htmlHelper, Post post, string viewType)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return string.Format("<img src=\"{0}\" width=\"1\" height=\"1\" />", urlHelper.PostViewBug(post, viewType));
        }

        public static string Thumbnail(this HtmlHelper htmlHelper, Post post, Func<string, string, string> localize)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            string defaultUrl = urlHelper.CssPath("images/thumbnail_404.png", htmlHelper.ViewContext);

            return htmlHelper.Thumbnail(post, localize, defaultUrl);               
        }

        public static string Thumbnail(this HtmlHelper htmlHelper, Post post, Func<string, string, string> localize, string defaultUrl)
        {
            if (post.Files.Count > 0)
            {
                File thumbnail = post.Files.Where(f => f.TypeName == "Preview Image (Medium)").FirstOrDefault();

                if (thumbnail != null)
                    return string.Format("<img src=\"{0}\" alt=\"{1}\" />", thumbnail.Url.ToString(), string.Format(localize("ThumbnailAltFormat", "Thumbnail for {0}"), post.Title));
            }

            return string.Format("<img src=\"{0}\" alt=\"{1}\" />", defaultUrl, string.Format(localize("ThumbnailAltFormat", "Thumbnail for {0}"), post.Title.CleanAttribute()));
        }
    }
}
