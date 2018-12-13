//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Conferences.Repositories
{
    public interface IScheduleItemRepository
    {
        ScheduleItem GetScheduleItem(Event evnt, string slug);
        IPageOfItems<ScheduleItem> GetScheduleItems(PagingInfo pagingInfo, Event evnt, string type);
        IPageOfItems<ScheduleItem> GetScheduleItemsByFlag(PagingInfo pagingInfo, Event evnt, string flagName);
        IPageOfItems<ScheduleItem> GetScheduleItemsBySpeaker(PagingInfo pagingInfo, Event evnt, string name);
        IPageOfItems<ScheduleItem> GetScheduleItems(PagingInfo pagingInfo, Event evnt, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItems(PagingInfo pagingInfo, Event evnt, Guid userID, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItemsByTag(Event evnt, Guid userID, Tag tag, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlot(Event evnt, DateRangeAddress dateRangeAddress);
        IEnumerable<ScheduleItemTag> GetScheduleItemTags(Event evnt);
        ScheduleItemTag GetScheduleItemTag(Tag tag);
        IEnumerable<ScheduleItemSubscription> GetSubscriptions(string eventName, string scheduleItemSlug);
        void AddSubscription(ScheduleItemSmall scheduleItem, Guid creatorUserID);
        void AddSubscription(ScheduleItemComment comment);
    }
}
