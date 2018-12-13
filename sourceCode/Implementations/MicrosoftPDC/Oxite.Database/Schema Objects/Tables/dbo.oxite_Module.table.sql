CREATE TABLE [dbo].[oxite_Module]
(
[SiteID] [uniqueidentifier] NOT NULL,
[ModuleName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ModuleOrder] [tinyint] NOT NULL,
[ModuleType] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Enabled] [bit] NOT NULL,
[IsSystem] [bit] NOT NULL
) ON [PRIMARY]


