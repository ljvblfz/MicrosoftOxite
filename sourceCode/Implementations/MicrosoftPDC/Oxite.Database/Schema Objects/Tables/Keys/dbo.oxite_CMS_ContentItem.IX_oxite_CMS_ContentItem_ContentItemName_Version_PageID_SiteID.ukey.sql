ALTER TABLE [dbo].[oxite_CMS_ContentItem] ADD CONSTRAINT [IX_oxite_CMS_ContentItem_ContentItemName_Version_PageID_SiteID] UNIQUE NONCLUSTERED  ([ContentItemName], [Version] DESC, [PageID], [SiteID]) ON [PRIMARY]


