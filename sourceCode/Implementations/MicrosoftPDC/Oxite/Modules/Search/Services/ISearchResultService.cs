//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;
using Oxite.Modules.Search.Models;
using Oxite.Services;

namespace Oxite.Modules.Search.Services
{
    public interface ISearchResultService
    {
        IPageOfItems<ISearchResult> GetSearchResults(int pageIndex, int pageSize, SearchCriteria criteria);
    }
}
