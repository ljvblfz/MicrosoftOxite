CREATE TABLE [dbo].[oxite_ExtendedPropertyValue]
(
[SiteID] [uniqueidentifier] NOT NULL,
[ExtendedPropertyID] [uniqueidentifier] NOT NULL,
[ExtendedPropertyType] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ExtendedPropertyValue] [xml] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


