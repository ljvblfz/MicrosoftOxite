ALTER TABLE [dbo].[oxite_CMS_PageRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_CMS_PageRoleUserRelationship_oxite_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[oxite_Role] ([RoleID])


