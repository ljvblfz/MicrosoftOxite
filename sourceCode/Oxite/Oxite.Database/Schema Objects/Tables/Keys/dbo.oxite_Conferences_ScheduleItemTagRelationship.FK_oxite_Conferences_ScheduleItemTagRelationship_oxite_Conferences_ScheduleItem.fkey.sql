ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemTagRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemTagRelationship_oxite_Conferences_ScheduleItem] FOREIGN KEY ([ScheduleItemID]) REFERENCES [dbo].[oxite_Conferences_ScheduleItem] ([ScheduleItemID])


