ALTER TABLE [dbo].[oxite_UserLanguage] ADD
CONSTRAINT [FK_oxite_UserLanguage_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


