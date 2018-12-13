// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Linq;

namespace Oxite.Modules.Conferences.Repositories.SqlServer
{
    public class SqlServerUserScheduleRepository : IUserScheduleRepository
    {
        private readonly OxiteConferencesDataContext context;

        public SqlServerUserScheduleRepository(OxiteConferencesDataContext context)
        {
            this.context = context;
        }

        public bool IsSchedulePublic(Guid userID)
        {
            var userSchedule = context.oxite_Conferences_UserSchedules.SingleOrDefault(us => us.UserID.Equals(userID));
            return userSchedule != null && userSchedule.IsPublic;
        }

        public void MakeSchedulePublic(Guid userID)
        {
            UpdateOrCreateUserSchedule(userID, true);
        }

        public void MakeSchedulePrivate(Guid userID)
        {
            UpdateOrCreateUserSchedule(userID, false);
        }

        private void UpdateOrCreateUserSchedule(Guid userID, bool value)
        {
            var userSchedule = context.oxite_Conferences_UserSchedules.SingleOrDefault(us => us.UserID.Equals(userID));
            if(userSchedule == null)
            {
                userSchedule = new oxite_Conferences_UserSchedule {IsPublic = value, UserID = userID};
                context.oxite_Conferences_UserSchedules.InsertOnSubmit(userSchedule);
            }
            else
            {
                userSchedule.IsPublic = value;
            }
            context.SubmitChanges();
        }
    }
}
