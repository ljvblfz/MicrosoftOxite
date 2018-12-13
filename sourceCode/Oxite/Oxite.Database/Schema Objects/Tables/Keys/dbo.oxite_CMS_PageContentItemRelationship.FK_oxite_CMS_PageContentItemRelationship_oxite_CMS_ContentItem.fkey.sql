ALTER TABLE [dbo].[oxite_CMS_PageContentItemRelationship] ADD
CONSTRAINT [FK_oxite_CMS_PageContentItemRelationship_oxite_CMS_ContentItem] FOREIGN KEY ([ContentItemID]) REFERENCES [dbo].[oxite_CMS_ContentItem] ([ContentItemID])


