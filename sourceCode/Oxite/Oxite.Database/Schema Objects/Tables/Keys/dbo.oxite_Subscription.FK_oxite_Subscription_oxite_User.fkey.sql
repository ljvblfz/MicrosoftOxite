ALTER TABLE [dbo].[oxite_Subscription] ADD
CONSTRAINT [FK_oxite_Subscription_oxite_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[oxite_User] ([UserID])


