// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Conferences.Services
{
    public interface IScheduleItemService
    {
        IPageOfItems<ScheduleItem> GetScheduleItems(ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItems(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItemsUncachedByUser(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItemsUncached(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItemsByTag(EventAddress eventAddress, Guid tagID, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IPageOfItems<ScheduleItem> GetScheduleItemsByFlag(int pageIndex, int pageSize, EventAddress eventAddress, string flagName);
        IPageOfItems<ScheduleItem> GetScheduleItemsByUser(int pageIndex, int pageSize, EventAddress eventAddress, Guid userID);
        IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlot(EventAddress eventAddress, DateRangeAddress dateRangeAddress);
        IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlotAndUser(EventAddress address, DateRangeAddress rangeAddress, Guid userID);
        ScheduleItem GetScheduleItem(ScheduleItemAddress scheduleItemAddress);
        IEnumerable<ScheduleItemTag> GetScheduleItemTags(EventAddress eventAddress);
        ScheduleItemTag GetScheduleItemTag(Tag tag);
        void AddUserToScheduleItem(ScheduleItemAddress scheduleItemAddress, Guid userID);
        void RemoveUserFromScheduleItem(ScheduleItemAddress scheduleItemAddress, Guid userID);
    }
}
