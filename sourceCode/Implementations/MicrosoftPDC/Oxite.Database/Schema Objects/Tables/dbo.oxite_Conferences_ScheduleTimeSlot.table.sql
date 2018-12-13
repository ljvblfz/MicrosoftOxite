CREATE TABLE [dbo].[oxite_Conferences_ScheduleTimeSlot]
(
[EventID] [uniqueidentifier] NOT NULL,
[ScheduleTimeSlotID] [uniqueidentifier] NOT NULL,
[StartTime] [datetime] NOT NULL,
[EndTime] [datetime] NOT NULL,
[Description] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]


