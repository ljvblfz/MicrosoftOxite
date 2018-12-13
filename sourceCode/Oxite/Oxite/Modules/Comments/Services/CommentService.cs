//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Comments.Infrastructure;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Comments.Repositories;

namespace Oxite.Modules.Comments.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;

        public CommentService(ICommentRepository repository)
        {
            this.repository = repository;
        }

        #region ICommentService Members

        public void FillComments(ICommentedEntity entity)
        {
            foreach (Comment comment in entity.GetComments())
            {
                if (comment.ID != Guid.Empty)
                {
                    Comment foundComment = repository.GetComment(comment.ID);

                    if (foundComment != null)
                        comment.Fill(foundComment);
                }
            }
        }

        #endregion
    }
}
