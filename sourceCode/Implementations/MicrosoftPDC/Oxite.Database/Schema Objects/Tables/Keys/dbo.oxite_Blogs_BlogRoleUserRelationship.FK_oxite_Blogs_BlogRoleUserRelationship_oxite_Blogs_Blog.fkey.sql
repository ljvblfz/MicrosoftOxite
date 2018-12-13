ALTER TABLE [dbo].[oxite_Blogs_BlogRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_BlogRoleUserRelationship_oxite_Blogs_Blog] FOREIGN KEY ([BlogID]) REFERENCES [dbo].[oxite_Blogs_Blog] ([BlogID])


