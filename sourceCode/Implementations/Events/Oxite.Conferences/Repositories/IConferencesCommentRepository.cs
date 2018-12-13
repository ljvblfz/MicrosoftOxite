//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Conferences.Models;

namespace Oxite.Modules.Conferences.Repositories
{
    public interface IConferencesCommentRepository
    {
        ScheduleItemCommentShell GetComment(Guid siteID, string eventName, string scheduleItemSlug, string commentSlug);
        ScheduleItemComment Save(ScheduleItemComment comment, Guid siteID, string eventName, string scheduleItemSlug);
    }
}
