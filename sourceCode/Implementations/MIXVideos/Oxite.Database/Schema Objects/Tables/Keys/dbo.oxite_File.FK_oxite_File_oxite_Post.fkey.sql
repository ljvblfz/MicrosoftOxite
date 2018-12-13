ALTER TABLE [dbo].[oxite_File] ADD
CONSTRAINT [FK_oxite_File_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])


