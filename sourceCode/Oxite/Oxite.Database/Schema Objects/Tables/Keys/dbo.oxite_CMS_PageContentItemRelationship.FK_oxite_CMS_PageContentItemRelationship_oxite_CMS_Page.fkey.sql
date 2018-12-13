ALTER TABLE [dbo].[oxite_CMS_PageContentItemRelationship] ADD
CONSTRAINT [FK_oxite_CMS_PageContentItemRelationship_oxite_CMS_Page] FOREIGN KEY ([PageID]) REFERENCES [dbo].[oxite_CMS_Page] ([PageID])


