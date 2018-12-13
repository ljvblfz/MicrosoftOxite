ALTER TABLE [dbo].[oxite_PluginSequence] ADD
CONSTRAINT [FK_oxite_PluginSequence_oxite_Plugin] FOREIGN KEY ([PluginID]) REFERENCES [dbo].[oxite_Plugin] ([PluginID])


