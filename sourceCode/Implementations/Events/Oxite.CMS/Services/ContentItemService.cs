//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Caching;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.CMS.Models;
using Oxite.Modules.CMS.Repositories;

namespace Oxite.Modules.CMS.Services
{
    public class ContentItemService : IContentItemService
    {
        private readonly IContentItemRepository repository;
        private readonly OxiteContext context;

        public ContentItemService(IContentItemRepository repository, OxiteContext context)
        {
            this.repository = repository;
            this.context = context;
        }
        static ContentItemService()
        {
            
            _cache = System.Web.HttpContext.Current.Cache;   
        }


        private static Cache _cache;

        #region IContentItemService Members

        public IEnumerable<ContentItem> GetContentItems()
        {

            IEnumerable<ContentItem> contentItems = null;
            string cacheKey = "SiteContentItems:" + context.Site.ID;

            if (_cache != null)
            {
                contentItems = _cache[cacheKey] as IEnumerable<ContentItem>;
            }

            if (contentItems != null)
                return contentItems;

            System.Diagnostics.Debug.WriteLine("GetSiteContentItems: " + context.Site.ID);

            contentItems = repository.GetContentItems(context.Site.ID).ToList();

            if (_cache != null)
            {
                _cache.Add(cacheKey, contentItems, null, DateTime.Now.AddHours(1),
                           Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return contentItems;

        }

        public void EditContentItems(IEnumerable<ContentItemInput> contentItems)
        {
            //TODO: (erikpo) Validate input

            using (TransactionScope transaction = new TransactionScope())
            {
                //TODO: (erikpo) Follow other editing patterns in Oxite and add plugin and module events

                foreach (ContentItemInput input in contentItems)
                    repository.Save(context.Site.ID, input.ToContentItem(context.User.Cast<UserAuthenticated>(), context.Site.ID));

                transaction.Complete();
            }
        }

        #endregion
    }
}
