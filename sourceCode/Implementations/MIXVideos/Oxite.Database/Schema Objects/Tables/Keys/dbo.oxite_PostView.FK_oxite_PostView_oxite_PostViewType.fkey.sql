ALTER TABLE [dbo].[oxite_PostView] ADD
CONSTRAINT [FK_oxite_PostView_oxite_PostViewType] FOREIGN KEY ([PostViewTypeID]) REFERENCES [dbo].[oxite_PostViewType] ([PostViewTypeID])


