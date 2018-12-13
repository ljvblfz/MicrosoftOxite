ALTER TABLE [dbo].[oxite_PostAreaRelationship] ADD
CONSTRAINT [FK_oxite_PostAreaRelationship_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


