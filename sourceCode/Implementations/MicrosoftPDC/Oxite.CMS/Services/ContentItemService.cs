//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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

        #region IContentItemService Members

        public IEnumerable<ContentItem> GetContentItems()
        {
            return repository.GetContentItems(context.Site.ID).ToList();
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
