//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemAddress : EventAddress
    {
        public ScheduleItemAddress(string eventName, string scheduleItemSlug)
            : base(eventName)
        {
            ScheduleItemSlug = scheduleItemSlug;
        }

        public ScheduleItemAddress(EventAddress eventAddress, string scheduleItemSlug)
            : this(eventAddress.EventName, scheduleItemSlug)
        {
        }

        public string ScheduleItemSlug { get; private set; }

        public EventAddress ToEventAddress()
        {
            return new EventAddress(EventName);
        }
    }
}