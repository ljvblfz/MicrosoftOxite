ALTER TABLE [dbo].[oxite_Blogs_PostSubscriptionRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostSubscriptionRelationship_oxite_Subscription] FOREIGN KEY ([SubscriptionID]) REFERENCES [dbo].[oxite_Subscription] ([SubscriptionID])


