ALTER TABLE [dbo].[oxite_PostAreaRelationship] ADD
CONSTRAINT [FK_oxite_PostAreaRelationship_oxite_Area] FOREIGN KEY ([AreaID]) REFERENCES [dbo].[oxite_Area] ([AreaID])


