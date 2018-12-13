ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemCommentRelationship] ADD CONSTRAINT [IX_oxite_Conferences_ScheduleItemCommentRelationship_Slug_ScheduleItemID] UNIQUE NONCLUSTERED  ([Slug], [ScheduleItemID]) ON [PRIMARY]


