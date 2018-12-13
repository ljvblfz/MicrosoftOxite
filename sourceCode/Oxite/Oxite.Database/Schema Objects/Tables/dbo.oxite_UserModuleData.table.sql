CREATE TABLE [dbo].[oxite_UserModuleData]
(
[UserID] [uniqueidentifier] NOT NULL,
[ModuleName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Data] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


