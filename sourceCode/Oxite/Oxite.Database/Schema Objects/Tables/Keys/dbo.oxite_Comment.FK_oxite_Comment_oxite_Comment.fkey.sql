ALTER TABLE [dbo].[oxite_Comment] ADD
CONSTRAINT [FK_oxite_Comment_oxite_Comment] FOREIGN KEY ([ParentCommentID]) REFERENCES [dbo].[oxite_Comment] ([CommentID])


