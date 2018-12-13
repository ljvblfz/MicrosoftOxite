ALTER TABLE [dbo].[oxite_UserLanguage] ADD
CONSTRAINT [FK_oxite_UserLanguage_oxite_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[oxite_Language] ([LanguageID])


