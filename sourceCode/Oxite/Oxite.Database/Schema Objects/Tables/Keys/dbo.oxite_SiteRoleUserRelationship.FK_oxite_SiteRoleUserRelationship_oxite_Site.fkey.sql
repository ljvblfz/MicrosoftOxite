ALTER TABLE [dbo].[oxite_SiteRoleUserRelationship] ADD
CONSTRAINT [FK_oxite_SiteRoleUserRelationship_oxite_Site] FOREIGN KEY ([SiteID]) REFERENCES [dbo].[oxite_Site] ([SiteID])


