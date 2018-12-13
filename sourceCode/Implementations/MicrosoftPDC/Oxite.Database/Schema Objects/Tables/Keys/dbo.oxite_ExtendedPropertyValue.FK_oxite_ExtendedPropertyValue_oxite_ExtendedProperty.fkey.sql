ALTER TABLE [dbo].[oxite_ExtendedPropertyValue] ADD
CONSTRAINT [FK_oxite_ExtendedPropertyValue_oxite_ExtendedProperty] FOREIGN KEY ([ExtendedPropertyID]) REFERENCES [dbo].[oxite_ExtendedProperty] ([ExtendedPropertyID])


