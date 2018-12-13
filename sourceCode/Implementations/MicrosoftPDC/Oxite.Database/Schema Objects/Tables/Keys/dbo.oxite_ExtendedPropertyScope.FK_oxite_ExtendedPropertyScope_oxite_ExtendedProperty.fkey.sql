ALTER TABLE [dbo].[oxite_ExtendedPropertyScope] ADD
CONSTRAINT [FK_oxite_ExtendedPropertyScope_oxite_ExtendedProperty] FOREIGN KEY ([ExtendedPropertyID]) REFERENCES [dbo].[oxite_ExtendedProperty] ([ExtendedPropertyID])


