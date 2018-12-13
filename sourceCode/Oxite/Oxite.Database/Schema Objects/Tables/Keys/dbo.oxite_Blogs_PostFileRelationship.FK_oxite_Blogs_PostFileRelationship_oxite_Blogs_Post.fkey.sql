ALTER TABLE [dbo].[oxite_Blogs_PostFileRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostFileRelationship_oxite_Blogs_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Blogs_Post] ([PostID])


