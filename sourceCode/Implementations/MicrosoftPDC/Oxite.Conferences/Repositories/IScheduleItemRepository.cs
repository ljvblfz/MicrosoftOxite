//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Repositories
{
    public interface IScheduleItemRepository
    {
        ScheduleItem GetScheduleItem(string eventName, string slug);
        IQueryable<ScheduleItem> GetScheduleItems(EventAddress eventAddress, string type);
        IQueryable<ScheduleItem> GetScheduleItemsByFlag(EventAddress eventAddress, string flagName);
        IQueryable<ScheduleItem> GetScheduleItemsBySpeaker(EventAddress eventAddress, string name);
        IQueryable<ScheduleItem> GetScheduleItems(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IQueryable<ScheduleItem> GetScheduleItems(EventAddress eventAddress, Guid userID, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IQueryable<ScheduleItem> GetScheduleItemsByTag(EventAddress eventAddress, Guid userID, Guid tagID, ScheduleItemFilterCriteria scheduleItemFilterCriteria);
        IQueryable<ScheduleItem> GetScheduleItemsByTimeSlot(EventAddress eventAddress, DateRangeAddress dateRangeAddress);
        IQueryable<ScheduleItem> GetScheduleItemsByTimeSlotAndUser(EventAddress eventAddress, DateRangeAddress dateRangeAddress, Guid userID);
        IQueryable<ScheduleItemTag> GetScheduleItemTags(EventAddress eventAddress);
        IQueryable<ScheduleItem> GetScheduleItemsByUser(EventAddress eventAddress, Guid userID);
        IQueryable<ScheduleItemUser> GetScheduleItemUsers(Guid scheduleItemID, Guid userID);
        ScheduleItemTag GetScheduleItemTag(Guid tagID);
        IEnumerable<ScheduleItemSubscription> GetSubscriptions(string eventName, string scheduleItemSlug);
        void AddSubscription(Guid siteID, ScheduleItemSmall scheduleItem, Guid creatorUserID);
        void AddSubscription(Guid siteID, ScheduleItemComment comment);
        void AddUserRelationship(ScheduleItemSmall scheduleItem, Guid userID);
        void RemoveUserRelationship(ScheduleItemSmall scheduleItem, Guid userID);
    }
}
