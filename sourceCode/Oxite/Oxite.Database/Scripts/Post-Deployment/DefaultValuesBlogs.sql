DECLARE @BlogID uniqueidentifier
SET @BlogID = '66F2AF76-8F03-4621-8114-CAA137AFF185'
DECLARE @SiteID uniqueidentifier
SET @SiteID = '4F36436B-0782-4a94-BB4C-FD3916734C03'

IF NOT EXISTS(SELECT * FROM oxite_Blogs_Blog WHERE BlogID = @BlogID)
	INSERT INTO
		oxite_Blogs_Blog
	(
		SiteID,
		BlogID,
		BlogName,
		DisplayName,
		Description,
		CommentingDisabled,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@SiteID,
		@BlogID,
		'Blog',
		'Oxite Sample',
		'This is the Oxite Sample description',
		0,
		getUtcDate(),
		getUtcDate()
	)