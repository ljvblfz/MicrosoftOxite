// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.ViewModels;

namespace Oxite.Modules.Conferences.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region Gravatar

        public static string Gravatar<TModel>(this HtmlHelper<TModel> htmlHelper, ScheduleItemComment comment, string size) where TModel : OxiteViewModel
        {
            return htmlHelper.Gravatar(
                comment.CreatorEmailHash.CleanAttribute(),
                comment.CreatorName.CleanAttribute(),
                size,
                htmlHelper.ViewData.Model.Site.GravatarDefault
                );
        }

        #endregion

        public static string ScheduleItemListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
            if (pageOfAList == null || pageOfAList.TotalPageCount < 2) return "";

            StringBuilder sb = new StringBuilder(75);
            ViewContext viewContext = htmlHelper.ViewContext;

            UrlHelper urlHelper = new UrlHelper(viewContext.RequestContext);
            string queryString = viewContext.HttpContext.Request.QueryString.ToQueryString();

            sb.Append("<ul class=\"paging\">");
            if (pageOfAList.TotalPageCount <= 7)
            {
                sb.Append(getPreviousPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, previousText));
                for (int i = 0; i < pageOfAList.TotalPageCount; i++)
                    sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
                sb.Append(getNextPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, nextText));
            }
            else
            {
                sb.Append(getPreviousPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, previousText));
                sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, 0, queryString));
                sb.Append("<li>&#8230;</li>");
                if (pageOfAList.PageIndex > 3 && pageOfAList.PageIndex < pageOfAList.TotalPageCount - 4)
                {
                    for (int i = pageOfAList.PageIndex - 2; i < pageOfAList.PageIndex + 3; i++)
                        sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
                }
                else if (pageOfAList.PageIndex <= 3)
                {
                    for (int i = 1; i < 6; i++)
                        sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
                }
                else if (pageOfAList.PageIndex >= pageOfAList.TotalPageCount - 4)
                {
                    for (int i = pageOfAList.TotalPageCount - 6; i < pageOfAList.TotalPageCount - 1; i++)
                        sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
                }
                sb.Append("<li>&#8230;</li>");
                sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, pageOfAList.TotalPageCount - 1, queryString));
                sb.Append(getNextPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, nextText));
                sb.Append(getViewAllPagerButton(urlHelper, routeName, getPageRouteValueDictionary, pageOfAList.TotalPageCount - 1));
            }
            sb.Append("</ul>");

            return sb.ToString();
        }

        public static string ManageExhibitors<TModel>(this HtmlHelper<TModel> htmlHelper) 
            where TModel : OxiteViewModel
        {
            var sb = new StringBuilder();
            
            if (htmlHelper.ViewData.Model.User.IsInRole("Admin"))
            {
                sb.Append(htmlHelper.manageExhibitors());
            }
            
            return sb.ToString();
        }

        private static string manageExhibitors(this HtmlHelper htmlHelper)
        {
            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            return htmlHelper.Link("*manage", url.ManageExhibitors(), new { @class = "manageContentItem" });
        }

        private static string getPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, int index, string queryString)
        {
            return getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, index, queryString, null, null);
        }

        private static string getPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, int index, string queryString, string cssClass)
        {
            return getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, index, queryString, null, cssClass);
        }

        private static string getViewAllPagerButton(UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, int index)
        {
            // <a href="/Page1/CountAll">view all</a>
            var values = getPageRouteValueDictionary(index);
            //var url = urlHelper.RouteUrl(routeName, values);

            var path = (values["pagePath"] ?? "").ToString();
            var root = path.Length > 1
                           ? path[0].ToString().ToUpperInvariant() + path.Substring(1)
                           : path;

            var url = string.Format("<li><a href=\"/{0}/Page1/CountAll\">View All</a></li>", root);

            return url;
        }

        private static string getPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, int index, string queryString, string buttonText, string cssClass)
        {
            string pageNumberFormat = index == pageOfAList.PageIndex
                ? string.Format(
                    "<li class=\"{0}selected\">{{2}}</li>",
                    !string.IsNullOrEmpty(cssClass)
                        ? string.Format("{0} ", cssClass)
                        : ""
                    )
                : string.Format(
                    "<li{0}><a href=\"{{0}}{{1}}\">{{2}}</a></li>",
                    !string.IsNullOrEmpty(cssClass)
                        ? string.Format(" class=\"{0}\"", cssClass)
                        : ""
                    );

            var values = getPageRouteValueDictionary(index);
            var url = urlHelper.RouteUrl(routeName, values);

            return string.Format(pageNumberFormat, url, queryString, !string.IsNullOrEmpty(buttonText) ? buttonText : (index + 1).ToString());
        }

        private static string getPreviousPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string queryString, string previousText)
        {
            return getPreviousPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, previousText, null);
        }

        private static string getPreviousPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string queryString, string previousText, string cssClass)
        {
            return getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, pageOfAList.PageIndex > 0 ? pageOfAList.PageIndex - 1 : pageOfAList.PageIndex, queryString, previousText, !string.IsNullOrEmpty(cssClass) ? string.Format("prev {0}", cssClass) : "prev");
        }

        private static string getNextPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string queryString, string nextText)
        {
            return getNextPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, nextText, null);
        }

        private static string getNextPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string queryString, string nextText, string cssClass)
        {
            return getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, pageOfAList.PageIndex < pageOfAList.TotalPageCount - 1 ? pageOfAList.PageIndex + 1 : pageOfAList.PageIndex, queryString, nextText, !string.IsNullOrEmpty(cssClass) ? string.Format("next {0}", cssClass) : "next");
        }

        #region MobileScheduleItemListPager
        public static string MobileScheduleItemListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
           if (pageOfAList == null || pageOfAList.TotalPageCount < 2) return "";

           StringBuilder sb = new StringBuilder(75);
           ViewContext viewContext = htmlHelper.ViewContext;

           UrlHelper urlHelper = new UrlHelper(viewContext.RequestContext);
           string queryString = viewContext.HttpContext.Request.QueryString.ToQueryString();

           sb.Append("<ul class=\"paging\">");
           if (pageOfAList.TotalPageCount <= 7)
           {
              sb.Append(getMobilePreviousPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, previousText));
              for (int i = 0; i < pageOfAList.TotalPageCount; i++)
                 sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
              sb.Append(getMobileNextPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, nextText));
           }
           else
           {
              sb.Append(getMobilePreviousPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, previousText));
              sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, 0, queryString));
              sb.Append("<li>&#8230;</li>");
              if (pageOfAList.PageIndex > 3 && pageOfAList.PageIndex < pageOfAList.TotalPageCount - 4)
              {
                 for (int i = pageOfAList.PageIndex - 2; i < pageOfAList.PageIndex + 3; i++)
                    sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
              }
              else if (pageOfAList.PageIndex <= 3)
              {
                 for (int i = 1; i < 6; i++)
                    sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
              }
              else if (pageOfAList.PageIndex >= pageOfAList.TotalPageCount - 4)
              {
                 for (int i = pageOfAList.TotalPageCount - 6; i < pageOfAList.TotalPageCount - 1; i++)
                    sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, i, queryString));
              }
              sb.Append("<li>&#8230;</li>");
              sb.Append(getPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, pageOfAList.TotalPageCount - 1, queryString));
              sb.Append(getMobileNextPagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, queryString, nextText));
           }
           sb.Append("</ul>");

           return sb.ToString();
        }

        private static string getMobilePreviousPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string queryString, string previousText)
        {
           return getMobilePagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, pageOfAList.PageIndex > 0 ? pageOfAList.PageIndex - 1 : pageOfAList.PageIndex, queryString, previousText, "prev");
        }

        private static string getMobileNextPagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string queryString, string nextText)
        {
           return getMobilePagerButton(pageOfAList, urlHelper, routeName, getPageRouteValueDictionary, pageOfAList.PageIndex < pageOfAList.TotalPageCount - 1 ? pageOfAList.PageIndex + 1 : pageOfAList.PageIndex, queryString, nextText, "next");
        }

        private static string getMobilePagerButton<T>(IPageOfItems<T> pageOfAList, UrlHelper urlHelper, string routeName, Func<int, RouteValueDictionary> getPageRouteValueDictionary, int index, string queryString, string buttonText, string cssClass)
        {
           string pageNumberFormat = index == pageOfAList.PageIndex
               ? string.Format(
                   "<li class=\"{0}\">{{2}}</li>",
                   !string.IsNullOrEmpty(cssClass)
                       ? string.Format("{0} ", cssClass)
                       : ""
                   )
               : string.Format(
                   "<li{0}><a href=\"{{0}}{{1}}\">{{2}}</a></li>",
                   !string.IsNullOrEmpty(cssClass)
                       ? string.Format(" class=\"{0}\"", cssClass)
                       : ""
                   );

           return string.Format(pageNumberFormat, urlHelper.RouteUrl(routeName, getPageRouteValueDictionary(index)), queryString, !string.IsNullOrEmpty(buttonText) ? buttonText : (index + 1).ToString());
        }
        #endregion
    }
}
