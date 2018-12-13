ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemSpeakerRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemSpeakerRelationship_oxite_Conferences_Speaker] FOREIGN KEY ([SpeakerID]) REFERENCES [dbo].[oxite_Conferences_Speaker] ([SpeakerID])


