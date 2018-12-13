//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Repositories;
using Oxite.Validation;

namespace Oxite.Services
{
    public class SiteService : ISiteService
    {
        private readonly ISiteRepository repository;
        private readonly IValidationService validator;
        private readonly IOxiteCacheModule cache;
        private readonly AppSettingsHelper appSettings;

        public SiteService(ISiteRepository repository, IValidationService validator, IModulesLoaded modulesLoaded, AppSettingsHelper appSettings)
        {
            IEnumerable<IOxiteCacheModule> modules = modulesLoaded.GetModules<IOxiteCacheModule>();

            this.repository = repository;
            this.validator = validator;
            this.cache = modules != null && modules.Count() > 0 ? modules.Reverse().First() : null;
            this.appSettings = appSettings;
        }

        #region ISiteService Members

        public Site GetSite()
        {
            string instanceName = appSettings.GetInstanceName();

            return
                cache != null
                ? cache.GetItem<Site>("GetSite", () => repository.GetSite(instanceName), null)
                : repository.GetSite(instanceName);
        }

        public void AddSite(Site site, out ValidationStateDictionary validationState, out Site newSite)
        {
            validationState = new ValidationStateDictionary();

            validationState.Add(typeof(Site), validator.Validate(site));

            if (!validationState.IsValid)
            {
                newSite = null;

                return;
            }

            repository.Save(site);

            newSite = repository.GetSite(site.Name);
        }

        public void EditSite(Site site, out ValidationStateDictionary validationState)
        {
            validationState = new ValidationStateDictionary();

            validationState.Add(typeof(Site), validator.Validate(site));

            if (!validationState.IsValid) return;

            repository.Save(site);
        }

        #endregion
    }
}
