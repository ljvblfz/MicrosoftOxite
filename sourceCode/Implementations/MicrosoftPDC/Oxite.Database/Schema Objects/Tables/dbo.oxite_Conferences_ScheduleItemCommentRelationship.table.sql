CREATE TABLE [dbo].[oxite_Conferences_ScheduleItemCommentRelationship]
(
[ScheduleItemID] [uniqueidentifier] NOT NULL,
[CommentID] [uniqueidentifier] NOT NULL,
[Slug] [char] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]


