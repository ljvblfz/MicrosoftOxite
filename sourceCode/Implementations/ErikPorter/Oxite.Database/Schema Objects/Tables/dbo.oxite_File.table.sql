CREATE TABLE [dbo].[oxite_File]
(
	ID uniqueidentifier NOT NULL,
	DisplayName nvarchar(max) NOT NULL,
	Url varchar(max) NOT NULL,
	MimeType varchar(max) NOT NULL,
	Length bigint NOT NULL,
	PostID uniqueidentifier NOT NULL
);
