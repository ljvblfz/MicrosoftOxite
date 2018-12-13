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

namespace Oxite.Modules.Comments.Models
{
    [DataContract]
    public class Comment : EntityBase
    {
        private readonly UserAnonymous userAnonymous;
        private readonly User user;

        public Comment(Guid id)
            : base(id)
        {
        }

        public Comment(string body, Guid creatorUserID, string creatorName, string creatorEmail, string creatorEmailHash, string creatorUrl, long creatorIP, string creatorUserAgent, Language language, CommentSmall parent, EntityState state)
        {
            Body = body;
            CreatorUserID = creatorUserID;
            CreatorName = creatorName;
            CreatorEmail = creatorEmail;
            CreatorEmailHash = creatorEmailHash;
            CreatorUrl = creatorUrl;
            CreatorIP = creatorIP;
            CreatorUserAgent = creatorUserAgent;
            Parent = parent;
            Language = language;
            State = state;
        }

        public Comment(string body, DateTime created, UserAnonymous creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, EntityState state)
            : this(body, Guid.Empty, creator.Name, creator.Email, creator.EmailHash, creator.Url, creatorIP, creatorUserAgent, language, null, state)
        {
            Created = created;
            userAnonymous = creator;
        }

        public Comment(string body, DateTime created, User creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, EntityState state)
            : this(body, creator.ID, null, null, null, null, creatorIP, creatorUserAgent, language, null, state)
        {
            Created = created;
            user = creator;
        }

        public Comment(string body, DateTime created, UserAnonymous creator, long creatorIP, string creatorUserAgent, Guid id, Language language, DateTime modified, CommentSmall parent, EntityState state)
            : this(id)
        {
            Body = body;
            Created = created;
            CreatorName = creator.Name;
            CreatorEmail = creator.Email;
            CreatorEmailHash = creator.EmailHash;
            CreatorUrl = creator.Url;
            CreatorIP = creatorIP;
            CreatorUserAgent = creatorUserAgent;
            Language = language;
            Parent = parent;
            State = state;

            userAnonymous = creator;
        }

        public Comment(string body, DateTime created, User creator, long creatorIP, string creatorUserAgent, Guid id, Language language, DateTime modified, CommentSmall parent, EntityState state)
            : this(id)
        {
            Body = body;
            Created = created;
            CreatorUserID = creator.ID;
            CreatorIP = creatorIP;
            CreatorUserAgent = creatorUserAgent;
            Parent = parent;
            Language = language;
            State = state;

            user = creator;
        }

        public Comment(string body, DateTime created, Guid creatorUserID, string creatorName, string creatorEmail, string creatorEmailHash, string creatorUrl, long creatorIP, string creatorUserAgent, Guid id, Language language, DateTime modified, CommentSmall parent, EntityState state)
            : this(id)
        {
            Body = body;
            Created = created;
            CreatorUserID = creatorUserID;
            CreatorName = creatorName;
            CreatorEmail = creatorEmail;
            CreatorEmailHash = creatorEmailHash;
            CreatorUrl = creatorUrl;
            CreatorIP = creatorIP;
            CreatorUserAgent = creatorUserAgent;
            Language = language;
            Modified = modified;
            Parent = parent;
            Language = language;
            State = state;
        }

        [DataMember]
        public CommentSmall Parent { get; private set; }
        public Guid CreatorUserID { get; private set; }
        [DataMember]
        public string CreatorName { get; private set; }
        public string CreatorEmail { get; private set; }
        [DataMember]
        public string CreatorEmailHash { get; private set; }
        [DataMember]
        public string CreatorUrl { get; private set; }
        public long CreatorIP { get; private set; }
        public string CreatorUserAgent { get; private set; }
        public Language Language { get; private set; }
        [DataMember]
        public string Body { get; private set; }
        public EntityState State { get; private set; }
        [DataMember]
        public DateTime Created { get; protected set; }
        public DateTime Modified { get; protected set; }

        internal void Fill(Comment comment)
        {
            Parent = comment.Parent;
            CreatorUserID = comment.CreatorUserID;
            CreatorName = comment.CreatorName;
            CreatorEmail = comment.CreatorEmail;
            CreatorEmailHash = comment.CreatorEmailHash;
            CreatorUrl = comment.CreatorUrl;
            CreatorIP = comment.CreatorIP;
            CreatorUserAgent = comment.CreatorUserAgent;
            Language = comment.Language;
            Body = comment.Body;
            State = comment.State;
            Created = comment.Created;
            Modified = comment.Modified;
        }

        private void setCreatorFields(UserAnonymous creator)
        {
            CreatorUserID = Guid.Empty;
            CreatorName = creator.Name;
            CreatorEmail = creator.Email;
            CreatorEmailHash = creator.EmailHash;
            CreatorUrl = creator.Url;
        }

        private void setupCreatorFields(User creator)
        {
            CreatorUserID = creator.ID;
            CreatorName = !string.IsNullOrEmpty(creator.DisplayName) ? creator.DisplayName : creator.Name;
            CreatorEmail = creator.Email;
            CreatorEmailHash = creator.EmailHash;
            CreatorUrl = "";
        }
    }
}
