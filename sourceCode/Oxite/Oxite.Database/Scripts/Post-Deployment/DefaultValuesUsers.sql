DECLARE @LanguageID uniqueidentifier
SET @LanguageID = '2E0FDFBD-99DD-4970-BAED-2B9653672FC1' --English

DECLARE @User1ID uniqueidentifier
SET @User1ID = '655D3498-03DB-4075-A80E-6514EC6BB6E2'

IF NOT EXISTS(SELECT * FROM oxite_User WHERE UserID = @User1ID)
BEGIN
	INSERT INTO
		oxite_User
	(
		UserID,
		Username,
		DisplayName,
		Email,
		HashedEmail,
		DefaultLanguageID,
		Status
	)
	VALUES
	(
		@User1ID,
		'Admin',
		'Oxite Administrator',
		'',
		'8d33d9c3c448f2c14d722900c2bd6098',
		@LanguageID,
		1
	)
	
	INSERT INTO
		oxite_UserModuleData
	(
		UserID,
		ModuleName,
		Data
	)
	VALUES
	(
		@User1ID,
		'FormsAuthentication',
		'NaCl|BQWPtrSvaXLSzkU6vOM4XeV/080hsgtsVIjLEPFny7k='
	)
END

DECLARE @User2ID uniqueidentifier
SET @User2ID = 'C0981693-799A-4331-B2DD-C83084538669'

IF NOT EXISTS(SELECT * FROM oxite_User WHERE UserID = @User2ID)
	INSERT INTO
		oxite_User
	(
		UserID,
		Username,
		DisplayName,
		Email,
		HashedEmail,
		DefaultLanguageID,
		Status
	)
	VALUES
	(
		@User2ID,
		'Anonymous',
		'',
		'',
		'',
		@LanguageID,
		1
	)

--UserLanguages
IF NOT EXISTS(SELECT * FROM oxite_UserLanguage WHERE UserID = @User1ID AND LanguageID = @LanguageID)
	INSERT INTO
		oxite_UserLanguage
	(
		UserID,
		LanguageID
	)
	VALUES
	(
		@User1ID,
		@LanguageID
	)

IF NOT EXISTS(SELECT * FROM oxite_UserLanguage WHERE UserID = @User2ID AND LanguageID = @LanguageID)
	INSERT INTO
		oxite_UserLanguage
	(
		UserID,
		LanguageID
	)
	VALUES
	(
		@User2ID,
		@LanguageID
	)