ALTER TABLE [dbo].[oxite_PostView] ADD
CONSTRAINT [FK_oxite_PostView_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


