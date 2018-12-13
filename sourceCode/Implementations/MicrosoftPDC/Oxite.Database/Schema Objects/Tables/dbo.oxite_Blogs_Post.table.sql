CREATE TABLE [dbo].[oxite_Blogs_Post]
(
[BlogID] [uniqueidentifier] NOT NULL,
[PostID] [uniqueidentifier] NOT NULL,
[CreatorUserID] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Body] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[BodyShort] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[State] [tinyint] NOT NULL,
[Slug] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CommentingDisabled] [bit] NOT NULL,
[CreatedDate] [datetime] NOT NULL,
[ModifiedDate] [datetime] NOT NULL,
[PublishedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


