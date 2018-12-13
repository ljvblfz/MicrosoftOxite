//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Modules.Search.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Search(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PostsBySearch");
        }

        public static string Search(this UrlHelper urlHelper, string term)
        {
            return urlHelper.RouteUrl("PostsBySearch", new { term });
        }

        public static string Search(this UrlHelper urlHelper, string dataFormat, string term)
        {
            return urlHelper.RouteUrl("PostsBySearch", new { dataFormat = dataFormat, term = term });
        }
    }
}
