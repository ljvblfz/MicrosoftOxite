//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Tags.Models;
using Oxite.Validation;

namespace Oxite.Modules.Blogs.Services
{
    public interface IBlogsCommentService
    {
        IPageOfItems<PostComment> GetComments(int pageIndex, int pageSize, bool includePending, bool sortDescending);
        IPageOfItems<PostComment> GetComments(int pageIndex, int pageSize, Blog blog);
        IPageOfItems<PostComment> GetComments(int pageIndex, int pageSize, Post post, bool includeUnapproved);
        IPageOfItems<PostComment> GetComments(int pageIndex, int pageSize, Tag tag);
        ValidationStateDictionary ValidateCommentInput(CommentInput commentInput);
        ModelResult<PostComment> AddComment(PostAddress postAddress, CommentInput commentInput);
        ModelResult<PostComment> AddComment(Post post, CommentInputForImport commentInput);
        ModelResult<PostComment> EditComment(PostCommentAddress commentAddress, CommentInput commentInput);
        bool RemoveComment(PostCommentAddress commentAddress);
        bool ApproveComment(PostCommentAddress commentAddress);
    }
}
