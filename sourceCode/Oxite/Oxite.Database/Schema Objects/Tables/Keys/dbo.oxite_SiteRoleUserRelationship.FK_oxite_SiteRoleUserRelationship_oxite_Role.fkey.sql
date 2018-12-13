ALTER TABLE [dbo].[oxite_SiteRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_SiteRoleUserRelationship_oxite_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[oxite_Role] ([RoleID])


