//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;

namespace Oxite.Modules.Search.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string SearchResultPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize)
        {
            return SearchResultPager(htmlHelper, pageOfAList, localize, null, localize("NewerPager", "&laquo; Newer"), localize("OlderPager", "Older &raquo;"), false);
        }

        public static string SearchResultPager<T>(this HtmlHelper htmlHelper, IPageOfItems<T> pageOfAList, Func<string, string, string> localize, object values, string previousText, string nextText, bool alwaysShowPreviousAndNext)
        {
            return htmlHelper.SimplePager(pageOfAList, "PageOfPostsBySearch", values, previousText, nextText, alwaysShowPreviousAndNext);
        }
    }
}
