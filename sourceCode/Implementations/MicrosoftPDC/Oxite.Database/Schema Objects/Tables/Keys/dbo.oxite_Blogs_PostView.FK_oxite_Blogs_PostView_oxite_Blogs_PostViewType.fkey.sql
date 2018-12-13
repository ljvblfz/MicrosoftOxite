ALTER TABLE [dbo].[oxite_Blogs_PostView] ADD
CONSTRAINT [FK_oxite_Blogs_PostView_oxite_Blogs_PostViewType] FOREIGN KEY ([PostViewTypeID]) REFERENCES [dbo].[oxite_Blogs_PostViewType] ([PostViewTypeID])


