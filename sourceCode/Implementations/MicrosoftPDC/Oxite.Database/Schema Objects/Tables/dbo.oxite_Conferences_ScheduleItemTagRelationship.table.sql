CREATE TABLE [dbo].[oxite_Conferences_ScheduleItemTagRelationship]
(
[ScheduleItemID] [uniqueidentifier] NOT NULL,
[TagID] [uniqueidentifier] NOT NULL,
[TagDisplayName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


