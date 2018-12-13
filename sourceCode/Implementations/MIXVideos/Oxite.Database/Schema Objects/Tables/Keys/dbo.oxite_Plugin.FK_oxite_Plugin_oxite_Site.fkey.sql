ALTER TABLE [dbo].[oxite_Plugin] ADD
CONSTRAINT [FK_oxite_Plugin_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


