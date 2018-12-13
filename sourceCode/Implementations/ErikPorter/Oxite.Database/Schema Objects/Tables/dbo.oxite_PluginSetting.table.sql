CREATE TABLE [dbo].[oxite_PluginSetting]
(
[SiteID] [uniqueidentifier] NOT NULL,
[PluginID] [uniqueidentifier] NOT NULL,
[PluginSettingName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PluginSettingValue] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


