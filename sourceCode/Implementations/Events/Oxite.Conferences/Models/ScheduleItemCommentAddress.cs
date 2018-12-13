//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemCommentAddress : ScheduleItemAddress
    {
        public ScheduleItemCommentAddress(string eventName, string scheduleItemSlug, string commentSlug)
            : base(eventName, scheduleItemSlug)
        {
            CommentSlug = commentSlug;
        }

        public ScheduleItemCommentAddress(ScheduleItemAddress scheduleItemAddress, string commentSlug)
            : this(scheduleItemAddress.EventName, scheduleItemAddress.ScheduleItemSlug, commentSlug)
        {
        }

        public string CommentSlug { get; private set; }

        public ScheduleItemAddress ToScheduleItemAddress()
        {
            return new ScheduleItemAddress(EventName, ScheduleItemSlug);
        }
    }
}
