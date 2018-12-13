// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web.Mvc;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string AddComment(this UrlHelper urlHelper, ScheduleItemSmall scheduleItemSmall)
        {
            return urlHelper.RouteUrl("AddCommentToScheduleItem", new { /*eventName = scheduleItemSmall.EventName, */scheduleItemSlug = scheduleItemSmall.Slug });
        }

        public static string AddComment(this UrlHelper urlHelper, ScheduleItem scheduleItem)
        {
            return urlHelper.RouteUrl("AddCommentToScheduleItem", new { /*eventName = scheduleItem.Event.Name, */scheduleItemSlug = scheduleItem.Slug });
        }

        public static string Comment(this UrlHelper urlHelper, ScheduleItemComment comment)
        {
            return urlHelper.RouteUrl("PDC09SessionCommentPermalink", new { /*eventName = comment.ScheduleItem.EventName, */scheduleItemSlug = comment.ScheduleItem.Slug, commentSlug = comment.Slug });
        }

        public static string CommentPending(this UrlHelper urlHelper, ScheduleItemComment comment)
        {
            //todo: (nheskew) really want ScheduleItemCommentForm w/ a query string inserted but that's not going to happen in the near term so hacking together the URL
            return string.Format("{0}#comment", urlHelper.RouteUrl("ScheduleItem", new { /*eventName = comment.ScheduleItem.EventName, */scheduleItemSlug = comment.ScheduleItem.Slug, pending = bool.TrueString }));
        }

        public static string RemoveComment(this UrlHelper urlHelper, ScheduleItemComment comment)
        {
            return urlHelper.RouteUrl("RemoveScheduleItemComment", new { /*eventName = comment.ScheduleItem.EventName, */scheduleItemSlug = comment.ScheduleItem.Slug, commentSlug = comment.Slug });
        }

        public static string ApproveComment(this UrlHelper urlHelper, ScheduleItemComment comment)
        {
            return urlHelper.RouteUrl("ApproveScheduleItemComment", new { /*eventName = comment.ScheduleItem.EventName, */scheduleItemSlug = comment.ScheduleItem.Slug, commentSlug = comment.Slug });
        }

        public static string ScheduleItems(this UrlHelper urlHelper, string dataFormat)
        {
            return urlHelper.RouteUrl("ScheduleItems", new { dataFormat });
        }
    }
}
