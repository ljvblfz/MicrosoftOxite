﻿//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Runtime.Serialization;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Conferences.Models
{
    [DataContract]
    public class ScheduleItemCommentSmall : CommentSmall
    {
        public ScheduleItemCommentSmall(Guid id)
            : base(id)
        {
        }

        public ScheduleItemCommentSmall(CommentSmall commentSmall, string slug)
            : base(commentSmall.ID, commentSmall.Created, commentSmall.CreatorName, commentSmall.CreatorEmailHash, commentSmall.CreatorUrl)
        {
            Slug = slug;
        }

        [DataMember]
        public string Slug { get; private set; }
    }
}
