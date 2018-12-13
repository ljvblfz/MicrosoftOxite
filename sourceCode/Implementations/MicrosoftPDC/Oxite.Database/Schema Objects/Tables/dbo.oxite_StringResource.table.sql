CREATE TABLE [dbo].[oxite_StringResource]
(
[StringResourceKey] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Language] [varchar] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Version] [smallint] NOT NULL,
[StringResourceValue] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatorUserID] [uniqueidentifier] NOT NULL,
[CreatedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


