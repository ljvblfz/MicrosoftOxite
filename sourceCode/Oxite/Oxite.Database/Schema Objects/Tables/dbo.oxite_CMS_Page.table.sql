CREATE TABLE [dbo].[oxite_CMS_Page]
(
[SiteID] [uniqueidentifier] NOT NULL,
[PageID] [uniqueidentifier] NOT NULL,
[TemplateName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Title] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Slug] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PublishedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


