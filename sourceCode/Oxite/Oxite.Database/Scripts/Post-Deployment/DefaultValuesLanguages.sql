DECLARE @LanguageID uniqueidentifier
SET @LanguageID = '2E0FDFBD-99DD-4970-BAED-2B9653672FC1'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'en',
		'English'
	)

SET @LanguageID = '73744873-B73A-4a2e-A9EB-D496452E5899'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'af',
		'Afrikaans'
	)
	
SET @LanguageID = 'A3CDD27F-13CF-40e5-ABD1-0AC461D92C90'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'sq',
		'Albanian'
	)
	
SET @LanguageID = 'D776FB0D-F80C-42a4-BD17-7F527D637931'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'gsw',
		'Alsatian'
	)

SET @LanguageID = 'D7555824-06A8-4092-B3F8-4C7704C79BF7'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'am',
		'Amharic'
	)
	
SET @LanguageID = '81C0BF33-4BFF-4fcc-9470-17770084656A'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'ar',
		'Arabic'
	)
	
SET @LanguageID = 'E75FAF5A-90A6-4ef6-A832-6D13CAF7A613'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'hy',
		'Armenian'
	)
	
SET @LanguageID = '57666CD3-8718-4434-B3CE-E035BC00FA2F'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'as',
		'Assamese'
	)
	
SET @LanguageID = '4473077D-F5D9-4ff1-B147-641B8107950C'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'az',
		'Azeri'
	)
	
SET @LanguageID = 'DE5F9E9B-73F1-4cad-9CD1-B07667904145'

IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	INSERT INTO
		oxite_Language
	(
		LanguageID,
		LanguageName,
		LanguageDisplayName
	)
	VALUES
	(
		@LanguageID,
		'ba',
		'Bashkir'
	)
	
--SET @LanguageID = ''

--IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageID = @LanguageID)
	--INSERT INTO
		--oxite_Language
	--(
		--LanguageID,
		--LanguageName,
		--LanguageDisplayName
	--)
	--VALUES
	--(
		--@LanguageID,
		--'',
		--''
	--)