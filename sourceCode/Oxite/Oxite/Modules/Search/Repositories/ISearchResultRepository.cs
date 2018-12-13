//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Search.Models;

namespace Oxite.Modules.Search.Repositories
{
    public interface ISearchResultRepository
    {
        IPageOfItems<ISearchResult> GetSearchResults(PagingInfo pagingInfo, SearchCriteria criteria);
    }
}
