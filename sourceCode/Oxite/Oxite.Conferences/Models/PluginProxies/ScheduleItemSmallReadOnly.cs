//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Modules.Conferences.Models;

namespace Oxite.Plugins.Models
{
    public class ScheduleItemSmallReadOnly
    {
        public ScheduleItemSmallReadOnly(ScheduleItemSmall scheduleItemSmall)
        {
            Title = scheduleItemSmall.Title;
            Slug = scheduleItemSmall.Slug;
        }

        public string Title { get; private set; }
        public string Slug { get; private set; }
    }
}
