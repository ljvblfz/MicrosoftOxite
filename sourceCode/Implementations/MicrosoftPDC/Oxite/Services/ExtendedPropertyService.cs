//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Transactions;
using Oxite.Infrastructure;
using Oxite.Repositories;

namespace Oxite.Services
{
    public class ExtendedPropertyService : IExtendedPropertyService
    {
        private readonly IExtendedPropertyRepository repository;
        private readonly OxiteContext context;

        public ExtendedPropertyService(IExtendedPropertyRepository repository, OxiteContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        #region IExtendedPropertyService Members

        public IEnumerable<ExtendedProperty> GetExtendedProperties(params IExtendedPropertyStore[] scopeItems)
        {
            return repository.GetExtendedProperties(context.Site.ID, scopeItems);
        }

        public void SaveExtendedProperties(IEnumerable<ExtendedProperty> extendedProperties, params IExtendedPropertyStore[] scopeItems)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                repository.Remove(context.Site.ID, scopeItems);

                foreach (ExtendedProperty item in extendedProperties)
                    repository.Save(context.Site.ID, item.Name, item.Type, item.Value, scopeItems);

                transaction.Complete();
            }
        }

        #endregion
    }
}
