//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Search.Models;
using Oxite.Modules.Search.Repositories;
using Oxite.Plugins.Extensions;

namespace Oxite.Modules.Search.Services
{
    public class SearchResultService : ISearchResultService
    {
        private readonly ISearchResultRepository repository;
        private readonly IPluginEngine pluginEngine;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public SearchResultService(ISearchResultRepository repository, IPluginEngine pluginEngine, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.pluginEngine = pluginEngine;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region ISearchPostService Members

        public IPageOfItems<ISearchResult> GetSearchResults(PagingInfo pagingInfo, SearchCriteria criteria)
        {
            IPageOfItems<ISearchResult> searchResults =
                cache.GetItems<IPageOfItems<ISearchResult>, ISearchResult>(
                    string.Format("GetPosts-SearchTerm:{0}", criteria.Term),
                    pagingInfo.ToCachePartition(),
                    () => repository.GetSearchResults(pagingInfo, criteria),
                    null
                    );

            //if (context.RequestDataFormat.IsFeed())
            //    searchResults = searchResults.Since(context.RequestContext.HttpContext.Request.IfModifiedSince());

            pluginEngine.ExecuteAll("UserSearched", new { context, criteria });

            return searchResults;
        }

        #endregion
    }
}
