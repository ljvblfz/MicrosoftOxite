DECLARE @Role1ID uniqueidentifier
SET @Role1ID = '10C0B1FD-F284-4a7d-BBE0-38A671E2BD34'

IF NOT EXISTS(SELECT * FROM oxite_Role WHERE RoleName = 'Admin')
	INSERT INTO
		oxite_Role
	(
		GroupRoleID,
		RoleID,
		RoleName,
		RoleType
	)
	VALUES
	(
		@Role1ID,
		@Role1ID,
		'Admin',
		15
	)