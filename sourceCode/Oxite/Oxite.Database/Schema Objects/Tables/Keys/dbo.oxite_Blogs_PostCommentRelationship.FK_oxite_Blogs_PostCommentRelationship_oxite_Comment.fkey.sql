ALTER TABLE [dbo].[oxite_Blogs_PostCommentRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostCommentRelationship_oxite_Comment] FOREIGN KEY ([CommentID]) REFERENCES [dbo].[oxite_Comment] ([CommentID])


