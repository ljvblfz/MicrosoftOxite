CREATE TABLE [dbo].[oxite_Tag]
(
[ParentTagID] [uniqueidentifier] NOT NULL,
[TagID] [uniqueidentifier] NOT NULL,
[TagName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
[CreatedDate] [datetime] NOT NULL
) ON [PRIMARY]


