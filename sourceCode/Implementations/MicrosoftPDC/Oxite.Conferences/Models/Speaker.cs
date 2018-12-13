//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Modules.Conferences.Models
{
    public class Speaker : EntityBase, INamedEntity
    {
        public Speaker(Guid id, string name, string displayName, string firstName, string lastName, string bio, IEnumerable<ScheduleItem> scheduleItems)
            : base(id)
        {
            Name = name;
            FirstName = firstName;
            LastName = lastName;
            DisplayName = displayName;
            Bio = bio;
            ScheduleItems = scheduleItems;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Bio { get; private set; }
        public IEnumerable<ScheduleItem> ScheduleItems { get; private set; }
    }
}
