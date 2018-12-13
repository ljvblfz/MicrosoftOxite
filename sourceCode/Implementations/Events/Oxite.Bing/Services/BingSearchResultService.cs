//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
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
        public IPageOfItems<ISearchResult> GetSearchResults(int pageIndex, int pageSize, SearchCriteria criteria)
        {
            CriteriaCollection criteriaCollection = new CriteriaCollection();
            criteriaCollection.Items.Add(new SearchTerm() { Term = criteria.Term });
            string apiKey = appSettings.GetString("BingAPIKey");
            string hostsToInclude = appSettings.GetString("BingSearchHosts", "microsoftpdc.com");
            string hostsNotToInclude = appSettings.GetString("BingExcludeSearchHosts", "commnet.microsoftpdc.com,www.microsoftpdc.com,sessions.microsoftpdc.com,microsoftpdc.com/Sessions/Tags/");

            string[] hostsYes = hostsToInclude.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);
            string[] hostsNo = hostsNotToInclude.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            DateTime minDate = DateTime.Parse(appSettings.GetString("BingMinDate"));




            foreach (string s in hostsYes)
            {
                criteriaCollection.Items.Add(new SiteRestriction() {Site = s});                
            }

            foreach (string s in hostsNo)
            {
                criteriaCollection.Items.Add(new SiteRestriction() { Site = s, Not = true});
            }

            criteriaCollection.Items.Add(new SearchTag() { ExactMatch = false, Not = true, TagName = "PageType", Value = "List" });



            return SearchProvider.Search(criteriaCollection, minDate, pageSize, pageIndex, apiKey);
        }

        #endregion
    }
}
