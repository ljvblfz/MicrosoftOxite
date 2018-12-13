ALTER TABLE [dbo].[oxite_Blogs_PostView] ADD
CONSTRAINT [FK_oxite_Blogs_PostView_oxite_Blogs_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Blogs_Post] ([PostID])


