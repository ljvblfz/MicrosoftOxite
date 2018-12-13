//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;

namespace Oxite.Modules.Bing.Services
{
    public static class SearchProvider
    {
        public static SearchResults Search(bingSearchCriteria criteria, PagingInfo pagingInfo, string apiKey)
        {
            Communication c = new Communication(apiKey);

            return c.DoQuery(criteria, pagingInfo.Size, pagingInfo.Index);
        }
    }
}
