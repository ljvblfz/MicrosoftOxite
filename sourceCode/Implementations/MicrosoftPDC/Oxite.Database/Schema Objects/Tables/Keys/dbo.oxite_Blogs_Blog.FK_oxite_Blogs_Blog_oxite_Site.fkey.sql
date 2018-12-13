ALTER TABLE [dbo].[oxite_Blogs_Blog] ADD
CONSTRAINT [FK_oxite_Blogs_Blog_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


