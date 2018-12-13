ALTER TABLE [dbo].[oxite_Blogs_PostTagRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostTagRelationship_oxite_Blogs_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Blogs_Post] ([PostID])


