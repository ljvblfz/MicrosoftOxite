//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using OxiteSite.App_Code.Modules.OxiteSite.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.Repositories.SqlServer
{
    public class SqlServerPDC09Repository : IRegistrationRepository
    {
        private readonly OxitePDC09DataContext context;

        public SqlServerPDC09Repository(OxitePDC09DataContext context)
        {
            this.context = context;
        }

        #region IPDC09Repository Members

        public UserRegistration GetUserRegistration(Guid userID)
        {
            var query =
                from b in context.pdc09_UserRegistrations
                where b.UserID == userID
                select b;

            return query.Select(ur => new UserRegistration(ur.IsRegistered, ur.LastCheckDate)).FirstOrDefault() ?? new UserRegistration(false, null);
        }

        public void SetUserRegistration(Guid userID, bool isRegistered)
        {
            pdc09_UserRegistration registration = context.pdc09_UserRegistrations.FirstOrDefault(ur => ur.UserID == userID);

            if (registration != null)
            {
                registration.LastCheckDate = DateTime.UtcNow;
                registration.IsRegistered = isRegistered;
            }
            else
            {
                context.pdc09_UserRegistrations.InsertOnSubmit(
                    new pdc09_UserRegistration
                    {
                        UserID = userID,
                        IsRegistered = isRegistered,
                        LastCheckDate = DateTime.UtcNow
                    }
                    );
            }

            context.SubmitChanges();
        }

        public List<Guid> GetUserSessions(Guid userID)
        {
            var query = from b in context.oxite_Conferences_ScheduleItemUserRelationships
                        where b.UserID == userID
                        select b.ScheduleItemID;

            return query.ToList();

        }

        #endregion
    }
}
