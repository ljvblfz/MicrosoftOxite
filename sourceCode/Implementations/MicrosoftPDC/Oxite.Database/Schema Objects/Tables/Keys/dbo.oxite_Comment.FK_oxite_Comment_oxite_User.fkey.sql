ALTER TABLE [dbo].[oxite_Comment] ADD
CONSTRAINT [FK_oxite_Comment_oxite_User] FOREIGN KEY ([CreatorUserID]) REFERENCES [dbo].[oxite_User] ([UserID])


