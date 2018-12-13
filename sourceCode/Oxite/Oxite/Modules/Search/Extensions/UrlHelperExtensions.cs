//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;

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
            return urlHelper.Search(term, 1);
        }

        public static string Search(this UrlHelper urlHelper, string term, int pageNumber)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(new { term });

            if (pageNumber > 1)
                routeValueDictionary["pageNumber"] = string.Format("Page{0}", pageNumber);

            return urlHelper.RouteUrl("PostsBySearch", routeValueDictionary);
        }

        public static string Search(this UrlHelper urlHelper, string dataFormat, string term)
        {
            return urlHelper.RouteUrl("PostsBySearch", new { dataFormat, term });
        }
    }
}
