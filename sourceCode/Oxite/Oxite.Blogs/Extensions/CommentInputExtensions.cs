//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class CommentInputExtensions
    {
        public static PostComment ToComment(this CommentInput commentInput, User creatorAuthenticated, long creatorIP, string creatorUserAgent, Language language, string slug, EntityState state)
        {
            if (creatorAuthenticated != null)
                return new PostComment(commentInput.Body, creatorAuthenticated.ID, null, null, null, null, creatorIP, creatorUserAgent, language, new PostCommentSmall(commentInput.ParentID), null, slug, state);
            else
                return new PostComment(commentInput.Body, Guid.Empty, commentInput.Creator.Name, commentInput.Creator.Email, commentInput.Creator.EmailHash, commentInput.Creator.Url, creatorIP, creatorUserAgent, language, new PostCommentSmall(commentInput.ParentID), null, slug, state);
        }
    }
}
