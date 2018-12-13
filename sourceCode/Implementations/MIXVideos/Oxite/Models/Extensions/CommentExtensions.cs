//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Routing;

namespace Oxite.Models.Extensions
{
    public static class CommentExtensions
    {
        //todo: (nheskew) need something a little more meaningful than just a timestamp. "comment-200901261012536" would be better but should 
        // be localized (text and format) - something that cannot be currently done w/out controller context :-/
        public static string GetSlug(this Comment comment)
        {
            return comment.Created != null 
                ? string.Format("c-{0}", ((DateTime)comment.Created).ToString("yyyyMMddhhmmssf"))
                : string.Empty;
        }
    }
}
