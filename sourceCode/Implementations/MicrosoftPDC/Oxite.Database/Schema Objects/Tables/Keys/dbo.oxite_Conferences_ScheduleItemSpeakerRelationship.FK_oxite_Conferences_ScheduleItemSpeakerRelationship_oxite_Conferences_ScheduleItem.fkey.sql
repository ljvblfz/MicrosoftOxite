ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemSpeakerRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemSpeakerRelationship_oxite_Conferences_ScheduleItem] FOREIGN KEY ([ScheduleItemID]) REFERENCES [dbo].[oxite_Conferences_ScheduleItem] ([ScheduleItemID])


