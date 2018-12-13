ALTER TABLE [dbo].[oxite_CMS_Page] ADD CONSTRAINT [CK_oxite_CMS_Page] CHECK (([TemplateName] IS NULL AND [Title] IS NOT NULL AND [Description] IS NOT NULL OR [TemplateName] IS NOT NULL AND [Title] IS NULL AND [Description] IS NULL))


