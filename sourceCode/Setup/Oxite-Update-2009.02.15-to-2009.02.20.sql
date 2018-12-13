/*
Script created by SQL Compare version 7.1.0 from Red Gate Software Ltd at 2/20/2009 1:43 AM
Run this script on an Oxite database from the February 15th, 2009 release to make it the 
same as the Oxite schema for the February 20th, 2009 source version
Please back up your database before running this script
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
IF EXISTS (SELECT * FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#tmpErrors')) DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
PRINT N'Creating [dbo].[oxite_File]'
GO
CREATE TABLE [dbo].[oxite_File]
(
[ID] [uniqueidentifier] NOT NULL,
[DisplayName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Url] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MimeType] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Length] [bigint] NOT NULL,
[PostID] [uniqueidentifier] NOT NULL
)
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Creating primary key [PK_oxite_File] on [dbo].[oxite_File]'
GO
ALTER TABLE [dbo].[oxite_File] ADD CONSTRAINT [PK_oxite_File] PRIMARY KEY CLUSTERED ([ID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
PRINT N'Adding foreign keys to [dbo].[oxite_File]'
GO
ALTER TABLE [dbo].[oxite_File] ADD
CONSTRAINT [FK_oxite_File_oxite_Post] FOREIGN KEY ([PostID]) REFERENCES [dbo].[oxite_Post] ([PostID])
GO
IF @@ERROR<>0 AND @@TRANCOUNT>0 ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT=0 BEGIN INSERT INTO #tmpErrors (Error) SELECT 1 BEGIN TRANSACTION END
GO
IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT 'The database update succeeded'
COMMIT TRANSACTION
END
ELSE PRINT 'The database update failed'
GO
DROP TABLE #tmpErrors
GO