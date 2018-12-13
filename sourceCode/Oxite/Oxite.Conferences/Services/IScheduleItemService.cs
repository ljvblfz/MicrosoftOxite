// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Conferences.Services
{
    public interface IScheduleItemService
    {
        IPageOfItems<ScheduleItem> GetScheduleItems(ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItems(Event evnt, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItemsByTag(Event evnt, Tag tag, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItemsByFlag(PagingInfo pagingInfo, Event evnt, string flagName);
        IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlot(Event evnt, DateRangeAddress dateRangeAddress);
        ScheduleItem GetScheduleItem(Event evnt, string scheduleItemSlug);
        IEnumerable<ScheduleItemTag> GetScheduleItemTags(Event evnt);
        ScheduleItemTag GetScheduleItemTag(Tag tag);
    }
}
