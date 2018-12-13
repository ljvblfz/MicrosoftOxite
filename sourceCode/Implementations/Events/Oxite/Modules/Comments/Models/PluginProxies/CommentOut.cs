//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Plugins.Models
{
    public class CommentOut
    {
        private readonly Comment original;

        public CommentOut(Comment comment, string url)
        {
            original = comment;

            Parent = new CommentSmallReadOnly(comment.Parent);
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
            Url = url;
        }

        public CommentSmallReadOnly Parent { get; private set; }
        public Guid CreatorUserID { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorEmailHash { get; set; }
        public string CreatorUrl { get; set; }
        public long CreatorIP { get; private set; }
        public string CreatorUserAgent { get; private set; }
        public Language Language { get; private set; }
        public string Body { get; set; }
        public EntityState State { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public string Url { get; private set; }
    }
}
