CREATE TABLE [dbo].[oxite_MessageOutbound]
(
[MessageOutboundID] [uniqueidentifier] NOT NULL,
[MessageTo] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MessageSubject] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MessageBody] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IsSending] [bit] NOT NULL,
[RemainingRetryCount] [tinyint] NOT NULL,
[SentDate] [smalldatetime] NULL,
[LastAttemptDate] [smalldatetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


