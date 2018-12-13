//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Text.RegularExpressions;

namespace Oxite.Extensions
{
    public static class StringExtensions
    {
        public static string CleanHtmlTags(this string s)
        {
            Regex exp = new Regex(
                "<[^<>]*>",
                RegexOptions.Compiled
                );

            return exp.Replace(s, "");
        }

        public static string CleanWhitespace(this string s)
        {
            return new Regex(@"\s+", RegexOptions.Compiled | RegexOptions.Singleline).Replace(s, " ");
        }
    }
}