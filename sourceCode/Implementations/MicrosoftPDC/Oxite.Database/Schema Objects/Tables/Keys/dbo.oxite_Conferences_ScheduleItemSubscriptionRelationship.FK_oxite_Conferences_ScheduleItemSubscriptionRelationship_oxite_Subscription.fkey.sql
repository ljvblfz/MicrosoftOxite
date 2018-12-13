ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemSubscriptionRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemSubscriptionRelationship_oxite_Subscription] FOREIGN KEY ([SubscriptionID]) REFERENCES [dbo].[oxite_Subscription] ([SubscriptionID])


