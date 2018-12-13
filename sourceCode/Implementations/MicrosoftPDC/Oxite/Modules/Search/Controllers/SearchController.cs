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
using Oxite.Services;
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

        public OxiteViewModelItems<ISearchResult> List(int? pageNumber, int pageSize, SearchCriteria criteria)
        {
            int pageIndex = pageNumber.HasValue ? pageNumber.Value - 1 : 0;
            IPageOfItems<ISearchResult> searchResults = criteria.HasCriteria() ? searchPostService.GetSearchResults(pageIndex, pageSize, criteria) : null;

            return new OxiteViewModelItems<ISearchResult>(searchResults) { Container = new SearchPageContainer() };
        }
    }
}
