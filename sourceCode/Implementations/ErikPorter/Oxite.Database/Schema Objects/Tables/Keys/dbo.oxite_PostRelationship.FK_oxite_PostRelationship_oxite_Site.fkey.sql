ALTER TABLE [dbo].[oxite_PostRelationship] ADD
CONSTRAINT [FK_oxite_PostRelationship_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


