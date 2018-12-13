ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemSubscriptionRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemSubscriptionRelationship_oxite_Conferences_ScheduleItem] FOREIGN KEY ([ScheduleItemID]) REFERENCES [dbo].[oxite_Conferences_ScheduleItem] ([ScheduleItemID])


