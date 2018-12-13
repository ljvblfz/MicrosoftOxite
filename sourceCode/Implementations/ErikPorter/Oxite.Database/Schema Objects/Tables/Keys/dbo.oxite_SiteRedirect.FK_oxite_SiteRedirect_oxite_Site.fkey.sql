ALTER TABLE [dbo].[oxite_SiteRedirect] ADD
CONSTRAINT [FK_oxite_SiteRedirect_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


