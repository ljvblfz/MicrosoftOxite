ALTER TABLE [dbo].[oxite_Conferences_ScheduleItem] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItem_oxite_Conferences_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[oxite_Conferences_Event] ([EventID])


