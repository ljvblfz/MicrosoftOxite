ALTER TABLE [dbo].[oxite_PostRelationship] ADD
CONSTRAINT [FK_oxite_PostRelationship_oxite_Post1] FOREIGN KEY ([ParentPostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


