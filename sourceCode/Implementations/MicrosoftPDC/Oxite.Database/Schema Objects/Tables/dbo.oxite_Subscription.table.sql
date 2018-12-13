CREATE TABLE [dbo].[oxite_Subscription]
(
[SubscriptionID] [uniqueidentifier] NOT NULL,
[UserID] [uniqueidentifier] NOT NULL,
[UserName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UserEmail] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]


