ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemCommentRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemCommentRelationship_oxite_Conferences_ScheduleItem] FOREIGN KEY ([ScheduleItemID]) REFERENCES [dbo].[oxite_Conferences_ScheduleItem] ([ScheduleItemID])


