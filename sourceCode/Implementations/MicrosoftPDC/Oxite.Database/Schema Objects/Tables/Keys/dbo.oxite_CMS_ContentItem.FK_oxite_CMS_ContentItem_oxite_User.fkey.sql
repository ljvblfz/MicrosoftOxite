ALTER TABLE [dbo].[oxite_CMS_ContentItem] ADD
CONSTRAINT [FK_oxite_CMS_ContentItem_oxite_User] FOREIGN KEY ([CreatorUserID]) REFERENCES [dbo].[oxite_User] ([UserID])


