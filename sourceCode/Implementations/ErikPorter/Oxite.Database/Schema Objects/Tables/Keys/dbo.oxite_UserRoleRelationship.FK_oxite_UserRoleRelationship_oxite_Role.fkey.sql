ALTER TABLE [dbo].[oxite_UserRoleRelationship] ADD
CONSTRAINT [FK_oxite_UserRoleRelationship_oxite_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[oxite_Role] ([RoleID])


