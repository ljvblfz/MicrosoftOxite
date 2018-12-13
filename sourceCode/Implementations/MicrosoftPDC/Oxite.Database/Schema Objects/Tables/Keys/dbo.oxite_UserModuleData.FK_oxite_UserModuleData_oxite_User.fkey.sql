ALTER TABLE [dbo].[oxite_UserModuleData] ADD
CONSTRAINT [FK_oxite_UserModuleData_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


