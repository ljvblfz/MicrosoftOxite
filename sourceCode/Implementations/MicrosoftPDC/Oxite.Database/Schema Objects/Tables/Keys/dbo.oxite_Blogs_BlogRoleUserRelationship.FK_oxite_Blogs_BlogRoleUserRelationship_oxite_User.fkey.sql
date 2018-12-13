ALTER TABLE [dbo].[oxite_Blogs_BlogRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_BlogRoleUserRelationship_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


