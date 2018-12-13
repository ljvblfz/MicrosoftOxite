//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Modules.Blogs.Models;
using Oxite.Plugins.Models;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class CommentOutExtensions
    {
        public static PostComment ToPostComment(this CommentOut comment, PostCommentSmall parent, PostSmall post)
        {
            return new PostComment(comment.Body, comment.Created, comment.CreatorUserID, comment.CreatorName, comment.CreatorEmail, comment.CreatorEmailHash, comment.CreatorUrl, comment.CreatorIP, comment.CreatorUserAgent, comment.Language, comment.Modified, parent, post, comment.Url, comment.State);
        }
    }
}
