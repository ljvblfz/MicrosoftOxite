ALTER TABLE [dbo].[oxite_CommentAnonymous] ADD
CONSTRAINT [FK_oxite_CommentAnonymous_oxite_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[oxite_Comment] ([CommentID])


