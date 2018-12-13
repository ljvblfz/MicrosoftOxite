ALTER TABLE [dbo].[oxite_SubscriptionAnonymous] ADD
CONSTRAINT [FK_oxite_SubscriptionAnonymous_oxite_Subscription] FOREIGN KEY ([SubscriptionID]) REFERENCES [dbo].[oxite_Subscription] ([SubscriptionID])


