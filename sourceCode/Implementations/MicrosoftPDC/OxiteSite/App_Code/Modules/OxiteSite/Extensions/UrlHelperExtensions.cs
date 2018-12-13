// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Tags.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.Extensions
{
    public static class UrlHelperExtensions
    {
        #region Comments

        public static string Comment(this UrlHelper urlHelper, ScheduleItemComment comment)
        {
            return urlHelper.RouteUrl("PDC09SessionCommentPermalink", new { scheduleItemSlug = comment.ScheduleItem.Slug, commentSlug = comment.Slug });
        }

        #endregion

        #region Pages

        public static string About(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Page", new { pagePath = "About" });
        }

        public static string Maps(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Page", new { pagePath = "Maps" });
        }

        #endregion

        #region Schedules

        public static string Schedule(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Schedule");
        }

        public static string ScheduleForMonday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Schedule", new { dayName = "Monday" });
        }

        public static string MyScheduleForMonday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("MySchedule", new { dayName = "Monday" });
        }

        public static string ScheduleForTuesday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Schedule", new { dayName = "Tuesday" });
        }

        public static string MyScheduleForTuesday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("MySchedule", new { dayName = "Tuesday" });
        }

        public static string ScheduleForWednesday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Schedule", new { dayName = "Wednesday" });
        }

        public static string MyScheduleForWednesday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("MySchedule", new { dayName = "Wednesday" });
        }

        public static string ScheduleForThursday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Schedule", new { dayName = "Thursday" });
        }

        public static string MyScheduleForThursday(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("MySchedule", new { dayName = "Thursday" });
        }

        #endregion

        #region Sessions

        public static string Session(this UrlHelper urlHelper, ScheduleItem scheduleItem)
        {
            return urlHelper.RouteUrl("PDC09Session", new { scheduleItemSlug = scheduleItem.Slug });
        }

        public static string Sessions(this UrlHelper urlHelper, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return urlHelper.RouteUrl("PDC09SessionsUrl", new { scheduleItemFilterCriteria = scheduleItemFilterCriteria.ToUrl() });
        }

        public static string Sessions(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("AllPDC09Sessions", new { dataFormat = "" });
        }

        public static string Sessions(this UrlHelper urlHelper, string dataFormat)
        {
            return urlHelper.RouteUrl("AllPDC09Sessions", new { dataFormat });
        }

        public static string Sessions(this UrlHelper urlHelper, Tag tag)
        {
            return urlHelper.RouteUrl("AllPDC09SessionsByTag", new { tagName = tag.Name });
        }

        public static string MySessions(this UrlHelper urlHelper, Tag tag)
        {
            return urlHelper.RouteUrl("MyPDC09SessionsByTag", new { tagName = tag.Name });
        }

        public static string Sessions(this UrlHelper urlHelper, string dataFormat, string fileFormat)
        {
            return urlHelper.RouteUrl("AllPDC09SessionsByFileFormat", new { dataFormat, fileFormat });
        }



        #endregion

        #region Speakers

        public static string Speaker(this UrlHelper urlHelper, Speaker speaker)
        {
            return urlHelper.RouteUrl("PDC09Speaker", new { speakerName = speaker.Name });
        }

        public static string Tag(this UrlHelper urlHelper, Tag tag)
        {
            return urlHelper.RouteUrl("PDC09SessionsByTag", new { tagName = tag.DisplayName });
        }

        public static string SpeakerImage(this UrlHelper urlHelper, Speaker speaker, string type)
        {
            string result = "";
            string cacheKey = "speakerimage:" +  type + ":" + speaker.Name;
            result = urlHelper.RequestContext.HttpContext.Cache.Get(cacheKey) as string;

            if (string.IsNullOrEmpty(result))
            {
                string imagePath = string.Format("~/Content/images/speakers/{1}/{0}.jpg", speaker.Name, type);
                string filePath = urlHelper.RequestContext.HttpContext.Server.MapPath(imagePath);
                result = System.IO.File.Exists(filePath) ? imagePath : "";
                urlHelper.RequestContext.HttpContext.Cache.Add(cacheKey, result, null, DateTime.Now.AddHours(1),
                                                               Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }
            if (result == "")
                result = null;

            return result;
        }

        public static string SponsorImage(this UrlHelper urlHelper, Exhibitor sponsor)
        {
            var slug = sponsor.Name.CleanSlug();
            var cacheKey = "sponsorimage:" + ":" + slug;
            var result = urlHelper.RequestContext.HttpContext.Cache.Get(cacheKey) as string;

            if (string.IsNullOrEmpty(result))
            {
                foreach(var ext in new[]{"jpg", "gif", "png"})
                {
                    var imagePath = string.Format("~/Content/images/sponsors/{0}.{1}", slug, ext);
                    var filePath = urlHelper.RequestContext.HttpContext.Server.MapPath(imagePath);
                    var exists = System.IO.File.Exists(filePath);
                    if (!exists)
                    {
                        continue;
                    }

                    result = VirtualPathUtility.ToAbsolute(imagePath);
                    urlHelper.RequestContext.HttpContext.Cache.Add(cacheKey, result, null, DateTime.Now.AddHours(1),
                                                                   Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);

                    break;
                }
            }
            if (result == "")
                result = null;

            return result;
        }


        #endregion
    }
}
