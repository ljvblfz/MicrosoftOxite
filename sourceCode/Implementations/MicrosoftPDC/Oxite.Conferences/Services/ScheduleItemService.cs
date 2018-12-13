// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Models.Extensions;
using Oxite.Modules.Comments.Extensions;
using Oxite.Modules.Comments.Services;
using Oxite.Modules.Conferences.Extensions;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;
using Oxite.Modules.Membership.Services;
using Oxite.Modules.Tags.Extensions;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Services;
using Oxite.Services;
using Oxite.Modules.Conferences.Models.Extensions;

namespace Oxite.Modules.Conferences.Services
{
    public class ScheduleItemService : IScheduleItemService
    {
        private readonly IScheduleItemRepository repository;
        private readonly ITagService tagService;
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public ScheduleItemService(IScheduleItemRepository repository, ITagService tagService, ICommentService commentService, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.tagService = tagService;
            this.commentService = commentService;
            cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IScheduleItemService Members

        public IPageOfItems<ScheduleItem> GetScheduleItems(ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return GetScheduleItems(null, scheduleItemFilterCriteria).FillTags(tagService);
        }

        public IPageOfItems<ScheduleItem> GetScheduleItems(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            string dataFormat = context.RequestContext.RouteData.Values["dataFormat"] as String;
            int pageIndex = scheduleItemFilterCriteria.PageIndex;
            int pageSize = scheduleItemFilterCriteria.PageSize;

            if (dataFormat != null)
            {
                dataFormat = dataFormat.ToLower();

                if (dataFormat == "rss" || dataFormat=="atom" || dataFormat=="sign" || dataFormat=="ics")
                {
                    pageIndex = 0;
                    pageSize = 1000;
                }
            }


            var cacheKey = string.Format("GetScheduleItems-FilterCriteria:{0}", scheduleItemFilterCriteria);
            var authenticatedUser = context.User.IsAuthenticated && context.User.Cast<UserAuthenticated>() != null;
            var userId = Guid.Empty;
            if(authenticatedUser)
            {
                userId = context.User.Cast<UserAuthenticated>().ID;
                cacheKey += String.Concat("-UserID:", userId);
            }

            IPageOfItems<ScheduleItem> scheduleItems =
                cache.GetItems<IPageOfItems<ScheduleItem>, ScheduleItem>(
                    cacheKey,
                    new CachePartition(pageIndex, pageSize),
                    () => repository.GetScheduleItems(eventAddress, userId, scheduleItemFilterCriteria).GetPage(pageIndex, pageSize).FillTags(tagService),
                    si => si.GetDependencies()
                    );

            SetScheduleItemUsers(scheduleItems);

            return scheduleItems;
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsByTag(EventAddress eventAddress, Guid tagID, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            var result =
                cache.GetItems<IPageOfItems<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItems-Event:{0},Tag:{1:N},FilterCriteria:{2}", eventAddress.EventName, tagID, scheduleItemFilterCriteria),
                    new CachePartition(scheduleItemFilterCriteria.PageIndex, scheduleItemFilterCriteria.PageSize),
                    () => repository.GetScheduleItemsByTag(eventAddress, context.User.IsAuthenticated && context.User.Cast<UserAuthenticated>() != null ? context.User.Cast<UserAuthenticated>().ID : Guid.Empty, tagID, scheduleItemFilterCriteria).GetPage(scheduleItemFilterCriteria.PageIndex, scheduleItemFilterCriteria.PageSize).FillTags(tagService),
                    si => si.GetDependencies()
                    );

            SetScheduleItemUsers(result);

            return result;
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsByFlag(int pageIndex, int pageSize, EventAddress eventAddress, string flagName)
        {
            var result =
                cache.GetItems<IPageOfItems<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItems-Event:{0},Flag:{1}", eventAddress.EventName, flagName),
                    new CachePartition(pageIndex, pageSize),
                    () => repository.GetScheduleItemsByFlag(eventAddress, flagName).GetPage(pageIndex, pageSize).FillTags(tagService),
                    si => si.GetDependencies()
                    );

            SetScheduleItemUsers(result);

            return result;
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsByUser(int pageIndex, int pageSize, EventAddress eventAddress, Guid userID)
        {
            // if this is cached, you'll see schedule items on your list that used to be on the list but aren't anymore
            var result =
                repository.GetScheduleItemsByUser(eventAddress, userID).GetPage(pageIndex, pageSize).FillTags(tagService);

            /*
            IPageOfItems<ScheduleItem> result =
                cache.GetItems<IPageOfItems<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItemsByUser-UserID:{0}", userID),
                    new CachePartition(pageIndex, pageSize),
                    () => repository.GetScheduleItemsByUser(eventAddress, userID).GetPage(pageIndex, pageSize).FillTags(tagService), 
                    si => si.GetDependencies()
                    );
            */

            SetScheduleItemUsers(result);

            return result;
        }

        public IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlot(EventAddress eventAddress, DateRangeAddress dateRangeAddress)
        {
            var result =
                cache.GetItems<IEnumerable<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItems-Event:{0},Start:{1},End:{2}", eventAddress.EventName, dateRangeAddress.StartDate.ToStringForFeed(), dateRangeAddress.EndDate.ToStringForFeed()),
                    () => repository.GetScheduleItemsByTimeSlot(eventAddress, dateRangeAddress).FillTags(tagService).ToList(),
                    si => si.GetDependencies()
                    );

            SetScheduleItemUsers(result);

            return result;
        }

        public IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlotAndUser(EventAddress address, DateRangeAddress rangeAddress, Guid userID)
        {


            // if this is cached, you'll see schedule items on your list that used to be on the list but aren't anymore
            var result = repository.GetScheduleItemsByTimeSlotAndUser(address, rangeAddress, userID).FillTags(tagService);

            SetScheduleItemUsers(result);

            return result;
        }

        public ScheduleItem GetScheduleItem(ScheduleItemAddress scheduleItemAddress)
        {
            var result = 
                cache.GetItem<ScheduleItem>(
                string.Format("GetScheduleItem-Event:{0},ScheduleItem:{1}", scheduleItemAddress.EventName, scheduleItemAddress.ScheduleItemSlug),
                () => repository.GetScheduleItem(scheduleItemAddress.EventName, scheduleItemAddress.ScheduleItemSlug).FillTags(tagService).FillComments(commentService),
                si => si.GetDependencies()
                );

            SetScheduleItemUsers(result);

            return result;
        }

        private void SetScheduleItemUsers(IEnumerable<ScheduleItem> scheduleItems)
        {
            foreach(var scheduleItem in scheduleItems)
            {
                SetScheduleItemUsers(scheduleItem);
            }
        }

        private void SetScheduleItemUsers(ScheduleItem scheduleItem)
        {
            var user = context.User.Cast<UserAuthenticated>();
            if (user == null) return;
            var users = repository.GetScheduleItemUsers(scheduleItem.ID, user.ID);
            scheduleItem.Users = users.ToList();
        }

        public IEnumerable<ScheduleItemTag> GetScheduleItemTags(EventAddress eventAddress)
        {
            var result =
                cache.GetItems<IEnumerable<ScheduleItemTag>, ScheduleItemTag>(
                    string.Format("GetScheduleItemTags-Event:{0}", eventAddress.EventName),
                    () => repository.GetScheduleItemTags(eventAddress).ToList().AsEnumerable().FillTags(tagService),
                    null
                    );

            return result;
        }

        public ScheduleItemTag GetScheduleItemTag(Tag tag)
        {
            var result =
                cache.GetItem(
                    string.Format("GetScheduleItemTag-TagID:{0}", tag.ID),
                    () => repository.GetScheduleItemTag(tag.ID).FillTag(tagService),
                    null
                    );
            
            return result;
        }

        public void AddUserToScheduleItem(ScheduleItemAddress scheduleItemAddress, Guid userID)
        {
            var scheduleItem = GetScheduleItem(scheduleItemAddress);

            repository.AddUserRelationship(scheduleItem.ToScheduleItemSmall(), userID);

            //cache.InvalidateItem(scheduleItem);
            //var key = string.Format("GetScheduleItemsByUser-UserID:{0}", userID);
            //cache.InvalidateContains(key, StringComparer.InvariantCultureIgnoreCase);
        }

        public void RemoveUserFromScheduleItem(ScheduleItemAddress scheduleItemAddress, Guid userID)
        {
            var scheduleItem = GetScheduleItem(scheduleItemAddress);

            repository.RemoveUserRelationship(scheduleItem.ToScheduleItemSmall(), userID);

            //cache.InvalidateItem(scheduleItem);
            //cache.Invalidate(string.Format("GetScheduleItemsByUser-UserID:{0}", userID));
        }

        // todo (dcrenna): remove this as soon as distributed caching is available
        public IPageOfItems<ScheduleItem> GetScheduleItemsUncachedByUser(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            int pageIndex = 0;
            int pageSize = 50;

            if (context.RequestDataFormat == RequestDataFormat.Web)
            {
                pageIndex = scheduleItemFilterCriteria.PageIndex;
                pageSize = scheduleItemFilterCriteria.PageSize;
            }

            var cacheKey = string.Format("GetScheduleItems-FilterCriteria:{0}", scheduleItemFilterCriteria);
            var authenticatedUser = context.User.IsAuthenticated && context.User.Cast<UserAuthenticated>() != null;
            var userId = Guid.Empty;
            if (authenticatedUser)
            {
                userId = context.User.Cast<UserAuthenticated>().ID;
                cacheKey += String.Concat("-UserID:", userId);
            }

            var items = repository.GetScheduleItems(eventAddress, userId, scheduleItemFilterCriteria);//.OrderBy(si => si.Start);
            var scheduleItems = items.GetPage(pageIndex,pageSize).FillTags(tagService);

            if (context.RequestDataFormat.IsFeed())
                scheduleItems = scheduleItems.Since(si => si.Modified, context.HttpContext.Request.IfModifiedSince());

            SetScheduleItemUsers(scheduleItems);
            
            return scheduleItems;
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsUncached(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            int pageIndex = 0;
            int pageSize = 50;

            if (context.RequestDataFormat == RequestDataFormat.Web)
            {
                pageIndex = scheduleItemFilterCriteria.PageIndex;
                pageSize = scheduleItemFilterCriteria.PageSize;
            }
            
            var items = repository.GetScheduleItems(eventAddress, scheduleItemFilterCriteria);//.OrderBy(si => si.Start);
            var scheduleItems = items.GetPage(pageIndex, pageSize).FillTags(tagService);

            if (context.RequestDataFormat.IsFeed())
                scheduleItems = scheduleItems.Since(si => si.Modified, context.HttpContext.Request.IfModifiedSince());

            return scheduleItems;
        }

        #endregion
    }
}
