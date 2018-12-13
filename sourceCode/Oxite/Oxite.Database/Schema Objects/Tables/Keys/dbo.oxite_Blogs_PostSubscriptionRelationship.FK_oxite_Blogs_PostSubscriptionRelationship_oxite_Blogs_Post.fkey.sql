ALTER TABLE [dbo].[oxite_Blogs_PostSubscriptionRelationship] ADD
CONSTRAINT [FK_oxite_Blogs_PostSubscriptionRelationship_oxite_Blogs_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Blogs_Post] ([PostID])


