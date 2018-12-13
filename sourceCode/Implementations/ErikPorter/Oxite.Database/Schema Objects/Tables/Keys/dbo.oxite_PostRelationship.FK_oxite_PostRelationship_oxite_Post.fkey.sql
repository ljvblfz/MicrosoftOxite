ALTER TABLE [dbo].[oxite_PostRelationship] ADD
CONSTRAINT [FK_oxite_PostRelationship_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


