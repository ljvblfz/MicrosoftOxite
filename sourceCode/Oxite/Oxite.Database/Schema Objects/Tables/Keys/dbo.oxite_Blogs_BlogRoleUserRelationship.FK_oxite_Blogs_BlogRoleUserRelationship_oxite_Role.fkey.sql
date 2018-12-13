ALTER TABLE [dbo].[oxite_Blogs_BlogRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_BlogRoleUserRelationship_oxite_Role] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[oxite_Role] ([RoleID])


