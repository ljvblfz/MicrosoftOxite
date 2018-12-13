//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Tests.Fakes
{
    public class FakeCommentService : ICommentService
    {
        public List<Comment> AllComments { get; set; }

        public int LastRequestedPageSize { get; set; }
        public DateTime LastRequestedSinceDate { get; set; }

        public FakeCommentService()
        {
            AllComments = new List<Comment>();
        }

        public Comment GetComment(Guid commentID)
        {
            throw new NotImplementedException();
        }

        public IList<Comment> GetComments()
        {
            return AllComments;
        }

        public IList<Comment> GetComments(Area area)
        {
            return AllComments;
        }

        public IPageOfItems<Comment> GetComments(int pageIndex, int pageSize, Post post, bool includeUnapproved)
        {
            return AllComments.AsQueryable().GetPage(pageIndex, pageSize);
        }

        public IList<Comment> GetComments(Tag tag)
        {
            return AllComments;
        }

        #region Unimplemented ICommentService Members

        public void ValidateComment(Comment comment, out ValidationStateDictionary validationState)
        {
            throw new NotImplementedException();
        }

        public void AddComment(Area area, Post post, CommentInput commentInput, UserBase creator, bool subscribe, out ValidationStateDictionary validationState, out Comment newComment)
        {
            throw new NotImplementedException();
        }

        public void AddCommentWithoutMessages(Area area, Post post, Comment comment, UserBase creator, bool subscribe, out ValidationStateDictionary validationState, out Comment newComment)
        {
            throw new NotImplementedException();
        }

        public void EditComment(Area area, Post post, Comment comment, out ValidationStateDictionary validationState)
        {
            throw new NotImplementedException();
        }

        public void RemoveComment(Post postInput, Guid commentID)
        {
            throw new NotImplementedException();
        }

        public void ApproveComment(Post postInput, Guid commentID)
        {
            throw new NotImplementedException();
        }

        public IPageOfItems<Comment> GetComments(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IPageOfItems<Comment> GetComments(int pageIndex, int pageSize, bool includePending, bool sortDescending)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
