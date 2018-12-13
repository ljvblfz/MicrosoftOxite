//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToStringForEdit(this DateTime dateTime)
        {
            return string.Format("{0} {1}", dateTime.ToShortDateString(), dateTime.ToShortTimeString());
        }

        public static string ToStringForFeed(this DateTime dateTime)
        {
            return dateTime.ToString("R");
        }

        public static string ToRelativeDateTime(this DateTime dateTime)
        {
            TimeSpan time = DateTime.UtcNow - dateTime;

            if (time.TotalDays > 7)
                return dateTime.ToString("MMM d yyyy");
            if (time.TotalHours > 24)
                return string.Format("{0} day{1} ago", time.Days, time.Days == 1 ? "" : "s");
            if (time.TotalMinutes > 60)
                return string.Format("{0} hour{1} ago", time.Hours, time.Hours == 1 ? "" : "s");
            if (time.TotalSeconds > 60)
                return string.Format("{0} minute{1} ago", time.Minutes, time.Minutes == 1 ? "" : "s");
            else if (time.TotalSeconds > 10)
                return string.Format("{0} second{1} ago", time.Seconds, time.Seconds == 1 ? "" : "s");
            else
                return "a moment ago";
        }
    }
}
