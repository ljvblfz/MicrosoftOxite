ALTER TABLE [dbo].[oxite_AreaRoleRelationship] ADD
CONSTRAINT [FK_oxite_AreaRoleRelationship_oxite_Area] FOREIGN KEY ([AreaID]) REFERENCES [dbo].[oxite_Area] ([AreaID])


