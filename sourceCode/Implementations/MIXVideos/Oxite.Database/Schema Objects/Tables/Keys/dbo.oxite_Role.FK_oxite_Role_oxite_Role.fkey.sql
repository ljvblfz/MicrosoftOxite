ALTER TABLE [dbo].[oxite_Role] ADD
CONSTRAINT [FK_oxite_Role_oxite_Role] FOREIGN KEY ([ParentRoleID]) REFERENCES [dbo].[oxite_Role] ([RoleID])


