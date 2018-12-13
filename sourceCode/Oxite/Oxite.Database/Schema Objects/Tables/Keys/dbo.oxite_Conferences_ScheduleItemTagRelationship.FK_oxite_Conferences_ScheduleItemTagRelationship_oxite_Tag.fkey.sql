ALTER TABLE [dbo].[oxite_Conferences_ScheduleItemTagRelationship] ADD
CONSTRAINT [FK_oxite_Conferences_ScheduleItemTagRelationship_oxite_Tag] FOREIGN KEY ([TagID]) REFERENCES [dbo].[oxite_Tag] ([TagID])


