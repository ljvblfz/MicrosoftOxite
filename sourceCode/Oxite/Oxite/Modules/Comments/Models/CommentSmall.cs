//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Runtime.Serialization;
using Oxite.Infrastructure;

namespace Oxite.Modules.Comments.Models
{
    [DataContract]
    public class CommentSmall
    {
        public CommentSmall(Guid id)
        {
            ID = id;
        }

        public CommentSmall(Guid id, DateTime created, User creator)
            : this(id, created, !string.IsNullOrEmpty(creator.DisplayName) ? creator.DisplayName : creator.Name, creator.EmailHash, "")
        {
        }

        public CommentSmall(Guid id, DateTime created, UserAnonymous creator)
            : this(id, created, creator.Name, creator.EmailHash, creator.Url)
        {
        }

        protected CommentSmall(Guid id, DateTime created, string creatorName, string creatorEmailHash, string creatorUrl)
            : this(id)
        {
            Created = created;
            CreatorName = creatorName;
            CreatorEmailHash = creatorEmailHash;
            CreatorUrl = creatorUrl;
        }

        [DataMember]
        public Guid ID { get; private set; }
        [DataMember]
        public DateTime Created { get; private set; }
        [DataMember]
        public string CreatorName { get; private set; }
        [DataMember]
        public string CreatorEmailHash { get; private set; }
        [DataMember]
        public string CreatorUrl { get; private set; }
    }
}
