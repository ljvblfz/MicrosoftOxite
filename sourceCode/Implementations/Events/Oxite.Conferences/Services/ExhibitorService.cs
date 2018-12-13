// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Models.Extensions;
using Oxite.Modules.Conferences.Repositories;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Conferences.Services
{
    public class ExhibitorService : IExhibitorService
    {
        private readonly IExhibitorRepository repository;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public ExhibitorService(IExhibitorRepository repository, IOxiteCacheModule cache, OxiteContext context)
        {
            this.repository = repository;
            this.cache = cache;
            this.context = context;
        }

        public IPageOfItems<Exhibitor> GetExhibitors(EventAddress eventAddress, ExhibitorFilterCriteria exhibitorFilterCriteria)
        {
            var pageIndex = 0;
            var pageSize = 50;

            if (context.RequestDataFormat == RequestDataFormat.Web)
            {
                pageIndex = exhibitorFilterCriteria.PageIndex;
                pageSize = exhibitorFilterCriteria.PageSize;
            }

            var varyBy = string.Join(",", exhibitorFilterCriteria.ParticipantLevels.ToArray()).Trim();

            if(!string.IsNullOrEmpty(exhibitorFilterCriteria.Term))
            {
                varyBy = exhibitorFilterCriteria.Term.Trim();
            }

            return
                cache.GetItems<IPageOfItems<Exhibitor>, Exhibitor>(
                    string.Format("GetExhibitors-Event:{0}-{1}", eventAddress != null
                                                                     ? eventAddress.EventName ?? ""
                                                                     : "", varyBy),
                    new CachePartition(pageIndex, pageSize),
                    () => repository.GetExhibitors(eventAddress, exhibitorFilterCriteria).GetPage(pageIndex, pageSize),
                    e => e.GetDependencies()
                    );
        }

        public IEnumerable<Exhibitor> GetExhibitors(EventAddress eventAddress)
        {
            return
                cache.GetItems<IEnumerable<Exhibitor>, Exhibitor>(
                    string.Format("GetExhibitors-Event:{0}", eventAddress != null
                                                                 ? eventAddress.EventName ?? ""
                                                                 : ""),
                    () => repository.GetExhibitors(eventAddress, null),
                    e => e.GetDependencies()
                    );
        }

        public Exhibitor GetExhibitor(EventAddress eventAddress, string name)
        {
            var exhibitorFilterCriteria = new ExhibitorFilterCriteria
                                              {
                                                  Term = name
                                              };

            var exhibitor = repository.GetExhibitors(eventAddress, exhibitorFilterCriteria)
                .FirstOrDefault();

            return exhibitor;
        }

        public ModelResult<Exhibitor> SaveExhibitor(EventAddress eventAddress, Exhibitor exhibitor)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();
            
            // todo (dcrenna) validation
            if (!validationState.IsValid) return new ModelResult<Exhibitor>(validationState);
            
            using (var transaction = new TransactionScope())
            {
                exhibitor = repository.SaveExhibitor(eventAddress, exhibitor);

                transaction.Complete();
            }

            cache.InvalidateItem(exhibitor);

            return new ModelResult<Exhibitor>(exhibitor, validationState);
        }

        public void RemoveExhibitor(EventAddress eventAddress, Exhibitor exhibitor)
        {
            using (var transaction = new TransactionScope())
            {
                repository.RemoveExhibitor(eventAddress, exhibitor);

                transaction.Complete();
            }

            cache.InvalidateItem(exhibitor);
        }
    }
}
