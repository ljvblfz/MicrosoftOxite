ALTER TABLE [dbo].[oxite_PostTagRelationship] ADD
CONSTRAINT [FK_oxite_PostTagRelationship_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


