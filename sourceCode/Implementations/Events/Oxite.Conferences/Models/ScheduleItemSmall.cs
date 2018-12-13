//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemSmall
    {
        public ScheduleItemSmall(Guid id, string eventName, string slug, string title)
        {
            ID = id;
            EventName = eventName;
            Slug = slug;
            Title = title;
        }

        public Guid ID { get; private set; }
        public string EventName { get; set; }
        public string Slug { get; private set; }
        public string Title { get; private set; }
    }
}
