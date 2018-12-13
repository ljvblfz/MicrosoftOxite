using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxite.Modules.Search.Models;

namespace Oxite.Modules.Bing.Services
{
    public static class SearchProvider
    {
        public static SearchResults Search(BingSearchCriteria criteria, DateTime? minDate, int pageSize, int pageIndex, string apiKey)
        {
            Communication c = new Communication(apiKey);
            SearchResults sr = c.DoQuery(criteria, pageSize, pageIndex);

            List<ISearchResult> resultsToRemove = new List<ISearchResult>();


            foreach (ISearchResult searchResult in sr)
            {
                if (minDate != null && 
                    searchResult != null &&
                    searchResult.ResultDateTime < minDate.Value )
                {
                    resultsToRemove.Add(searchResult);
                }
            }

            foreach (ISearchResult searchResult in resultsToRemove)
            {
                sr.Results.Remove(searchResult);
            }

            return sr;
        }
    }
}
