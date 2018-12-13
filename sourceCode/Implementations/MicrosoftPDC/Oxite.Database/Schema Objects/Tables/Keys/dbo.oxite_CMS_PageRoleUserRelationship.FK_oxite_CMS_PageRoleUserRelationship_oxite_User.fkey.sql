ALTER TABLE [dbo].[oxite_CMS_PageRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_CMS_PageRoleUserRelationship_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


