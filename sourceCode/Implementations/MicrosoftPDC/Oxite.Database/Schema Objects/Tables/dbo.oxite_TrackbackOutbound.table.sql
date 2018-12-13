CREATE TABLE [dbo].[oxite_TrackbackOutbound]
(
[TrackbackOutboundID] [uniqueidentifier] NOT NULL,
[TargetUrl] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PostID] [uniqueidentifier] NOT NULL,
[PostTitle] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PostBlogTitle] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PostBody] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PostUrl] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsSending] [bit] NOT NULL,
[RemainingRetryCount] [tinyint] NOT NULL,
[SentDate] [smalldatetime] NULL,
[LastAttemptDate] [smalldatetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


