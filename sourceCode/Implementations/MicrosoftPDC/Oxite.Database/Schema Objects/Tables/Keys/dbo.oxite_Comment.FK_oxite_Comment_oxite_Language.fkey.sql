ALTER TABLE [dbo].[oxite_Comment] ADD
CONSTRAINT [FK_oxite_Comment_oxite_Language] FOREIGN KEY ([LanguageID]) REFERENCES [dbo].[oxite_Language] ([LanguageID])


