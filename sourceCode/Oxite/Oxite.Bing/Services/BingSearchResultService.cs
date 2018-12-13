//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Search.Models;
using Oxite.Modules.Search.Services;
using Oxite.Services;

namespace Oxite.Modules.Bing.Services
{
    public class BingSearchResultService : ISearchResultService
    {
        private readonly OxiteContext context;
        private readonly AppSettingsHelper appSettings;

        public BingSearchResultService(OxiteContext context, AppSettingsHelper appSettings)
        {
            this.context = context;
            this.appSettings = appSettings;
        }

        #region ISearchResultService Members

        public IPageOfItems<ISearchResult> GetSearchResults(PagingInfo pagingInfo, SearchCriteria criteria)
        {
            CriteriaCollection criteriaCollection = new CriteriaCollection();

            criteriaCollection.Items.Add(new SearchTerm() { Term = criteria.Term });
            //TODO: (erikpo) The following hardcoded values need to be moved to somewhere more generic like module settings
            criteriaCollection.Items.Add(new SiteRestriction() { Site = "microsoftpdc.com" });
            criteriaCollection.Items.Add(new SiteRestriction() { Site = "commnet.microsoftpdc.com", Not = true });
            criteriaCollection.Items.Add(new SiteRestriction() { Site = "sessions.microsoftpdc.com", Not = true });

            return SearchProvider.Search(criteriaCollection, pagingInfo, appSettings.GetString("BingAPIKey"));
        }

        #endregion
    }
}
