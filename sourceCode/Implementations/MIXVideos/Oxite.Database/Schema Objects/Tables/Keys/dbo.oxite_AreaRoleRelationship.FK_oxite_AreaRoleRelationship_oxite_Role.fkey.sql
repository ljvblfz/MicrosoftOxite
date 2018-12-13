ALTER TABLE [dbo].[oxite_AreaRoleRelationship] ADD
CONSTRAINT [FK_oxite_AreaRoleRelationship_oxite_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[oxite_Role] ([RoleID])


