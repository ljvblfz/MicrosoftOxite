//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Extensions
{
    public static class IQueryableExtensions
    {
        public static IPageOfItems<T> GetPage<T>(this IQueryable<T> query, PagingInfo pagingInfo)
        {
            return new PageOfItems<T>(query.Skip(pagingInfo.Index * pagingInfo.Size).Take(pagingInfo.Size), pagingInfo.Index, pagingInfo.Size, query.Count());
        }
    }
}