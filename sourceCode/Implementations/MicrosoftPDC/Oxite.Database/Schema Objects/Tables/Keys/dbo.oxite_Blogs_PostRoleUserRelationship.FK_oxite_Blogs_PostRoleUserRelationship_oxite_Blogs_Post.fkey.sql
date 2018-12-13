ALTER TABLE [dbo].[oxite_Blogs_PostRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostRoleUserRelationship_oxite_Blogs_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Blogs_Post] ([PostID])


