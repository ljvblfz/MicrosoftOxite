//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemUser : EntityBase
    {
        public Guid UserID { get; private set; }
        public string Username { get; private set; }

        public ScheduleItemUser(Guid userID, string userName)
        {
            UserID = userID;
            Username = userName;
        }
    }
}
