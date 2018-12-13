// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Conferences.Extensions;
using Oxite.Modules.Conferences.Models;
using Oxite.ViewModels;

namespace OxiteSite.App_Code.Modules.OxiteSite.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string SessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return htmlHelper.SessionListPager(pageOfAList, i => getSessionPageRouteValueDictionary(htmlHelper.ViewContext.RouteData, i), localize("Pager.Previous", "Previous"), localize("Pager.Next", "Next"));
        }

        public static string SessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
            return htmlHelper.ScheduleItemListPager(pageOfAList, "PDC09Sessions", getPageRouteValueDictionary, previousText, nextText);
        }

        public static string SessionListByTagPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return htmlHelper.SessionListByTagPager(pageOfAList, i => getSessionPageRouteValueDictionary(htmlHelper.ViewContext.RouteData, i), localize("Pager.Previous", "Previous"), localize("Pager.Next", "Next"));
        }

        public static string SessionListByTagPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
            return htmlHelper.ScheduleItemListPager(pageOfAList, "PDC09SessionsByTag", getPageRouteValueDictionary, previousText, nextText);
        }

        public static string MySessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return htmlHelper.MySessionListPager(pageOfAList, i => getMySessionPageRouteValueDictionary(htmlHelper.ViewContext.RouteData, i), localize("Pager.Previous", "Previous"), localize("Pager.Next", "Next"));
        }

        public static string MySessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
            return htmlHelper.ScheduleItemListPager(pageOfAList, "MyPDC09Sessions", getPageRouteValueDictionary, previousText, nextText);
        }

        private static RouteValueDictionary getMySessionPageRouteValueDictionary(RouteData routeData, int pageIndex)
        {
            return getSessionPageRouteValueDictionary(routeData, pageIndex);
        }

        private static RouteValueDictionary getSessionPageRouteValueDictionary(RouteData routeData, int pageIndex)
        {
            var rvd = new RouteValueDictionary();

            foreach (KeyValuePair<string, object> item in routeData.Values)
            {
                if (item.Key != "X-Requested-With")
                    rvd.Add(item.Key, item.Value);
            }

            rvd.Remove("controller");
            rvd.Remove("action");
            rvd.Remove("eventname");
            rvd.Remove("scheduleItemType");
            rvd.Remove("dataFormat");

            var criteria = new ScheduleItemFilterCriteria(routeData.Values["scheduleItemFilterCriteria"] as string)
                               {
                                   PageIndex = pageIndex, //ForUser = forUser
                               };

            rvd["scheduleItemFilterCriteria"] = criteria.ToUrl();
            return rvd;
        }

        public static string SpeakerListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return htmlHelper.SpeakerListPager(pageOfAList, i => getSpeakerPageRouteValueDictionary(htmlHelper.ViewContext.RouteData, i), localize("Pager.Previous", "Previous"), localize("Pager.Next", "Next"));
        }

        public static string SpeakerListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
            return htmlHelper.ScheduleItemListPager(pageOfAList, "PDC09Speakers", getPageRouteValueDictionary, previousText, nextText);
        }

        private static RouteValueDictionary getSpeakerPageRouteValueDictionary(RouteData routeData, int pageIndex)
        {
            RouteValueDictionary rvd = new RouteValueDictionary();

            foreach (KeyValuePair<string, object> item in routeData.Values)
            {
                if (item.Key != "X-Requested-With")
                    rvd.Add(item.Key, item.Value);
            }

            rvd.Remove("controller");
            rvd.Remove("action");
            rvd.Remove("eventname");
            rvd.Remove("scheduleItemType");

            rvd["speakerFilterCriteria"] =
                (new ScheduleItemFilterCriteria(routeData.Values["speakerFilterCriteria"] as string)
                {
                    PageIndex = pageIndex
                }).ToUrl();

            return rvd;
        }

        public static void RenderPlayer<TModel>(this HtmlHelper<TModel> htmlHelper, string viewName, IEnumerable<File> files, string bug, string canonicalURL) where TModel : OxiteViewModel
        {
            
            List<File> fileList = files.ToList();
            string message = "<p class=\"streamingMessage\"><em>This streaming file is very high quality and may be difficult to view in the Silverlight player, a download version is available below</em></p>";
            if (fileList.Count() > 0)
            {
                File preview = fileList.Where(f => f.TypeName == "Preview Image (Large)").FirstOrDefault();
                File media = null;
                RequestContext requestContext = htmlHelper.ViewContext.RequestContext;
                File wmv, wmvHigh, smooth;


                if (preview == null)
                {
                    UrlHelper urlHelper = new UrlHelper(requestContext);

                    preview = new File(Guid.Empty, null, null, new Uri(urlHelper.CssPath("/images/DefaultPlayerBackground.png", htmlHelper.ViewContext), UriKind.Relative), 0);
                }

                wmv = fileList.Where(f => f.TypeName == "WMV").FirstOrDefault();
                wmvHigh = fileList.Where(f => f.TypeName == "WMVHigh").FirstOrDefault();
                smooth = fileList.Where(f => f.TypeName == "Smooth").FirstOrDefault();

                string requestedMediaType = requestContext.HttpContext.Request.QueryString["type"];
                if (!String.IsNullOrEmpty(requestedMediaType))
                {
                    media = fileList.Where(f => f.TypeName.Equals(requestedMediaType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                }

                if (media == null)
                    media = smooth;

                if (media != null)
                {
                    message = "";
                }

                if (media == null)
                    media = fileList.Where(f => f.TypeName == "WMVStreamingOnly").FirstOrDefault();

                if (media == null)
                    media = wmvHigh;

                if (media == null)
                    media = fileList.Where(f => f.TypeName == "WMVStreaming").FirstOrDefault();

                if (media == null)
                    media = fileList.Where(f => f.TypeName == "WMV640x360").FirstOrDefault();

                if (media == null)
                    media = wmv;

                if (media != null)
                {
                    string options = "";
                    string currentStream;

                    message = "<p class='typeChoice'>You are currently watching the {0} video. {1}</p>";

                    switch (media.TypeName)
                    {
                        case "Smooth":
                            currentStream = "Smooth Streaming (up to 1280x720)";
                            break;

                        case "WMVHigh":
                            currentStream = "High Quality WMV (1280x720)";
                            break;

                        case "WMV":
                            currentStream = "WMV (640x360)";
                            break;

                        default:
                            currentStream = "WMV";
                            break;
                    }

                    if (media.TypeName == "Smooth")
                    {
                      if (wmv != null || wmvHigh != null)
                      {
                          options = "Click to watch ";
                          if (wmv != null)
                          {
                              options += string.Format("the <a href=\"{0}\">WMV (640x360)</a> video ", canonicalURL + "?type=wmv");
                          }

                          if (wmvHigh != null)
                          {
                              if (wmv != null)
                                  options += "or ";

                              options += string.Format("the <a href=\"{0}\">High Quality WMV (1280x720, not smooth streaming)</a> video ", canonicalURL + "?type=wmvhigh");
                          }
                      }
                      
                    }

                    if (media.TypeName == "WMV")
                    {
                        if (smooth != null || wmvHigh != null)
                        {
                            options = "click to watch ";
                            if (smooth != null)
                            {
                                options += string.Format("the <a href=\"{0}\">Smooth Streaming (1280x720)</a> video ", canonicalURL + "?type=smooth");
                            }

                            if (wmvHigh != null)
                            {
                                if (smooth != null)
                                    options += "or ";

                                options += string.Format("the <a href=\"{0}\">High Quality WMV (1280x720, not smooth streaming)</a> video ", canonicalURL + "?type=wmvhigh");
                            }
                        }
                        
                    }

                    if (media.TypeName == "WMVHigh")
                    {
                        if (smooth != null || wmv != null)
                        {
                            options = "click to watch ";
                            if (smooth != null)
                            {
                                options += string.Format("the <a href=\"{0}\">Smooth Streaming (1280x720)</a> video ", canonicalURL + "?type=smooth");
                            }

                            if (wmv != null)
                            {
                                if (smooth != null)
                                    options += "or ";

                                options += string.Format("the <a href=\"{0}\">WMV (640x360)</a> video ", canonicalURL + "?type=wmv");
                            }
                        }

                    }

                    message = options == "" ? "" : string.Format(message, currentStream, options);
                    
                    htmlHelper.RenderPartialFromSkin(viewName,
                                                     new OxiteViewModelPartial<PlayerViewModel>(
                                                         htmlHelper.ViewData.Model, new PlayerViewModel(media, preview, message, bug)),
                                                     htmlHelper.ViewData);

                    
                }

            }
        }

        #region MobileSessionListPager
        public static string MobileSessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
           return htmlHelper.MobileSessionListPager(pageOfAList, i => getSessionPageRouteValueDictionary(htmlHelper.ViewContext.RouteData, i), localize("Pager.Previous", "Previous"), localize("Pager.Next", "Next"));
        }

        public static string MobileSessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
           return htmlHelper.MobileScheduleItemListPager(pageOfAList, "PDC09Sessions", getPageRouteValueDictionary, previousText, nextText);
        }

        public static string MyMobileSessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return htmlHelper.MyMobileSessionListPager(pageOfAList, i => getSessionPageRouteValueDictionary(htmlHelper.ViewContext.RouteData, i), localize("Pager.Previous", "Previous"), localize("Pager.Next", "Next"));
        }

        public static string MyMobileSessionListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
            return htmlHelper.MobileScheduleItemListPager(pageOfAList, "MyPDC09Sessions", getPageRouteValueDictionary, previousText, nextText);
        }

        public static string MobileSpeakerListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
           return htmlHelper.MobileSpeakerListPager(pageOfAList, i => getSpeakerPageRouteValueDictionary(htmlHelper.ViewContext.RouteData, i), localize("Pager.Previous", "Previous"), localize("Pager.Next", "Next"));
        }

        public static string MobileSpeakerListPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<int, RouteValueDictionary> getPageRouteValueDictionary, string previousText, string nextText)
        {
           return htmlHelper.MobileScheduleItemListPager(pageOfAList, "PDC09Speakers", getPageRouteValueDictionary, previousText, nextText);
        }
        #endregion
    }
}
