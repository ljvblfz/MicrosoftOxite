ALTER TABLE [dbo].[oxite_PostTagRelationship] ADD
CONSTRAINT [FK_oxite_PostTagRelationship_oxite_Tag] FOREIGN KEY ([TagID]) REFERENCES [dbo].[oxite_Tag] ([TagID])


