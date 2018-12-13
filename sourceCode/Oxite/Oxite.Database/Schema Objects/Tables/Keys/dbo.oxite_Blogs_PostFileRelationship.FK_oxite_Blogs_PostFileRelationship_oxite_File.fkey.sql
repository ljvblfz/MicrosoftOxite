ALTER TABLE [dbo].[oxite_Blogs_PostFileRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostFileRelationship_oxite_File] FOREIGN KEY ([FileID]) REFERENCES [dbo].[oxite_File] ([FileID])


