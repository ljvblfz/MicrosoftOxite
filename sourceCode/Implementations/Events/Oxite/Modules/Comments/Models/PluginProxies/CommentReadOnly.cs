//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Comments.Models;

namespace Oxite.Plugins.Models
{
    public class CommentReadOnly
    {
        public CommentReadOnly(Comment comment, string url)
        {
            Parent = comment.Parent != null ? new CommentSmallReadOnly(comment.Parent) : null;
            CreatorName = comment.CreatorName;
            CreatorEmail = comment.CreatorEmail;
            CreatorEmailHash = comment.CreatorEmailHash;
            CreatorUrl = comment.CreatorUrl;
            CreatorIP = comment.CreatorIP;
            CreatorUserAgent = comment.CreatorUserAgent;
            Body = comment.Body;
            State = (State)(byte)comment.State;
            Created = comment.Created;
            Modified = comment.Modified;
            Url = url;
        }

        public CommentSmallReadOnly Parent { get; private set; }
        public string CreatorName { get; private set; }
        public string CreatorEmail { get; private set; }
        public string CreatorEmailHash { get; private set; }
        public string CreatorUrl { get; private set; }
        public long CreatorIP { get; private set; }
        public string CreatorUserAgent { get; private set; }
        public string Body { get; private set; }
        public State State { get; private set; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public string Url { get; private set; }
    }
}
