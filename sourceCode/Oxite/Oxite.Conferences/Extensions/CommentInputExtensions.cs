//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Extensions
{
    public static class CommentInputExtensions
    {
        public static ScheduleItemComment ToComment(this CommentInput commentInput, string eventName, string scheduleItemSlug, User creatorAuthenticated, long creatorIP, string creatorUserAgent, Language language, string slug, EntityState state)
        {
            if (creatorAuthenticated != null)
                return new ScheduleItemComment(commentInput.Body, creatorAuthenticated.ID, null, null, null, null, creatorIP, creatorUserAgent, language, new ScheduleItemCommentSmall(commentInput.ParentID), new ScheduleItemSmall(Guid.Empty, eventName, scheduleItemSlug, null), slug, state);
            else
                return new ScheduleItemComment(commentInput.Body, Guid.Empty, commentInput.Creator.Name, commentInput.Creator.Email, commentInput.Creator.EmailHash, commentInput.Creator.Url, creatorIP, creatorUserAgent, language, new ScheduleItemCommentSmall(commentInput.ParentID), new ScheduleItemSmall(Guid.Empty, eventName, scheduleItemSlug, null), slug, state);
        }
    }
}
