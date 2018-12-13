// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using Oxite.Models;
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Conferences.Services
{
    public interface IConferencesCommentService
    {
        ModelResult<ScheduleItemComment> AddComment(ScheduleItemAddress scheduleItemAddress, CommentInput commentInput);
        bool RemoveComment(ScheduleItemCommentAddress commentAddress);
        bool ApproveComment(ScheduleItemCommentAddress commentAddress);
    }
}
