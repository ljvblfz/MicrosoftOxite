//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Conferences.Models;

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

        public ScheduleItem GetScheduleItem(/*Guid siteID, */string eventName, string slug)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where /*e.SiteID == siteID && */string.Compare(e.EventName, eventName, true) == 0 && string.Compare(si.Slug, slug, true) == 0
                select si;

            return projectScheduleItems(query).FirstOrDefault();
        }

        public IQueryable<ScheduleItem> GetScheduleItems(EventAddress eventAddress, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return GetScheduleItems(eventAddress, Guid.Empty, scheduleItemFilterCriteria);
        }

        public IQueryable<ScheduleItem> GetScheduleItems(EventAddress eventAddress, Guid userID, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            return projectScheduleItems(getScheduleItems(eventAddress, userID, scheduleItemFilterCriteria));
        }

        public IQueryable<ScheduleItem> GetScheduleItemsByTag(EventAddress eventAddress, Guid userID, Guid tagID, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            var query =
                getScheduleItems(eventAddress, userID, scheduleItemFilterCriteria)
                .Where(si => si.oxite_Conferences_ScheduleItemTagRelationships.Any(sitr => sitr.TagID == tagID));

            return projectScheduleItems(query);
        }

        public IQueryable<ScheduleItem> GetScheduleItems(EventAddress eventAddress, string type)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where string.Compare(e.EventName, eventAddress.EventName, true) == 0 && string.Compare(si.Type, type, true) == 0
                select si;

            return projectScheduleItems(query);
        }

        public IQueryable<ScheduleItem> GetScheduleItemsBySpeaker(EventAddress eventAddress, string name)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join sis in context.oxite_Conferences_ScheduleItemSpeakerRelationships on si.ScheduleItemID equals sis.ScheduleItemID
                join s in context.oxite_Conferences_Speakers on sis.SpeakerID equals s.SpeakerID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where string.Compare(e.EventName, eventAddress.EventName, true) == 0 && string.Compare(s.SpeakerName, name, true) == 0
                select si;

            return projectScheduleItems(query);
        }

        public IQueryable<ScheduleItem> GetScheduleItemsByTimeSlot(EventAddress eventAddress, DateRangeAddress dateRangeAddress)
        {
            var query =
                from si in context.oxite_Conferences_ScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where string.Compare(e.EventName, eventAddress.EventName, true) == 0 && si.StartTime >= dateRangeAddress.StartDate && si.EndTime <= dateRangeAddress.EndDate
                select si;

            return projectScheduleItems(query);
        }

        public IQueryable<ScheduleItem> GetScheduleItemsByTimeSlotAndUser(EventAddress eventAddress, DateRangeAddress dateRangeAddress, Guid userID)
        {
            IQueryable<oxite_Conferences_ScheduleItem> query =
                from si in context.oxite_Conferences_ScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where string.Compare(e.EventName, eventAddress.EventName, true) == 0 
                    && si.StartTime >= dateRangeAddress.StartDate && si.EndTime <= dateRangeAddress.EndDate
                select si;

            query = query.Where(
                    si => si.oxite_Conferences_ScheduleItemUserRelationships
                              .Any(siu => siu.UserID == userID)
                    );

            return projectScheduleItems(query);
        }

        public IQueryable<ScheduleItem> GetScheduleItemsByFlag(EventAddress eventAddress, string flagName)
        {
            var query =
                from sif in context.oxite_Conferences_ScheduleItemFlags
                join si in context.oxite_Conferences_ScheduleItems on sif.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where string.Compare(e.EventName, eventAddress.EventName, true) == 0 && string.Compare(sif.FlagType, flagName, true) == 0
                select si;

            return projectScheduleItems(query);
        }

        public IQueryable<ScheduleItemTag> GetScheduleItemTags(EventAddress eventAddress)
        {
            return GetScheduleItemTags(eventAddress, true /* filterUnpublishedScheduleItems */);
        }

        public IQueryable<ScheduleItemTag> GetScheduleItemTags(EventAddress eventAddress, bool filterUnpublishedScheduleItems)
        {
            // [DC]: Tags with no published sessions should be filtered for display purposes
            var local = DateTime.Now;
            var utc = DateTime.UtcNow;
            var offset = local.Hour - utc.Hour; // i.e. GMT-5

            return
                from sitr in context.oxite_Conferences_ScheduleItemTagRelationships
                join si in context.oxite_Conferences_ScheduleItems on sitr.ScheduleItemID equals si.ScheduleItemID
                where (filterUnpublishedScheduleItems && (!si.PublishedDate.HasValue || (si.PublishedDate.Value.AddHours(offset) <= utc))) 
                      || !filterUnpublishedScheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                group si by new { sitr.TagID, sitr.TagDisplayName, Evnt = e } into results
                where string.Compare(results.Key.Evnt.EventName, eventAddress.EventName, true) == 0
                select new ScheduleItemTag(results.Key.TagID, results.Key.TagDisplayName);
        }

        public IQueryable<ScheduleItem> GetScheduleItemsByUser(EventAddress eventAddress, Guid userID)
        {
            var query =
                from siur in context.oxite_Conferences_ScheduleItemUserRelationships
                join si in context.oxite_Conferences_ScheduleItems on siur.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where string.Compare(e.EventName, eventAddress.EventName, true) == 0
                where siur.UserID.Equals(userID)
                orderby si.StartTime
                select si ;

            return projectScheduleItems(query);
        }

        public ScheduleItemTag GetScheduleItemTag(Guid tagID)
        {
            return
                (from sitr in context.oxite_Conferences_ScheduleItemTagRelationships
                where sitr.TagID == tagID
                select new ScheduleItemTag(tagID, sitr.TagDisplayName)).FirstOrDefault();
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

        public void AddSubscription(Guid siteID, ScheduleItemSmall scheduleItem, Guid creatorUserID)
        {
            if (getSubscriptionExists(siteID, scheduleItem, creatorUserID)) return;

            oxite_Subscription subscription = new oxite_Subscription { SubscriptionID = Guid.NewGuid(), UserID = creatorUserID };

            context.oxite_Subscriptions.InsertOnSubmit(subscription);
            context.oxite_Conferences_ScheduleItemSubscriptionRelationships.InsertOnSubmit(new oxite_Conferences_ScheduleItemSubscriptionRelationship { SubscriptionID = subscription.SubscriptionID, ScheduleItemID = GetScheduleItem(scheduleItem.EventName, scheduleItem.Slug).ID });

            context.SubmitChanges();
        }

        public void AddSubscription(Guid siteID, ScheduleItemComment comment)
        {
            if (getSubscriptionExists(siteID, comment.ScheduleItem, comment.CreatorEmail)) return;

            oxite_Subscription subscription = new oxite_Subscription { SubscriptionID = Guid.NewGuid(), UserID = context.oxite_Users.Single(u => u.Username == "Anonymous").UserID, UserName = comment.CreatorName, UserEmail = comment.CreatorEmail };

            context.oxite_Subscriptions.InsertOnSubmit(subscription);
            context.oxite_Conferences_ScheduleItemSubscriptionRelationships.InsertOnSubmit(new oxite_Conferences_ScheduleItemSubscriptionRelationship { SubscriptionID = subscription.SubscriptionID, ScheduleItemID = GetScheduleItem(comment.ScheduleItem.EventName, comment.ScheduleItem.Slug).ID });

            context.SubmitChanges();
        }

        public void AddUserRelationship(ScheduleItemSmall scheduleItem, Guid userID)
        {
            if(scheduleItem == null || getUserRelationshipExists(scheduleItem, userID))
            {
                return;
            }

            var relationship = new oxite_Conferences_ScheduleItemUserRelationship()
                                   {
                                     ScheduleItemID = scheduleItem.ID,
                                     UserID = userID
                                   };

            context.oxite_Conferences_ScheduleItemUserRelationships.InsertOnSubmit(relationship);
            context.SubmitChanges();
        }

        public void RemoveUserRelationship(ScheduleItemSmall scheduleItem, Guid userID)
        {
            if (scheduleItem == null || !getUserRelationshipExists(scheduleItem, userID))
            {
                return;
            }

            var relationship =
                context.oxite_Conferences_ScheduleItemUserRelationships.Single(
                    r => r.ScheduleItemID.Equals(scheduleItem.ID) && r.UserID.Equals(userID));
            
            context.oxite_Conferences_ScheduleItemUserRelationships.DeleteOnSubmit(relationship);
            context.SubmitChanges();
        }

        public IQueryable<ScheduleItemUser> GetScheduleItemUsers(Guid scheduleItemID, Guid userID)
        {
            var scheduleUsers = context.oxite_Conferences_ScheduleItemUserRelationships
                .Where(u => u.UserID.Equals(userID) && u.ScheduleItemID.Equals(scheduleItemID));

            return scheduleUsers.Select(s => projectScheduleUser(s));
        }

        #endregion

        #region Private Methods

        private IQueryable<oxite_Conferences_ScheduleItem> getScheduleItems(EventAddress eventAddress, Guid userID, ScheduleItemFilterCriteria scheduleItemFilterCriteria)
        {
            IQueryable<oxite_Conferences_ScheduleItem> query = from si in context.oxite_Conferences_ScheduleItems
                                                               select si;

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
                        sis.oxite_Conferences_Speaker.SpeakerFirstName.Contains(scheduleItemFilterCriteria.Term) ||
                        sis.oxite_Conferences_Speaker.SpeakerLastName.Contains(scheduleItemFilterCriteria.Term) ||
                        si.oxite_Conferences_ScheduleItemTagRelationships.Any(
                        sitr =>
                        sitr.TagDisplayName.Contains(scheduleItemFilterCriteria.Term))
                        )
                    );

            if (scheduleItemFilterCriteria.ForUser && userID != Guid.Empty)
                query = query
                    .Where(
                    si => si.oxite_Conferences_ScheduleItemUserRelationships
                              .Any(siu => siu.UserID == userID)
                    );

            if (eventAddress != null)
                query = query
                    .Where(si => string.Compare(si.oxite_Conferences_Event.EventName, eventAddress.EventName, true) == 0);

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
                        ? query.OrderBy(si => si.StartTime)
                    //todo: (nheskew)order by some pre-calculated popularity score
                        : query.OrderBy(si => si.StartTime);

            if (scheduleItemFilterCriteria.ForUser && userID != Guid.Empty)
            {
                query = query.OrderBy(s => s.StartTime);
            }

            // note only videos supported currently
            if(scheduleItemFilterCriteria.WithFileTypes.Count > 0 &&
               scheduleItemFilterCriteria.WithFileTypes.Contains("video"))
            {
                query = query.Where(si => si.oxite_Conferences_ScheduleItemFileRelationships
                                              .Any(
                                              sif =>
                                              sif.oxite_File.TypeName == "WMVHigh" ||
                                              sif.oxite_File.TypeName == "WMVStreamingOnly"));
            }

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

            return new Speaker(s.SpeakerID, s.SpeakerName, s.SpeakerDisplayName, s.SpeakerFirstName, s.SpeakerLastName, s.Bio, s.LargeImage, s.SmallImage, s.Twitter, scheduleItems);
        }

        private IQueryable<ScheduleItem> projectScheduleItems(IQueryable<oxite_Conferences_ScheduleItem> scheduleItems)
        {
            var local = DateTime.Now;
            var utc = DateTime.UtcNow;
            var offset = local.Hour - utc.Hour; // i.e. GMT-5

            return
                from si in scheduleItems
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where !si.PublishedDate.HasValue || (si.PublishedDate.Value.AddHours(offset) <= utc)
                let s = getSpeakersQuery(si.ScheduleItemID)
                let t = getTagsQuery(si.ScheduleItemID)
                let c = getCommentsQuery(si.ScheduleItemID)
                let u = getUsersQuery(si.ScheduleItemID)
                let f = getFilesQuery(si.ScheduleItemID)
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
                        u.ToList(),
                        si.CreatedDate,
                        si.ModifiedDate,
                        si.PublishedDate,
                        f.ToArray()
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
                from c in context.oxite_Comments
                join sicr in context.oxite_Conferences_ScheduleItemCommentRelationships on c.CommentID equals sicr.CommentID
                join si in context.oxite_Conferences_ScheduleItems on sicr.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where sicr.ScheduleItemID == scheduleItemID && c.State != (byte)EntityState.Removed
                select new ScheduleItemComment(sicr.CommentID, new ScheduleItemSmall(si.ScheduleItemID, e.EventName, si.Slug, si.Title), sicr.Slug);
        }

        private IQueryable<ScheduleItemUser> getUsersQuery(Guid scheduleItemID)
        {
            return
                from siur in context.oxite_Conferences_ScheduleItemUserRelationships
                join u in context.oxite_Users on siur.UserID equals u.UserID
                where siur.ScheduleItemID == scheduleItemID
                select new ScheduleItemUser(u.UserID, u.Username);
        }

        private IQueryable<File> getFilesQuery(Guid scheduleItemID)
        {
            return
                from sifr in context.oxite_Conferences_ScheduleItemFileRelationships
                join f in context.oxite_Files on sifr.FileID equals f.FileID
                where sifr.ScheduleItemID == scheduleItemID
                select new File(f.FileID, f.TypeName, f.MimeType, new Uri(f.Url), f.Length);
        }

        private bool getSubscriptionExists(Guid siteID, ScheduleItemSmall scheduleItem, Guid creatorUserID)
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

        private bool getSubscriptionExists(Guid siteID, ScheduleItemSmall scheduleItem, string creatorEmail)
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

        private bool getUserRelationshipExists(ScheduleItemSmall scheduleItem, Guid userID)
        {
            return (
                       from si in context.oxite_Conferences_ScheduleItems
                       join siur in context.oxite_Conferences_ScheduleItemUserRelationships on si.ScheduleItemID equals siur.ScheduleItemID
                       join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                       where string.Compare(e.EventName, scheduleItem.EventName, true) == 0 && si.ScheduleItemID == scheduleItem.ID && siur.UserID == userID
                       select si
                   ).Any();
        }

        private ScheduleItemUser projectScheduleUser(oxite_Conferences_ScheduleItemUserRelationship scheduleItemUserRelationship)
        {
            var result = new ScheduleItemUser(scheduleItemUserRelationship.UserID,
                                              context.oxite_Users.Single(
                                                  u => u.UserID.Equals(scheduleItemUserRelationship.UserID)).Username);

            return result;
        }

        #endregion
    }
}
