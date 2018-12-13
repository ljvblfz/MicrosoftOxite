ALTER TABLE [dbo].[oxite_CMS_ContentItem] ADD
CONSTRAINT [FK_oxite_CMS_ContentItem_oxite_CMS_Page] FOREIGN KEY ([PageID]) REFERENCES [dbo].[oxite_CMS_Page] ([PageID])


