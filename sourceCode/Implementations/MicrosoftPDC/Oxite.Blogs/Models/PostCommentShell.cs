//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Blogs.Models
{
    public class PostCommentShell
    {
        public PostCommentShell(PostSmall post, Guid commentID, string commentSlug)
        {
            Post = post;
            CommentID = commentID;
            CommentSlug = commentSlug;
        }

        public PostSmall Post { get; private set; }
        public Guid CommentID { get; private set; }
        public string CommentSlug { get; private set; }
    }
}
