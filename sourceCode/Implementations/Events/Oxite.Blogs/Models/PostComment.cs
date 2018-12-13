//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Blogs.Models
{
    [DataContract]
    public class PostComment : Comment, ICacheEntity, ISecureEntity
    {
        public PostComment(Guid id)
            : base(id)
        {
        }

        public PostComment(Guid id, PostSmall post, string slug)
            : this(id)
        {
            Post = post;
            Slug = slug;
        }

        public PostComment(PostSmall post, Comment comment, string commentSlug)
            : base(comment.Body, comment.Created, comment.CreatorUserID, comment.CreatorName, comment.CreatorEmail, comment.CreatorEmailHash, comment.CreatorUrl, comment.CreatorIP, comment.CreatorUserAgent, comment.ID, comment.Language, comment.Modified, comment.Parent, comment.State)
        {
            if (comment.Parent != null)
                Parent = new PostCommentSmall(comment.Parent, commentSlug);
            Post = post;
            Slug = commentSlug;
        }
        
        public PostComment(string body, DateTime created, UserAnonymous creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, language, modified, state)
        {
            Slug = slug;
        }

        public PostComment(string body, DateTime created, UserAuthenticated creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, language, modified, state)
        {
            Slug = slug;
        }

        public PostComment(string body, DateTime created, UserAuthenticated creator, long creatorIP, string creatorUserAgent, Guid id, Language language, DateTime modified, PostCommentSmall parent, PostSmall post, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, id, language, modified, null, state)
        {
            Parent = parent;
            Post = post;
            Slug = slug;
        }

        public PostComment(string body, DateTime created, UserAnonymous creator, long creatorIP, string creatorUserAgent, Guid id, Language language, DateTime modified, PostCommentSmall parent, PostSmall post, string slug, EntityState state)
            : base(body, created, creator, creatorIP, creatorUserAgent, id, language, modified, null, state)
        {
            Parent = parent;
            Post = post;
            Slug = slug;
        }

        public PostComment(string body, Guid creatorUserID, string creatorName, string creatorEmail, string creatorEmailHash, string creatorUrl, long creatorIP, string creatorUserAgent, Language language, PostCommentSmall parent, string slug, EntityState state)
            : base(body, creatorUserID, creatorName, creatorEmail, creatorEmailHash, creatorUrl, creatorIP, creatorUserAgent, language, null, state)
        {
            Parent = parent;
            Slug = slug;
        }

        public new PostCommentSmall Parent { get; private set; }
        public PostSmall Post { get; private set; }
        public string Slug { get; private set; }

        #region ICacheEntity Members

        IEnumerable<ICacheEntity> ICacheEntity.GetCacheDependencyItems()
        {
            return new EntityBase[] { new Post(Post.ID) };
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
