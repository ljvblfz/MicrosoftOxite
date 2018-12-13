//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Modules.Conferences.Models;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Conferences.Repositories.SqlServer
{
    public class SqlServerConferencesCommentRepository : IConferencesCommentRepository
    {
        private readonly OxiteConferencesDataContext context;

        public SqlServerConferencesCommentRepository(OxiteConferencesDataContext context)
        {
            this.context = context;
        }

        public ScheduleItemCommentShell GetComment(string eventName, string scheduleItemSlug, string commentSlug)
        {
            return (
                from sicr in context.oxite_Conferences_ScheduleItemCommentRelationships
                join si in context.oxite_Conferences_ScheduleItems on sicr.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                where /*e.SiteID == siteID && */string.Compare(e.EventName, eventName, true) == 0 && si.Slug == scheduleItemSlug && sicr.Slug == commentSlug
                select new ScheduleItemCommentShell(new ScheduleItemSmall(si.ScheduleItemID, e.EventName, si.Slug, si.Title), sicr.CommentID, sicr.Slug)
                ).FirstOrDefault();
        }

        public ScheduleItemComment Save(ScheduleItemComment comment)
        {
            oxite_Comment commentToSave = null;

            if (comment.ID != Guid.Empty)
                commentToSave = context.oxite_Comments.FirstOrDefault(c => c.CommentID == comment.ID);

            if (commentToSave == null)
            {
                commentToSave = new oxite_Comment();

                commentToSave.CommentID = comment.ID != Guid.Empty ? comment.ID : Guid.NewGuid();
                commentToSave.CreatedDate = commentToSave.ModifiedDate = DateTime.UtcNow;

                context.oxite_Comments.InsertOnSubmit(commentToSave);
            }
            else
                commentToSave.ModifiedDate = DateTime.UtcNow;

            oxite_Conferences_ScheduleItem scheduleItem = (from si in context.oxite_Conferences_ScheduleItems join e in context.oxite_Conferences_Events on si.EventID equals e.EventID where /*e.SiteID == siteID && */string.Compare(e.EventName, comment.ScheduleItem.EventName, true) == 0 && string.Compare(si.Slug, comment.ScheduleItem.Slug, true) == 0 select si).FirstOrDefault();
            if (scheduleItem == null) throw new InvalidOperationException(string.Format("ScheduleItem in Event {0} at slug {1} could not be found to add the comment to", comment.ScheduleItem.EventName, comment.ScheduleItem.Slug));
            context.oxite_Conferences_ScheduleItemCommentRelationships.InsertOnSubmit(
                new oxite_Conferences_ScheduleItemCommentRelationship
                {
                    CommentID = commentToSave.CommentID,
                    ScheduleItemID = scheduleItem.ScheduleItemID,
                    Slug = comment.Slug
                }
                );

            commentToSave.ParentCommentID = comment.Parent != null && comment.Parent.ID != Guid.Empty ? comment.Parent.ID : commentToSave.CommentID;
            commentToSave.Body = comment.Body;
            commentToSave.CreatorIP = comment.CreatorIP;
            commentToSave.State = (byte)comment.State;
            commentToSave.UserAgent = comment.CreatorUserAgent;
            commentToSave.oxite_Language = context.oxite_Languages.Where(l => l.LanguageName == comment.Language.Name).FirstOrDefault();

            if (comment.CreatorUserID != Guid.Empty)
                commentToSave.CreatorUserID = comment.CreatorUserID;
            else
            {
                oxite_User anonymousUser = context.oxite_Users.FirstOrDefault(u => u.Username == "Anonymous");
                if (anonymousUser == null) throw new InvalidOperationException("Could not find anonymous user");
                commentToSave.CreatorUserID = anonymousUser.UserID;

                commentToSave.CreatorName = comment.CreatorName;
                commentToSave.CreatorEmail = comment.CreatorEmail;
                commentToSave.CreatorHashedEmail = comment.CreatorEmailHash;
                commentToSave.CreatorUrl = comment.CreatorUrl;
            }

            context.SubmitChanges();

            return (
                from c in context.oxite_Comments
                join sicr in context.oxite_Conferences_ScheduleItemCommentRelationships on c.CommentID equals sicr.CommentID
                join si in context.oxite_Conferences_ScheduleItems on sicr.ScheduleItemID equals si.ScheduleItemID
                join e in context.oxite_Conferences_Events on si.EventID equals e.EventID
                join u in context.oxite_Users on c.CreatorUserID equals u.UserID
                where /*e.SiteID == siteID && */string.Compare(e.EventName, comment.ScheduleItem.EventName, true) == 0 && string.Compare(si.Slug, scheduleItem.Slug, true) == 0 && string.Compare(sicr.Slug, comment.Slug, true) == 0
                select projectComment(c, sicr, si, e, u)
                ).FirstOrDefault();
        }

        private ScheduleItemComment projectComment(oxite_Comment comment, oxite_Conferences_ScheduleItemCommentRelationship sicr, oxite_Conferences_ScheduleItem si, oxite_Conferences_Event e, oxite_User user)
        {
            ScheduleItemCommentSmall parent = comment.ParentCommentID != comment.CommentID ? getParentComment(comment.ParentCommentID) : null;
            Language language = new Language(comment.oxite_Language.LanguageID)
            {
                DisplayName = comment.oxite_Language.LanguageDisplayName,
                Name = comment.oxite_Language.LanguageName
            };

            if (user.Username != "Anonymous")
                return new ScheduleItemComment(comment.Body, comment.CreatedDate, getUserAuthenticated(comment, user), comment.CreatorIP, comment.UserAgent, comment.CommentID, language, comment.ModifiedDate, parent, new ScheduleItemSmall(si.ScheduleItemID, e.EventName, si.Slug, si.Title), sicr.Slug, (EntityState)comment.State);
            else
                return new ScheduleItemComment(comment.Body, comment.CreatedDate, getUserAnonymous(comment, user), comment.CreatorIP, comment.UserAgent, comment.CommentID, language, comment.ModifiedDate, parent, new ScheduleItemSmall(si.ScheduleItemID, e.EventName, si.Slug, si.Title), sicr.Slug, (EntityState)comment.State);
        }

        private ScheduleItemCommentSmall getParentComment(Guid commentID)
        {
            return (
                from c in context.oxite_Comments
                join sicr in context.oxite_Conferences_ScheduleItemCommentRelationships on c.CommentID equals sicr.CommentID
                join u in context.oxite_Users on c.CreatorUserID equals u.UserID
                where c.State != (byte)EntityState.Removed && c.CommentID == commentID
                select projectScheduleItemCommentSmall(c, sicr, u)
                ).FirstOrDefault();
        }

        private static User getUserAuthenticated(oxite_Comment comment, oxite_User user)
        {
            return new User(user.UserID, user.Username, user.DisplayName, user.Email, user.HashedEmail, (EntityState)user.Status);
        }

        private static UserAnonymous getUserAnonymous(oxite_Comment comment, oxite_User user)
        {
            return new UserAnonymous(comment.CreatorName, comment.CreatorEmail, comment.CreatorHashedEmail, comment.CreatorUrl);
        }

        private static ScheduleItemCommentSmall projectScheduleItemCommentSmall(oxite_Comment comment, oxite_Conferences_ScheduleItemCommentRelationship sicr, oxite_User user)
        {
            if (user.Username != "Anonymous")
                return new ScheduleItemCommentSmall(new CommentSmall(comment.CommentID, comment.CreatedDate, getUserAuthenticated(comment, user)), sicr.Slug);
            else
                return new ScheduleItemCommentSmall(new CommentSmall(comment.CommentID, comment.CreatedDate, getUserAnonymous(comment, user)), sicr.Slug);
        }
    }
}
