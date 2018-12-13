ALTER TABLE [dbo].[oxite_ExtendedPropertyValue] ADD
CONSTRAINT [FK_oxite_ExtendedPropertyValue_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


