//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Modules.Comments.Models;

namespace Oxite.Plugins.Models
{
    public class CommentIn
    {
        private readonly CommentInput originalInput;

        public CommentIn(CommentInput commentInput)
        {
            originalInput = commentInput;

            Body = commentInput.Body;
            if (commentInput.Creator != null)
            {
                CreatorName = commentInput.Creator.Name;
                CreatorEmail = commentInput.Creator.Email;
                CreatorUrl = commentInput.Creator.Url;
            }
            Subscribe = commentInput.Subscribe;
            SaveAnonymousUser = commentInput.SaveAnonymousUser;
        }

        public string Body { get; set; }
        public string CreatorName { get; set; }
        public string CreatorEmail { get; set; }
        public string CreatorUrl { get; set; }
        public bool Subscribe { get; private set; }
        public bool SaveAnonymousUser { get; private set; }

        public CommentInput ToCommentInput()
        {
            return new CommentInput(originalInput.ParentID, Body, Subscribe, SaveAnonymousUser, originalInput.Creator != null ? new UserAnonymous(CreatorName, CreatorEmail, originalInput.Creator.Email != CreatorEmail ? CreatorEmail.ComputeHash() : originalInput.Creator.EmailHash, CreatorUrl) : null);
        }
    }
}
