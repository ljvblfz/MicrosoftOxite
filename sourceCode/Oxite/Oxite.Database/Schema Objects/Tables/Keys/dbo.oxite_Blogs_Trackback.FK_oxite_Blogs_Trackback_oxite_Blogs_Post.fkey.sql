ALTER TABLE [dbo].[oxite_Blogs_Trackback] ADD
CONSTRAINT [FK_oxite_Blogs_Trackback_oxite_Blogs_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Blogs_Post] ([PostID])


