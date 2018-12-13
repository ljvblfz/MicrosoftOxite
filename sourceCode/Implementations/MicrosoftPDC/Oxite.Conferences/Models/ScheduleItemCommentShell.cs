//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItemCommentShell
    {
        public ScheduleItemCommentShell(ScheduleItemSmall scheduleItem, Guid commentID, string commentSlug)
        {
            ScheduleItem = scheduleItem;
            CommentID = commentID;
            CommentSlug = commentSlug;
        }

        public ScheduleItemSmall ScheduleItem { get; private set; }
        public Guid CommentID { get; private set; }
        public string CommentSlug { get; private set; }
    }
}
