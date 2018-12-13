ALTER TABLE [dbo].[oxite_Blogs_Post] ADD
CONSTRAINT [FK_oxite_Blogs_Post_oxite_Blogs_Blog] FOREIGN KEY ([BlogID]) REFERENCES [dbo].[oxite_Blogs_Blog] ([BlogID])


