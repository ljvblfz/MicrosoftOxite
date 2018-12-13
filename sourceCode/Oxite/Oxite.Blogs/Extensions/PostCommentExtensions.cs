//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class PostCommentExtensions
    {
        public static PostComment Apply(this PostComment comment, CommentInput input, User creator)
        {
            if (creator != null)
                return new PostComment(input.Body, comment.Created, creator, comment.CreatorIP, comment.CreatorUserAgent, comment.ID, comment.Language, comment.Modified, new PostCommentSmall(input.ParentID), comment.Post, comment.Slug, comment.State);
            else
                return new PostComment(input.Body, comment.Created, input.Creator, comment.CreatorIP, comment.CreatorUserAgent, comment.ID, comment.Language, comment.Modified, new PostCommentSmall(input.ParentID), comment.Post, comment.Slug, comment.State);
        }
    }
}
