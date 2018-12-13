//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IBlogsCommentRepository
    {
        PostCommentShell GetComment(string blogName, string postSlug, string commentSlug);
        IPageOfItems<PostCommentShell> GetComments(PagingInfo pagingInfo, bool includePending, bool sortDescending);
        IPageOfItems<PostCommentShell> GetComments(PagingInfo pagingInfo, Blog blog);
        IPageOfItems<PostCommentShell> GetComments(PagingInfo pagingInfo, Post post, bool includeUnapproved);
        IPageOfItems<PostCommentShell> GetComments(PagingInfo pagingInfo, Tag tag);
        PostComment Save(PostComment comment, string blogName, string postSlug);
    }
}
