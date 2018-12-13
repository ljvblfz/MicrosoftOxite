ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemUserRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemUserRelationship_oxite_Conferences_ScheduleItem] FOREIGN KEY ([ScheduleItemID]) REFERENCES [dbo].[oxite_Conferences_ScheduleItem] ([ScheduleItemID])


