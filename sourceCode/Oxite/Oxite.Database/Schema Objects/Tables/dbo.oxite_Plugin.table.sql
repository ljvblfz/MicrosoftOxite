CREATE TABLE [dbo].[oxite_Plugin]
(
[SiteID] [uniqueidentifier] NOT NULL,
[PluginID] [uniqueidentifier] NOT NULL,
[VirtualPath] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Enabled] [bit] NOT NULL
) ON [PRIMARY]


