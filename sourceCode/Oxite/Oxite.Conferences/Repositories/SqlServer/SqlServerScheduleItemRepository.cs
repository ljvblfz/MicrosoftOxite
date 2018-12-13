//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Conferences.Repositories.SqlServer
{
    public class SqlServerScheduleItemRepository : IScheduleItemRepository
    {
        private readonly OxiteConferencesDataContext context;

        public SqlServerScheduleItemRepository(OxiteConferencesDataContext context)
        {
            this.context = context;
        }

        #region IScheduleItemRepository Members

        public ScheduleItem GetScheduleItem(/*Guid siteID, */Event evnt, string slug)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where /*e.SiteID == siteID && */e.EventID == evnt.ID && string.Compare(si.Slug, slug, true) == 0
                select si;

            return projectScheduleItems(query).FirstOrDefault();
        }

        public IPageOfItems<ScheduleItem> GetScheduleItems(PagingInfo pagingInfo, Event evnt, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return GetScheduleItems(pagingInfo, evnt, Guid.Empty, scheduleItemFilterCriteria);
        }

        public IPageOfItems<ScheduleItem> GetScheduleItems(PagingInfo pagingInfo, Event evnt, Guid userID, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return projectScheduleItems(getScheduleItems(evnt, userID, scheduleItemFilterCriteria)).GetPage(pagingInfo);
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsByTag(Event evnt, Guid userID, Tag tag, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            var query =
                getScheduleItems(evnt, userID, scheduleItemFilterCriteria)
                .Where(si => si.oxite_Conferences_ScheduleItemTagRelationships.Any(sitr => sitr.TagID == tag.ID));

            return projectScheduleItems(query).GetPage(new PagingInfo(scheduleItemFilterCriteria.PageIndex, scheduleItemFilterCriteria.PageSize));
        }

        public IPageOfItems<ScheduleItem> GetScheduleItems(PagingInfo pagingInfo, Event evnt, string type)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where e.EventID == evnt.ID && string.Compare(si.Type, type, true) == 0
                select si;

            return projectScheduleItems(query).GetPage(pagingInfo);
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsBySpeaker(PagingInfo pagingInfo, Event evnt, string name)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join sis in context.oxite_Conferences_ScheduleItemSpeakerRelationships on si.ScheduleItemID equals sis.ScheduleItemID
                join s in context.oxite_Conferences_Speakers on sis.SpeakerID equals s.SpeakerID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where e.EventID ==  evnt.ID && string.Compare(s.SpeakerName, name, true) == 0
                select si;

            return projectScheduleItems(query).GetPage(pagingInfo);
        }

        public IEnumerable<ScheduleItem> GetScheduleItemsByTimeSlot(Event evnt, DateRangeAddress dateRangeAddress)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where e.EventID == evnt.ID && si.StartTime >= dateRangeAddress.StartDate && si.EndTime <= dateRangeAddress.EndDate
                select si;

            return projectScheduleItems(query).ToArray();
        }

        public IPageOfItems<ScheduleItem> GetScheduleItemsByFlag(PagingInfo pagingInfo, Event evnt, string flagName)
        {
            var query =
                from sif in context.oxite_Conferences_ScheduleItemFlags
                join si in context.oxite_Conferences_ScheduleItems on sif.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where e.EventID == evnt.ID && string.Compare(sif.FlagType, flagName, true) == 0
                select si;

            return projectScheduleItems(query).GetPage(pagingInfo);
        }

        public IEnumerable<ScheduleItemTag> GetScheduleItemTags(Event evnt)
        {
            return (
                from sitr in context.oxite_Conferences_ScheduleItemTagRelationships
                join si in context.oxite_Conferences_ScheduleItems on sitr.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                group si by new { TagID = sitr.TagID, TagDisplayName = sitr.TagDisplayName, Evnt = e } into results
                where results.Key.Evnt.EventID == evnt.ID
                select new ScheduleItemTag(results.Key.TagID, results.Key.TagDisplayName)
                ).ToArray();
        }

        public ScheduleItemTag GetScheduleItemTag(Tag tag)
        {
            return
                (from sitr in context.oxite_Conferences_ScheduleItemTagRelationships
                where sitr.TagID == tag.ID
                select new ScheduleItemTag(sitr.TagID, sitr.TagDisplayName)).FirstOrDefault();
        }

        public IEnumerable<ScheduleItemSubscription> GetSubscriptions(string eventName, string scheduleItemSlug)
        {
            var query =
                from s in context.oxite_Subscriptions
                join sisr in context.oxite_Conferences_ScheduleItemSubscriptionRelationships on s.SubscriptionID equals sisr.SubscriptionID
                join si in context.oxite_Conferences_ScheduleItems on sisr.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                join u in context.oxite_Users on s.UserID equals u.UserID
                where /*e.SiteID == siteID && */string.Compare(e.EventName, eventName, true) == 0 && si.Slug == scheduleItemSlug
                select new { Event = e, ScheduleItem = si, User = u, Subscription = s };

            //TOOD: (erikpo) Need to figure out why this query fails if not .ToArray()'d before selecting.  LINQ to SQL isn't liking something in the select, but not sure what.
            var temp = query.ToArray();

            return temp.Select(t => new ScheduleItemSubscription(
                    t.Subscription.SubscriptionID,
                    new ScheduleItemSmall(t.ScheduleItem.ScheduleItemID, t.Event.EventName, t.ScheduleItem.Slug, t.ScheduleItem.Title),
                    t.User.Username != "Anonymous" ? !string.IsNullOrEmpty(t.User.DisplayName) ? t.User.DisplayName : t.User.Username : t.Subscription.UserName,
                    t.User.Username != "Anonymous" ? t.User.Email : t.Subscription.UserEmail
                    )
                );
        }

        public void AddSubscription(ScheduleItemSmall scheduleItem, Guid creatorUserID)
        {
            if (getSubscriptionExists(scheduleItem, creatorUserID)) return;

            oxite_Subscription subscription = new oxite_Subscription { SubscriptionID = Guid.NewGuid(), UserID = creatorUserID };

            context.oxite_Subscriptions.InsertOnSubmit(subscription);
            context.oxite_Conferences_ScheduleItemSubscriptionRelationships.InsertOnSubmit(new oxite_Conferences_ScheduleItemSubscriptionRelationship { SubscriptionID = subscription.SubscriptionID, ScheduleItemID = scheduleItem.ID });

            context.SubmitChanges();
        }

        public void AddSubscription(ScheduleItemComment comment)
        {
            if (getSubscriptionExists(comment.ScheduleItem, comment.CreatorEmail)) return;

            oxite_Subscription subscription = new oxite_Subscription { SubscriptionID = Guid.NewGuid(), UserID = context.oxite_Users.Single(u => u.Username == "Anonymous").UserID, UserName = comment.CreatorName, UserEmail = comment.CreatorEmail };

            context.oxite_Subscriptions.InsertOnSubmit(subscription);
            context.oxite_Conferences_ScheduleItemSubscriptionRelationships.InsertOnSubmit(new oxite_Conferences_ScheduleItemSubscriptionRelationship { SubscriptionID = subscription.SubscriptionID, ScheduleItemID = comment.ScheduleItem.ID });

            context.SubmitChanges();
        }

        #endregion

        #region Private Methods

        private IQueryable<oxite_Conferences_ScheduleItem> getScheduleItems(Event evnt, Guid userID, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            IQueryable<oxite_Conferences_ScheduleItem> query = from si in context.oxite_Conferences_ScheduleItems select si;

            //todo: (nheskew) probably should only look through speaker names if there isn't speaker name criteria
            if (!string.IsNullOrEmpty(scheduleItemFilterCriteria.Term))
                query = query
                    .Where(
                    si =>
                    si.Title.Contains(scheduleItemFilterCriteria.Term) ||
                    si.Body.Contains(scheduleItemFilterCriteria.Term) ||
                    si.Slug.Contains(scheduleItemFilterCriteria.Term) ||
                    si.Location.Contains(scheduleItemFilterCriteria.Term) ||
                    si.oxite_Conferences_ScheduleItemSpeakerRelationships
                        .Any(
                        sis =>
                        sis.oxite_Conferences_Speaker.SpeakerDisplayName.Contains(scheduleItemFilterCriteria.Term))
                    );

            if (scheduleItemFilterCriteria.ForUser && userID != Guid.Empty)
                query = query
                    .Where(
                    si => si.oxite_Conferences_ScheduleItemUserRelationships
                              .Any(siu => siu.UserID == userID)
                    );

            if (evnt != null)
                query = query
                    .Where(si => si.oxite_Conferences_Event.EventID == evnt.ID);

            //if (!string.IsNullOrEmpty(scheduleItemFilterCriteria.ScheduleItemType))
            //    query = query
            //        .Where(si => string.Compare(si.Type, scheduleItemFilterCriteria.ScheduleItemType, true) == 0);

            if (!string.IsNullOrEmpty(scheduleItemFilterCriteria.SpeakerName))
                query = query
                    .Where(
                    si => si.oxite_Conferences_ScheduleItemSpeakerRelationships
                              .Any(
                              sis =>
                              string.Compare(sis.oxite_Conferences_Speaker.SpeakerName,
                                             scheduleItemFilterCriteria.SpeakerName, true) == 0)
                    );

            query = scheduleItemFilterCriteria.OrderByPopular
                        ? query.OrderBy(si => si.ModifiedDate)
                    //todo: (nheskew)order by some pre-calculated popularity score
                        : query.OrderByDescending(si => si.ModifiedDate);

            return query;
        }

        private Speaker projectSpeaker(OxiteConferencesDataContext context, oxite_Conferences_Speaker s)
        {
            return projectSpeaker(s, true);
        }

        private Speaker projectSpeaker(oxite_Conferences_Speaker s, bool loadScheduleItems)
        {
            IEnumerable<ScheduleItem> scheduleItems = !loadScheduleItems ? Enumerable.Empty<ScheduleItem>() : projectScheduleItems(
                    from sisr in context.oxite_Conferences_ScheduleItemSpeakerRelationships
                    join si in context.oxite_Conferences_ScheduleItems on sisr.ScheduleItemID equals si.ScheduleItemID
                    where sisr.SpeakerID == s.SpeakerID
                    orderby si.StartTime descending //TODO: (erikpo) Not sure how these should be ordered.  Fix.
                    select si
                    ).ToArray();

            return new Speaker(s.SpeakerID, s.SpeakerName, s.SpeakerDisplayName, s.Bio, scheduleItems);
        }

        private IQueryable<ScheduleItem> projectScheduleItems(IQueryable<oxite_Conferences_ScheduleItem> scheduleItems)
        {
            return
                from si in scheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                let s = getSpeakersQuery(si.ScheduleItemID)
                let t = getTagsQuery(si.ScheduleItemID)
                let c = getCommentsQuery(si.ScheduleItemID)
                select
                    new ScheduleItem(
                        new Event(e.EventID, e.EventName, e.EventDisplayName, e.Year),
                        si.ScheduleItemID,
                        si.Title,
                        si.Body,
                        si.Location,
                        si.Code,
                        si.Type,
                        si.StartTime,
                        si.EndTime,
                        si.Slug,
                        s.ToList(),
                        t.ToList(),
                        c.ToList(),
                        si.CreatedDate,
                        si.ModifiedDate
                        );
        }

        private IQueryable<Speaker> getSpeakersQuery(Guid scheduleItemID)
        {
            return
                from sisr in context.oxite_Conferences_ScheduleItemSpeakerRelationships
                join s in context.oxite_Conferences_Speakers on sisr.SpeakerID equals s.SpeakerID
                where sisr.ScheduleItemID == scheduleItemID
                select projectSpeaker(s, false);
        }

        private IQueryable<ScheduleItemTag> getTagsQuery(Guid scheduleItemID)
        {
            return
                from sitr in context.oxite_Conferences_ScheduleItemTagRelationships
                where sitr.ScheduleItemID == scheduleItemID
                orderby sitr.TagDisplayName
                select new ScheduleItemTag(sitr.TagID, sitr.TagDisplayName);
        }

        private IQueryable<ScheduleItemComment> getCommentsQuery(Guid scheduleItemID)
        {
            return
                from sicr in context.oxite_Conferences_ScheduleItemCommentRelationships
                join si in context.oxite_Conferences_ScheduleItems on sicr.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where sicr.ScheduleItemID == scheduleItemID
                select new ScheduleItemComment(sicr.CommentID, new ScheduleItemSmall(si.ScheduleItemID, e.EventName, si.Slug, si.Title), sicr.Slug);
        }

        private bool getSubscriptionExists(ScheduleItemSmall scheduleItem, Guid creatorUserID)
        {
            return (
                from s in context.oxite_Subscriptions
                join sisr in context.oxite_Conferences_ScheduleItemSubscriptionRelationships on s.SubscriptionID equals sisr.SubscriptionID
                join si in context.oxite_Conferences_ScheduleItems on sisr.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where /*e.SiteID == siteID && */string.Compare(e.EventName, scheduleItem.EventName, true) == 0 && si.Slug == scheduleItem.Slug && s.UserID == creatorUserID
                select sisr
                ).Any();
        }

        private bool getSubscriptionExists(ScheduleItemSmall scheduleItem, string creatorEmail)
        {
            Guid userID = context.oxite_Users.Single(u => u.Username == "Anonymous").UserID;

            return (
                       from s in context.oxite_Subscriptions
                       join sisr in context.oxite_Conferences_ScheduleItemSubscriptionRelationships on s.SubscriptionID equals sisr.SubscriptionID
                       join si in context.oxite_Conferences_ScheduleItems on sisr.ScheduleItemID equals si.ScheduleItemID
                       join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                       where /*e.SiteID == siteID && */string.Compare(e.EventName, scheduleItem.EventName, true) == 0 && si.Slug == scheduleItem.Slug && s.UserID == userID && s.UserEmail == creatorEmail
                       select s
                   ).Any();
        }

        #endregion
    }
}
