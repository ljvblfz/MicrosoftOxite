ALTER TABLE [dbo].[oxite_Tag] ADD
CONSTRAINT [FK_oxite_Tag_oxite_Tag] FOREIGN KEY ([ParentTagID]) REFERENCES [dbo].[oxite_Tag] ([TagID])


