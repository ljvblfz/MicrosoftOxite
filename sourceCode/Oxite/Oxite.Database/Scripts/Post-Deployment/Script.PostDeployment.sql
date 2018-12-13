/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script		
 Use SQLCMD syntax to include a file into the post-deployment script			
 Example:      :r .\filename.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\RoleMemberships.sql

:r .\RulesAndDefaults.sql

:r .\DatabaseObjectOptions.sql

:r .\Signatures.sql

:r .\DefaultValuesSites.sql

:r .\DefaultValuesLanguages.sql

:r .\DefaultValuesUsers.sql

:r .\DefaultValuesPostViewTypes.sql

:r .\DefaultValuesBlogs.sql

:r .\DefaultValuesPosts.sql

:r .\DefaultValuesPages.sql

:r .\DefaultValuesUsersRoles.sql

:r .\DefaultValuesSitesRolesUsers.sql
:r .\Permissions.sql
