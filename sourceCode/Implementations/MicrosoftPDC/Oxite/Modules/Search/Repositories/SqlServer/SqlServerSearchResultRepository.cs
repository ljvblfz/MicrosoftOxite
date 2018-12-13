//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Modules.Search.Models;
using System.Collections.Generic;

namespace Oxite.Modules.Search.Repositories.SqlServer
{
    public class SqlServerSearchResultRepository : ISearchResultRepository
    {
        private readonly OxiteSearchDataContext context;

        public SqlServerSearchResultRepository(OxiteSearchDataContext context)
        {
            this.context = context;
        }

        #region ISearchResultRepository Members

        public IQueryable<ISearchResult> GetSearchResults(Guid siteID, SearchCriteria criteria)
        {
            return (
                from sr in context.oxite_SearchResults
                where sr.SearchResultIndex.Contains(criteria.Term)
                select new SearchResult(sr.SearchResultID, sr.Title, sr.Body, sr.Url, new Dictionary<string, object>(), DateTime.Now)
                ).Cast<ISearchResult>();
        }

        #endregion
    }
}
