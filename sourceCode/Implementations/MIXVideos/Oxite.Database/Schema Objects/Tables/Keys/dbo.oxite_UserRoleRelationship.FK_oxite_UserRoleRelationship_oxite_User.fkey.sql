ALTER TABLE [dbo].[oxite_UserRoleRelationship] ADD
CONSTRAINT [FK_oxite_UserRoleRelationship_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


