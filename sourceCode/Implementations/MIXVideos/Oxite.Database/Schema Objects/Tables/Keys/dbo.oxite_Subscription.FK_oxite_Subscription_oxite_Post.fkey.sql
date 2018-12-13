ALTER TABLE [dbo].[oxite_Subscription] ADD
CONSTRAINT [FK_oxite_Subscription_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


