//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Search.Models;
using Oxite.Modules.Search.Services;
using Oxite.ViewModels;

namespace Oxite.Modules.Search.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchResultService searchPostService;

        public SearchController(ISearchResultService searchPostService)
        {
            this.searchPostService = searchPostService;
        }

        public OxiteViewModelItems<ISearchResult> List(PagingInfo pagingInfo, SearchCriteria criteria)
        {
            IPageOfItems<ISearchResult> searchResults = criteria.HasCriteria() ? searchPostService.GetSearchResults(pagingInfo, criteria) : null;

            return new OxiteViewModelItems<ISearchResult>(searchResults) { Container = new SearchPageContainer() };
        }
    }
}
