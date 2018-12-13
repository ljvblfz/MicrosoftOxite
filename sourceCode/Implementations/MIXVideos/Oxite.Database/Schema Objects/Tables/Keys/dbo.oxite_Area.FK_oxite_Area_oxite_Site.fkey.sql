ALTER TABLE [dbo].[oxite_Area] ADD
CONSTRAINT [FK_oxite_Area_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


