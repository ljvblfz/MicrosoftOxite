//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Extensions
{
    public static class PagingInfoExtensions
    {
        public static CachePartition ToCachePartition(this PagingInfo pagingInfo)
        {
            return new CachePartition(pagingInfo.Index, pagingInfo.Size);
        }
    }
}