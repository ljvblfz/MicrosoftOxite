ALTER TABLE [dbo].[oxite_PluginSetting] ADD
CONSTRAINT [FK_oxite_PluginSetting_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


