//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Oxite.BackgroundServices;
using Oxite.Repositories;

namespace Oxite.Services
{
    public class BackgroundServiceService : IBackgroundServiceService
    {
        private readonly IBackgroundServiceRepository repository;

        public BackgroundServiceService(IBackgroundServiceRepository repository)
        {
            this.repository = repository;
        }

        #region IBackgroundServiceService Members

        public IList<IBackgroundService> GetBackgroundServices()
        {
            return repository.GetBackgroundServices();
        }

        public IBackgroundService GetBackgroundService(Guid backgroundServiceID)
        {
            return repository.GetBackgroundService(backgroundServiceID);
        }

        public NameValueCollection LoadSettings(IBackgroundService backgroundService)
        {
            if (!repository.GetBackgroundServiceExists(backgroundService.ID))
            {
                repository.Save(backgroundService);
            }

            return repository.GetBackgroundServiceSettings(backgroundService.ID);
        }

        public void Save(IBackgroundService backgroundService)
        {
            repository.Save(backgroundService, backgroundService.Settings);
        }

        public void SaveSetting(IBackgroundService backgroundService, string name, string value)
        {
            if (!repository.GetBackgroundServiceExists(backgroundService.ID))
            {
                repository.Save(backgroundService);
            }

            repository.SaveSetting(backgroundService.ID, name, value);
        }

        #endregion
    }
}
