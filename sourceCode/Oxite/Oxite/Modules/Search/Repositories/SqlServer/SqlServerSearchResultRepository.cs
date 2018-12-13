//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Search.Models;
using System.Collections.Generic;

namespace Oxite.Modules.Search.Repositories.SqlServer
{
    public class SqlServerSearchResultRepository : ISearchResultRepository
    {
        private readonly OxiteSearchDataContext context;
        private readonly Guid siteID;

        public SqlServerSearchResultRepository(OxiteSearchDataContext context, OxiteContext oxiteContext)
        {
            this.context = context;
            siteID = oxiteContext.Site.ID;
        }

        #region ISearchResultRepository Members

        public IPageOfItems<ISearchResult> GetSearchResults(PagingInfo pagingInfo, SearchCriteria criteria)
        {
            return (
                from sr in context.oxite_SearchResults
                where sr.SearchResultIndex.Contains(criteria.Term)
                select (ISearchResult)new SearchResult(sr.SearchResultID, sr.Title, sr.Body, sr.Url, new Dictionary<string, object>())
                )
                .GetPage(pagingInfo);
        }

        #endregion
    }
}
