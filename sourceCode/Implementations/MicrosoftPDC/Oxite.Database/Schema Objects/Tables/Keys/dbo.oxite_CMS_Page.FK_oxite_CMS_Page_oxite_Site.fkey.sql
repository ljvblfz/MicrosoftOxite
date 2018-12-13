ALTER TABLE [dbo].[oxite_CMS_Page] ADD
CONSTRAINT [FK_oxite_CMS_Page_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


