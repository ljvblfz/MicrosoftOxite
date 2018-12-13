//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Comments.Infrastructure;
using Oxite.Modules.Comments.Services;

namespace Oxite.Modules.Comments.Extensions
{
    public static class FillCommentsExtensions
    {
        public static IPageOfItems<T> FillComments<T>(this IPageOfItems<T> items, ICommentService commentService) where T : ICommentedEntity
        {
            foreach (T item in items)
                item.FillComments(commentService);

            return items;
        }

        public static IEnumerable<T> FillComments<T>(this IEnumerable<T> items, ICommentService commentService) where T : ICommentedEntity
        {
            foreach (T item in items)
                item.FillComments(commentService);

            return items;
        }

        public static T FillComments<T>(this T item, ICommentService commentService) where T : ICommentedEntity
        {

            commentService.FillComments(item);

            return item;
        }
    }
}
