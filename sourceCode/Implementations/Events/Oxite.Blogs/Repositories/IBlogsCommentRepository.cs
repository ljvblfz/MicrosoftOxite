//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IBlogsCommentRepository
    {
        PostCommentShell GetComment(Guid siteID, string blogName, string postSlug, string commentSlug);
        IQueryable<PostCommentShell> GetComments(Guid siteID, bool includePending, bool sortDescending);
        IQueryable<PostCommentShell> GetCommentsByBlog(Guid blogID);
        IQueryable<PostCommentShell> GetCommentsByPost(Guid postID, bool includeUnapproved);
        IQueryable<PostCommentShell> GetCommentsByTag(Guid siteID, Guid tagID);
        PostComment Save(PostComment comment, Guid siteID, string blogName, string postSlug);
    }
}
