ALTER TABLE [dbo].[oxite_Module] ADD
CONSTRAINT [FK_oxite_Module_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


