ALTER TABLE [dbo].[oxite_Comment] ADD
CONSTRAINT [FK_oxite_Comment_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


