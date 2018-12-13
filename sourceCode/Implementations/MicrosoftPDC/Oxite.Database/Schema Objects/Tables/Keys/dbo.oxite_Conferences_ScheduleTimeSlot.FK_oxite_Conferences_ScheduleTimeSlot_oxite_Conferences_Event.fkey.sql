ALTER TABLE [dbo].[oxite_Conferences_ScheduleTimeSlot] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleTimeSlot_oxite_Conferences_Event] FOREIGN KEY ([EventID]) REFERENCES [dbo].[oxite_Conferences_Event] ([EventID])


