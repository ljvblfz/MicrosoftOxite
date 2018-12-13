ALTER TABLE [dbo].[oxite_SiteRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_SiteRoleUserRelationship_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


