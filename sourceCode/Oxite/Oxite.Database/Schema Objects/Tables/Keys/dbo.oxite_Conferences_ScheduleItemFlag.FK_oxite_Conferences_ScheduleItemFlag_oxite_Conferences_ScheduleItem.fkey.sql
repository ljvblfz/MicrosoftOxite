ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemFlag] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemFlag_oxite_Conferences_ScheduleItem] FOREIGN KEY ([ScheduleItemID]) REFERENCES [dbo].[oxite_Conferences_ScheduleItem] ([ScheduleItemID])


