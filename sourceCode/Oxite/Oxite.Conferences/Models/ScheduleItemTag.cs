//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemTag : Tag
    {
        public ScheduleItemTag(Guid id, string displayName)
            : base(id)
        {
            DisplayName = displayName;
        }

        public ScheduleItemTag(string name, string displayName)
            : base(name)
        {
            DisplayName = displayName;
        }

        public ScheduleItemTag(Guid id, string name, string displayName, DateTime created)
            : base(id, name, created)
        {
            DisplayName = displayName;
        }
    }
}
