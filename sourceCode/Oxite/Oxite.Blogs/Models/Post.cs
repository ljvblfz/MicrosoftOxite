//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Comments.Infrastructure;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Tags.Infrastructure;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class Post : EntityBase, INamedEntity, ITaggedEntity, ICommentedEntity, ICacheEntity
    {
        public Post(Guid id)
            : base(id)
        {
        }

        public Post(Blog blog, string body, string bodyShort, bool commentingDisabled, User creator, DateTime? published, string slug, EntityState state, IEnumerable<PostTag> tags, string title)
        {
            Blog = blog;
            Body = body;
            BodyShort = bodyShort;
            CommentingDisabled = commentingDisabled;
            Creator = creator;
            Published = published;
            Slug = slug;
            State = state;
            Tags = tags;
            Title = title;
        }

        public Post(Blog blog, string body, string bodyShort, bool commentingDisabled, DateTime created, User creator, Guid id, DateTime modified, DateTime? published, string slug, EntityState state, IEnumerable<PostTag> tags, string title, IEnumerable<PostComment> comments, IEnumerable<Trackback> trackbacks)
            : this(id)
        {
            Blog = blog;
            Body = body;
            BodyShort = bodyShort;
            CommentingDisabled = commentingDisabled;
            Creator = creator;
            Published = published;
            Slug = slug;
            State = state;
            Tags = tags;
            Title = title;
            Created = created;
            Comments = comments;
            Modified = modified;
            Trackbacks = trackbacks;
        }

        public Blog Blog { get; private set; }
        public string Body { get; private set; }
        public string BodyShort { get; private set; }
        public bool CommentingDisabled { get; private set; }
        public IEnumerable<PostComment> Comments { get; private set; }
        public DateTime Created { get; private set; }
        public User Creator { get; private set; }
        public DateTime Modified { get; private set; }
        public DateTime? Published { get; private set; }
        public string Slug { get; private set; }
        public EntityState State { get; private set; }
        public IEnumerable<PostTag> Tags { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<Trackback> Trackbacks { get; private set; }

        #region INamedEntity Members

        string INamedEntity.Name
        {
            get { return Slug; }
        }

        string INamedEntity.DisplayName
        {
            get { return Title; }
        }

        #endregion

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

        #region ICacheEntity Members

        IEnumerable<ICacheEntity> ICacheEntity.GetCacheDependencyItems()
        {
            List<ICacheEntity> dependencies = new List<ICacheEntity>();

            if (Blog != null)
                dependencies.Add(Blog);

            if (Tags != null)
                dependencies.AddRange(Tags.Cast<ICacheEntity>());

            return dependencies;
        }

        #endregion
    }
}
