CREATE TABLE [dbo].[oxite_Conferences_Event]
(
[EventID] [uniqueidentifier] NOT NULL,
[EventName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[EventDisplayName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Year] [smallint] NOT NULL
) ON [PRIMARY]


