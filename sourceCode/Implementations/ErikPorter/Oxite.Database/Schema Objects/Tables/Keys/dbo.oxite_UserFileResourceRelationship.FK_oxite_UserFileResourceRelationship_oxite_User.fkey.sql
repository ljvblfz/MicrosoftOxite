ALTER TABLE [dbo].[oxite_UserFileResourceRelationship] ADD
CONSTRAINT [FK_oxite_UserFileResourceRelationship_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


