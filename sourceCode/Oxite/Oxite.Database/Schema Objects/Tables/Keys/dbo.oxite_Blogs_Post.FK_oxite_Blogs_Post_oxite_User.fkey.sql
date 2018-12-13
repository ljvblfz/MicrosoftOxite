ALTER TABLE [dbo].[oxite_Blogs_Post] ADD
CONSTRAINT [FK_oxite_Blogs_Post_oxite_User] FOREIGN KEY ([CreatorUserID]) REFERENCES [dbo].[oxite_User] ([UserID])


