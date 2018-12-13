ALTER TABLE [dbo].[oxite_File]
	ADD CONSTRAINT [FK_oxite_File_oxite_Post] 
	FOREIGN KEY (PostID)
	REFERENCES oxite_Post (PostID)	

