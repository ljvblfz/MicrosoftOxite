ALTER TABLE [dbo].[oxite_PluginSetting] ADD
CONSTRAINT [FK_oxite_PluginSetting_oxite_Plugin] FOREIGN KEY ([SiteID], [PluginID]) REFERENCES [dbo].[oxite_Plugin] ([SiteID], [PluginID])


