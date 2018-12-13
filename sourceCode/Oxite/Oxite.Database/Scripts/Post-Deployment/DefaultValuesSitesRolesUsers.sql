DECLARE @SiteID uniqueidentifier
SET @SiteID = '4F36436B-0782-4a94-BB4C-FD3916734C03'
DECLARE @Role1ID uniqueidentifier
SET @Role1ID = '10C0B1FD-F284-4a7d-BBE0-38A671E2BD34'

DECLARE @User1ID uniqueidentifier
SET @User1ID = '655D3498-03DB-4075-A80E-6514EC6BB6E2'
IF NOT EXISTS(SELECT * FROM oxite_SiteRoleUserRelationship WHERE SiteID = @SiteID AND RoleID = @Role1ID AND UserID = @User1ID)
	INSERT INTO
		oxite_SiteRoleUserRelationship
	(
		SiteID,
		RoleID,
		UserID
	)
	VALUES
	(
		@SiteID,
		@Role1ID,
		@User1ID
	)