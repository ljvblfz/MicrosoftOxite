ALTER TABLE [dbo].[oxite_User] ADD
CONSTRAINT [FK_oxite_User_oxite_Language] FOREIGN KEY ([DefaultLanguageID]) REFERENCES [dbo].[oxite_Language] ([LanguageID])


