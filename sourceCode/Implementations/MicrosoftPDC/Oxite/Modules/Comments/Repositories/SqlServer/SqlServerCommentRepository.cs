//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Comments.Repositories.SqlServer
{
    public class SqlServerCommentRepository : ICommentRepository
    {
        private readonly OxiteCommentsDataContext context;

        public SqlServerCommentRepository(OxiteCommentsDataContext context)
        {
            this.context = context;
        }

        #region ICommentRepository Members

        public Comment GetComment(Guid commentID)
        {
            return projectComments(context.oxite_Comments.Where(c => c.CommentID == commentID)).FirstOrDefault();
        }

        public void ChangeState(Guid commentID, EntityState state)
        {
            oxite_Comment comment = context.oxite_Comments.FirstOrDefault(c => c.CommentID == commentID);

            if (comment != null)
                comment.State = (byte)state;

            context.SubmitChanges();
        }

        #endregion

        #region Private Methods

        private IQueryable<Comment> projectComments(IQueryable<oxite_Comment> comments)
        {
            return
                from c in comments
                join u in context.oxite_Users on c.CreatorUserID equals u.UserID
                select getComment(c, u);
        }

        private Comment getComment(oxite_Comment comment, oxite_User user)
        {
            CommentSmall parent = comment.ParentCommentID != comment.CommentID ? getParentComment(comment.ParentCommentID) : null;
            Language language = new Language(comment.oxite_Language.LanguageID)
            {
                DisplayName = comment.oxite_Language.LanguageDisplayName,
                Name = comment.oxite_Language.LanguageName
            };

            if (user.Username != "Anonymous")
                return new Comment(comment.Body, comment.CreatedDate, getUserAuthenticated(comment, user), comment.CreatorIP, comment.UserAgent, comment.CommentID, language, comment.ModifiedDate, parent, (EntityState)comment.State);
            else
                return new Comment(comment.Body, comment.CreatedDate, getUserAnonymous(comment, user), comment.CreatorIP, comment.UserAgent, comment.CommentID, language, comment.ModifiedDate, parent, (EntityState)comment.State);
        }

        private CommentSmall getParentComment(Guid commentID)
        {
            return (
                from c in context.oxite_Comments
                join u in context.oxite_Users on c.CreatorUserID equals u.UserID
                where c.State != (byte)EntityState.Removed && c.CommentID == commentID
                select projectCommentSmall(c, u)
                ).FirstOrDefault();
        }

        private static UserAuthenticated getUserAuthenticated(oxite_Comment comment, oxite_User user)
        {
            return new UserAuthenticated(user.UserID, user.Username, user.DisplayName, user.Email, user.HashedEmail, (EntityState)user.Status);
        }

        private static UserAnonymous getUserAnonymous(oxite_Comment comment, oxite_User user)
        {
            return new UserAnonymous(comment.CreatorName, comment.CreatorEmail, comment.CreatorHashedEmail, comment.CreatorUrl);
        }

        private static CommentSmall projectCommentSmall(oxite_Comment comment, oxite_User user)
        {
            if (user.Username != "Anonymous")
                return new CommentSmall(comment.CommentID, comment.CreatedDate, getUserAuthenticated(comment, user));
            else
                return new CommentSmall(comment.CommentID, comment.CreatedDate, getUserAnonymous(comment, user));
        }

        #endregion
    }
}
