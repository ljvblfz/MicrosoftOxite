ALTER TABLE [dbo].[oxite_Blogs_PostCommentRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostCommentRelationship_oxite_Blogs_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Blogs_Post] ([PostID])


