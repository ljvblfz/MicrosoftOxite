CREATE TABLE [dbo].[oxite_Blogs_Blog]
(
[SiteID] [uniqueidentifier] NOT NULL,
[BlogID] [uniqueidentifier] NOT NULL,
[BlogName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DisplayName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CommentingDisabled] [bit] NOT NULL,
[CreatedDate] [datetime] NOT NULL,
[ModifiedDate] [datetime] NOT NULL
) ON [PRIMARY]


