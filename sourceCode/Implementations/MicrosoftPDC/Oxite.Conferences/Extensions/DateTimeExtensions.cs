// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Modules.Conferences.Models;
using System;

namespace Oxite.Modules.Conferences.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToiCalendarFormat(this DateTime dateTime)
        {
            return dateTime.AddHours(3).ToUniversalTime().ToString("yyyyMMddTHHmmssZ");
        }

    }
}
