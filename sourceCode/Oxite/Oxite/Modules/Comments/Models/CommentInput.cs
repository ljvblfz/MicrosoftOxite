//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;

namespace Oxite.Modules.Comments.Models
{
    public class CommentInput
    {
        public CommentInput(Guid parentID, string body, bool subscribe)
        {
            ParentID = parentID;
            Body = body;
            Subscribe = subscribe;
        }

        public CommentInput(Guid parentID, string body, bool subscribe, bool saveAnonymousUser, UserAnonymous creator)
            : this(parentID, body, subscribe)
        {
            SaveAnonymousUser = saveAnonymousUser;
            Creator = creator;
        }

        public CommentInput(Guid id, Guid parentID, string body, bool subscribe)
            : this(parentID, body, subscribe)
        {
            ID = id;
        }

        public Guid ID { get; private set; }
        public Guid ParentID { get; private set; }
        public string Body { get; private set; }
        public UserAnonymous Creator { get; private set; }
        public bool Subscribe { get; private set; }
        public bool SaveAnonymousUser { get; private set; }
    }
}
