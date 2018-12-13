CREATE TABLE [dbo].[oxite_File]
(
[PostID] [uniqueidentifier] NOT NULL,
[FileID] [uniqueidentifier] NOT NULL,
[TypeName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MimeType] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Url] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Length] [bigint] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


