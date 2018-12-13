ALTER TABLE [dbo].[oxite_FileResource] ADD
CONSTRAINT [FK_oxite_FileResource_oxite_User] FOREIGN KEY ([CreatorUserID]) REFERENCES [dbo].[oxite_User] ([UserID])


