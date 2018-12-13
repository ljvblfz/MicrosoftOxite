ALTER TABLE [dbo].[oxite_Blogs_PostRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostRoleUserRelationship_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


