ALTER TABLE [dbo].[pdc09_UserRegistration] ADD
CONSTRAINT [FK_pdc09_UserRegistration_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


