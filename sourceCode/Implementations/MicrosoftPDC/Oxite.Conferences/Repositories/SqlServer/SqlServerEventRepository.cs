// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Linq;
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

        #region IEventRepository Members

        public Event GetEvent(string eventName)
        {
            if (eventName == null)
                return null;

            return (
                from e in context.oxite_Conferences_Events
                where string.Compare(e.EventName, eventName, true) == 0
                select projectEvent(e)
                ).FirstOrDefault();
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
