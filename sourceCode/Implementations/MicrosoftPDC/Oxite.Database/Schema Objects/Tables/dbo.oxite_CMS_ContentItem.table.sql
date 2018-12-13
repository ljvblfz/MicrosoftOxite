CREATE TABLE [dbo].[oxite_CMS_ContentItem]
(
[SiteID] [uniqueidentifier] NOT NULL,
[PageID] [uniqueidentifier] NULL,
[ContentItemID] [uniqueidentifier] NOT NULL,
[ContentItemName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ContentItemDisplayName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Body] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Version] [smallint] NOT NULL,
[CreatorUserID] [uniqueidentifier] NOT NULL,
[CreatedDate] [datetime] NOT NULL,
[PublishedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


