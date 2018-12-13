ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemCommentRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemCommentRelationship_oxite_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[oxite_Comment] ([CommentID])


