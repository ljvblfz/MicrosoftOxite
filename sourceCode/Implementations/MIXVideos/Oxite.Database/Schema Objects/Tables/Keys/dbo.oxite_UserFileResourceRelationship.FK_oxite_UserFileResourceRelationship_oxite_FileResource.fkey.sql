ALTER TABLE [dbo].[oxite_UserFileResourceRelationship] ADD
CONSTRAINT [FK_oxite_UserFileResourceRelationship_oxite_FileResource] FOREIGN KEY ([FileResourceID]) REFERENCES [dbo].[oxite_FileResource] ([FileResourceID])


