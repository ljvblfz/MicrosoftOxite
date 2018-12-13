CREATE TABLE [dbo].[oxite_Comment]
(
[ParentCommentID] [uniqueidentifier] NOT NULL,
[CommentID] [uniqueidentifier] NOT NULL,
[CreatorUserID] [uniqueidentifier] NOT NULL,
[CreatorName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatorEmail] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatorHashedEmail] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatorUrl] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LanguageID] [uniqueidentifier] NOT NULL,
[CreatorIP] [bigint] NOT NULL,
[UserAgent] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Body] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[State] [tinyint] NOT NULL,
[CreatedDate] [datetime] NOT NULL,
[ModifiedDate] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


