// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;

namespace Oxite.Modules.Conferences.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository repository;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public EventService(IEventRepository repository, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IEventService Members

        public bool GetEventExists(string eventName)
        {
            return cache.GetItem<bool?>(
                string.Format("GetEventExists-Name:{0}", eventName),
                () => repository.GetEvent(eventName) != null,
                null
                ).GetValueOrDefault(false);
        }

        public Event GetEvent(string eventName)
        {
            return repository.GetEvent(eventName);
        }

        #endregion
    }
}