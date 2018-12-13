ALTER TABLE [dbo].[oxite_StringResourceVersion] ADD
CONSTRAINT [FK_oxite_StringResourceVersion_oxite_StringResource] FOREIGN KEY ([StringResourceKey]) REFERENCES [dbo].[oxite_StringResource] ([StringResourceKey])


