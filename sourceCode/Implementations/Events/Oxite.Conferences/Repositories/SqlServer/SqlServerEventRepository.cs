// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Linq;
using System.Web.Caching;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Repositories.SqlServer
{
    public class SqlServerEventRepository : IEventRepository
    {
        private readonly OxiteConferencesDataContext context;

        public SqlServerEventRepository(OxiteConferencesDataContext context)
        {
            this.context = context;
        }

        static SqlServerEventRepository()
        {
            _cache = System.Web.HttpContext.Current.Cache;   
        }

        private static Cache _cache;


        #region IEventRepository Members

        public Event GetEvent(string eventName)
        {

            if (eventName == null)
                return null;

            Event eventObject = null;
            string cacheKey = "event:" + eventName;

            if (_cache != null)
            {
                eventObject = _cache[cacheKey] as Event;
            }

            if (eventObject != null)
                return eventObject;

            System.Diagnostics.Debug.WriteLine("GetEvent: " + eventName);

            eventObject = (
                from e in context.oxite_Conferences_Events
                where string.Compare(e.EventName, eventName, true) == 0
                select projectEvent(e)
                ).FirstOrDefault();

            if (_cache != null)
            {
                _cache.Add(cacheKey, eventObject, null, DateTime.Now.AddHours(1),
                           Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            }

            return eventObject;
            
        }

        #endregion

        #region Private Methods

        private static Event projectEvent(oxite_Conferences_Event e)
        {
            return new Event(e.EventID, e.EventName, e.EventDisplayName, e.Year);
        }

        #endregion
    }
}
