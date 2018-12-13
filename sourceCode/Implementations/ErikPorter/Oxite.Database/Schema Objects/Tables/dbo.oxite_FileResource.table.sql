CREATE TABLE [dbo].[oxite_FileResource]
(
[SiteID] [uniqueidentifier] NOT NULL,
[FileResourceID] [uniqueidentifier] NOT NULL,
[FileResourceName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CreatorUserID] [uniqueidentifier] NOT NULL,
[Data] [varbinary] (max) NULL,
[ContentType] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Path] [nvarchar] (1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[State] [tinyint] NOT NULL,
[CreatedDate] [datetime] NOT NULL,
[ModifiedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


