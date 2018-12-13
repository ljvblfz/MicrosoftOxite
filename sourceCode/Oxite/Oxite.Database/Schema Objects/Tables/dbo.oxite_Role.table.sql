CREATE TABLE [dbo].[oxite_Role]
(
[GroupRoleID] [uniqueidentifier] NOT NULL,
[RoleID] [uniqueidentifier] NOT NULL,
[RoleName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[RoleType] [tinyint] NOT NULL
) ON [PRIMARY]


