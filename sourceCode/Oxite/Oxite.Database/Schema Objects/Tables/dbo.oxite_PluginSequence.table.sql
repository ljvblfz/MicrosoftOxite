CREATE TABLE [dbo].[oxite_PluginSequence]
(
[PluginID] [uniqueidentifier] NOT NULL,
[OperationType] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[OperationName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SequenceNumber] [tinyint] NOT NULL
) ON [PRIMARY]


