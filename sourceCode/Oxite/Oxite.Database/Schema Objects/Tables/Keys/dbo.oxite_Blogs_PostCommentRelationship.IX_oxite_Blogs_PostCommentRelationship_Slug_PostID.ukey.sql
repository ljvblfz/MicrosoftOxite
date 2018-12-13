ALTER TABLE [dbo].[oxite_Blogs_PostCommentRelationship] ADD CONSTRAINT [IX_oxite_Blogs_PostCommentRelationship_Slug_PostID] UNIQUE NONCLUSTERED  ([Slug], [PostID]) ON [PRIMARY]


