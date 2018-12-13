//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class CommentInputForImport
    {
        private readonly UserAnonymous creatorAnonymous;
        private readonly UserAuthenticated creatorAuthenticated;

        public CommentInputForImport(string body, DateTime created, UserAnonymous creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, EntityState state)
            : this(body, created, creatorIP, creatorUserAgent, language, modified, state)
        {
            creatorAnonymous = creator;
        }

        public CommentInputForImport(string body, DateTime created, UserAuthenticated creator, long creatorIP, string creatorUserAgent, Language language, DateTime modified, EntityState state)
            : this(body, created, creatorIP, creatorUserAgent, language, modified, state)
        {
            creatorAuthenticated = creator;
        }

        private CommentInputForImport(string body, DateTime created, long creatorIP, string creatorUserAgent, Language language, DateTime modified, EntityState state)
        {
            Body = body;
            Created = created;
            CreatorIP = creatorIP;
            CreatorUserAgent = creatorUserAgent;
            Modified = modified;
            State = state;
        }

        public string Body { get; private set; }
        public DateTime Created { get; private set; }
        public long CreatorIP { get; private set; }
        public string CreatorUserAgent { get; private set; }
        public Language Language { get; private set; }
        public DateTime Modified { get; private set; }
        public EntityState State { get; private set; }

        public PostComment ToComment(string slug)
        {
            if (creatorAuthenticated != null)
                return new PostComment(Body, Created, creatorAuthenticated, CreatorIP, CreatorUserAgent, Language, Modified, slug, State);
            else
                return new PostComment(Body, Created, creatorAnonymous, CreatorIP, CreatorUserAgent, Language, Modified, slug, State);
        }
    }
}
