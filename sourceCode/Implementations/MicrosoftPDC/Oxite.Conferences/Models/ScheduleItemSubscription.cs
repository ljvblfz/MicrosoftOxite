//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemSubscription : EntityBase
    {
        public ScheduleItemSubscription(Guid id, ScheduleItemSmall scheduleItem, string userName, string userEmail)
            : base(id)
        {
            ScheduleItem = scheduleItem;
            UserName = userName;
            UserEmail = userEmail;
        }

        public ScheduleItemSmall ScheduleItem { get; private set; }
        public string UserName { get; private set; }
        public string UserEmail { get; private set; }
    }
}
