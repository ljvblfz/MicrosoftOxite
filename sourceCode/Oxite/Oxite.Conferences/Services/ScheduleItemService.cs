// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Models.Extensions;
using Oxite.Modules.Comments.Extensions;
using Oxite.Modules.Comments.Services;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;
using Oxite.Modules.Tags.Extensions;
using Oxite.Modules.Tags.Models;
using Oxite.Modules.Tags.Services;
using Oxite.Modules.Conferences.Models.Extensions;

namespace Oxite.Modules.Conferences.Services
{
    public class ScheduleItemService : IScheduleItemService
    {
        private readonly IScheduleItemRepository repository;
        private readonly ITagService tagService;
        private readonly ICommentService commentService;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public ScheduleItemService(IScheduleItemRepository repository, ITagService tagService, ICommentService commentService, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.tagService = tagService;
            this.commentService = commentService;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IScheduleItemService Members

        public IPageOfItems<ScheduleItem> GetScheduleItems(ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return GetScheduleItems(null, scheduleItemFilterCriteria).FillTags(tagService);
        }

        public IPageOfItems<ScheduleItem> GetScheduleItems(Event evnt, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            int pageIndex = 0;
            int pageSize = 50;

            if (context.RequestDataFormat == RequestDataFormat.Web)
            {
                pageIndex = scheduleItemFilterCriteria.PageIndex;
                pageSize = scheduleItemFilterCriteria.PageSize;
            }

            IPageOfItems<ScheduleItem> scheduleItems =
                cache.GetItems<IPageOfItems<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItems-FilterCriteria:{0}", scheduleItemFilterCriteria),
                    new CachePartition(pageIndex, pageSize),
                    () => repository.GetScheduleItems(new PagingInfo(pageIndex, pageSize),  evnt, context.User.IsAuthenticated && context.User.Cast<User>() != null ? context.User.Cast<User>().ID : Guid.Empty, scheduleItemFilterCriteria).FillTags(tagService),
                    si => si.GetDependencies()
                    );

            if (context.RequestDataFormat.IsFeed())
                scheduleItems = scheduleItems.Since(si => si.Modified, context.HttpContext.Request.IfModifiedSince());

            return scheduleItems;
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsByTag(Event evnt, Tag tag, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return
                cache.GetItems<IPageOfItems<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItems-Event:{0},Tag:{1:N},FilterCriteria:{2}", evnt.Name, tag.ID, scheduleItemFilterCriteria),
                    new CachePartition(scheduleItemFilterCriteria.PageIndex, scheduleItemFilterCriteria.PageSize),
                    () => repository.GetScheduleItemsByTag(evnt, context.User.IsAuthenticated && context.User.Cast<User>() != null ? context.User.Cast<User>().ID : Guid.Empty, tag, scheduleItemFilterCriteria).FillTags(tagService),
                    si => si.GetDependencies()
                    );
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsByFlag(PagingInfo pagingInfo, Event evnt, string flagName)
        {
            return
                cache.GetItems<IPageOfItems<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItems-Event:{0},Flag:{1}", evnt.Name, flagName),
                    pagingInfo.ToCachePartition(),
                    () => repository.GetScheduleItemsByFlag(pagingInfo, evnt, flagName).FillTags(tagService),
                    si => si.GetDependencies()
                    );
        }

        public IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlot(Event evnt, DateRangeAddress dateRangeAddress)
        {
            return
                cache.GetItems<IEnumerable<ScheduleItem>, ScheduleItem>(
                    string.Format("GetScheduleItems-Event:{0},Start:{1},End:{2}", evnt.Name, dateRangeAddress.StartDate.ToStringForFeed(), dateRangeAddress.EndDate.ToStringForFeed()),
                    () => repository.GetScheduleItemsByTimeSlot(evnt, dateRangeAddress).FillTags(tagService),
                    si => si.GetDependencies()
                    );
        }

        public ScheduleItem GetScheduleItem(Event evnt, string scheduleItemSlug)
        {
            return cache.GetItem<ScheduleItem>(
                string.Format("GetScheduleItem-Event:{0},ScheduleItem:{1}", evnt.Name, scheduleItemSlug),
                () => repository.GetScheduleItem(evnt, scheduleItemSlug).FillTags(tagService).FillComments(commentService),
                si => si.GetDependencies()
                );
        }

        public IEnumerable<ScheduleItemTag> GetScheduleItemTags(Event evnt)
        {
            return
                cache.GetItems<IEnumerable<ScheduleItemTag>, ScheduleItemTag>(
                    string.Format("GetScheduleItemTags-{0}", evnt.GetCacheItemKey()),
                    () => repository.GetScheduleItemTags(evnt).FillTags(tagService),
                    null
                    );
        }

        public ScheduleItemTag GetScheduleItemTag(Tag tag)
        {
            return
                cache.GetItem(
                    string.Format("GetScheduleItemTag-{0}", tag.GetCacheItemKey()),
                    () => repository.GetScheduleItemTag(tag).FillTag(tagService),
                    null
                    );
        }

        #endregion
    }
}
