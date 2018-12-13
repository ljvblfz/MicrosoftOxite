ALTER TABLE [dbo].[oxite_Trackback] ADD
CONSTRAINT [FK_oxite_Trackback_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


