ALTER TABLE [dbo].[oxite_Post] ADD
CONSTRAINT [FK_oxite_Post_oxite_User] FOREIGN KEY ([CreatorUserID]) REFERENCES [dbo].[oxite_User] ([UserID])


