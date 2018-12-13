//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Comments.Infrastructure;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Tags.Infrastructure;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Conferences.Models
{
    public class ScheduleItem : EntityBase, ITaggedEntity, ICommentedEntity
    {
        public ScheduleItem(Guid id)
            : base(id)
        {
        }

        public ScheduleItem(Event e, Guid id, string title, string body, string location, string code, string type, DateTime start, DateTime end, string slug, IEnumerable<Speaker> speakers, IEnumerable<ScheduleItemTag> tags, IEnumerable<ScheduleItemComment> comments, DateTime created, DateTime modified)
            : this(id)
        {
            Event = e;
            Title = title;
            Body = body;
            Location = location;
            Code = code;
            Type = type;
            Start = start;
            End = end;
            Slug = slug;
            Speakers = speakers.ToList();
            Tags = tags.ToList();
            Comments = comments.ToList();
            Created = created;
            Modified = modified;
        }

        public Event Event { get; set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Location { get; private set; }
        public string Code { get; private set; }
        public string Type { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public string Slug { get; private set; }
        public IEnumerable<Speaker> Speakers { get; private set; }
        public IEnumerable<ScheduleItemTag> Tags { get; private set; }
        public IEnumerable<ScheduleItemComment> Comments { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }

        #region ITaggedEntity Members

        public IEnumerable<Tag> GetTags()
        {
            return Tags.Cast<Tag>();
        }

        #endregion

        #region ICommentedEntity Members

        public IEnumerable<Comment> GetComments()
        {
            return Comments.Cast<Comment>();
        }

        #endregion
    }
}
