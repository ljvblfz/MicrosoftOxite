//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Tags.Models;
using Oxite.Validation;

namespace Oxite.Modules.Blogs.Services
{
    public interface IBlogsCommentService
    {
        PostComment GetComment(string blogName, string postSlug, string commentSlug);
        IPageOfItems<PostComment> GetComments(PagingInfo pagingInfo, bool includePending, bool sortDescending);
        IPageOfItems<PostComment> GetComments(PagingInfo pagingInfo, Blog blog);
        IPageOfItems<PostComment> GetComments(PagingInfo pagingInfo, Post post, bool includeUnapproved);
        IPageOfItems<PostComment> GetComments(PagingInfo pagingInfo, Tag tag);
        ValidationStateDictionary ValidateCommentInput(CommentInput commentInput);
        ModelResult<PostComment> AddComment(Post post, CommentInput commentInput);
        ModelResult<PostComment> AddComment(Post post, CommentInputForImport commentInput);
        ModelResult<PostComment> EditComment(PostComment comment, CommentInput commentInput);
        bool RemoveComment(PostComment comment);
        bool ApproveComment(PostComment comment);
    }
}
