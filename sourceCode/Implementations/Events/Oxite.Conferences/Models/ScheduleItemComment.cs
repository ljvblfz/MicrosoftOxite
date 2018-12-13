//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Conferences.Models
{
    [DataContract]
    public class ScheduleItemComment : Comment, ICacheEntity, ISecureEntity
    {
        public ScheduleItemComment(Guid id)
            : base(id)
        {
        }

        public ScheduleItemComment(Guid id, ScheduleItemSmall scheduleItem, string slug)
            : this(id)
        {
            ScheduleItem = scheduleItem;
            Slug = slug;
        }

        public ScheduleItemComment(ScheduleItemSmall scheduleItem, Comment comment, string commentSlug)
            : base(comment.Body, comment.Created, comment.CreatorUserID, comment.CreatorName, comment.CreatorEmail, comment.CreatorEmailHash, comment.CreatorUrl, comment.CreatorIP, comment.CreatorUserAgent, comment.ID, comment.Language, comment.Modified, comment.Parent, comment.State)
        {
            if (comment.Parent != null)
                Parent = new ScheduleItemCommentSmall(comment.Parent, commentSlug);
            ScheduleItem = scheduleItem;
            Slug = commentSlug;
        }

        public ScheduleItemComment(string body, DateTime created, UserAnonymous creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, language, modified, state)
        {
            Slug = slug;
        }

        public ScheduleItemComment(string body, DateTime created, UserAuthenticated creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, language, modified, state)
        {
            Slug = slug;
        }

        public ScheduleItemComment(string body, DateTime created, UserAuthenticated creator, long creatorIP, string creatorUserAgent, Guid id, Language language, DateTime modified, ScheduleItemCommentSmall parent, ScheduleItemSmall scheduleItem, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, id, language, modified, null, state)
        {
            Parent = parent;
            ScheduleItem = scheduleItem;
            Slug = slug;
        }

        public ScheduleItemComment(string body, DateTime created, UserAnonymous creator, long creatorIP, string creatorUserAgent, Guid id, Language language, DateTime modified, ScheduleItemCommentSmall parent, ScheduleItemSmall scheduleItem, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, id, language, modified, null, state)
        {
            Parent = parent;
            ScheduleItem = scheduleItem;
            Slug = slug;
        }

        public ScheduleItemComment(string body, Guid creatorUserID, string creatorName, string creatorEmail, string creatorEmailHash, string creatorUrl, long creatorIP, string creatorUserAgent, Language language, ScheduleItemCommentSmall parent, string slug, EntityState state)
            : base(body, creatorUserID, creatorName, creatorEmail, creatorEmailHash, creatorUrl, creatorIP, creatorUserAgent, language, null, state)
        {
            Parent = parent;
            Slug = slug;
        }

        public new ScheduleItemCommentSmall Parent { get; private set; }
        public ScheduleItemSmall ScheduleItem { get; private set; }
        public string Slug { get; private set; }

        #region ICacheEntity Members

        IEnumerable<ICacheEntity> ICacheEntity.GetCacheDependencyItems()
        {
            return Enumerable.Empty<ICacheEntity>();
        }

        #endregion

        #region ISecureEntity Members

        public bool IsInRole(UserAuthenticated user, string role)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
