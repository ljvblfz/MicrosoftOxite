DECLARE @SiteID uniqueidentifier
SET @SiteID = '4F36436B-0782-4a94-BB4C-FD3916734C03'
DECLARE @BlogID uniqueidentifier
SET @BlogID = '66F2AF76-8F03-4621-8114-CAA137AFF185'
DECLARE @PostID uniqueidentifier
SET @PostID = 'C5FA9D2A-24B2-45e2-955A-92E88A34260C'
DECLARE @User1ID uniqueidentifier
SET @User1ID = '655D3498-03DB-4075-A80E-6514EC6BB6E2'

IF NOT EXISTS(SELECT * FROM oxite_Blogs_Post WHERE PostID = @PostID)
	INSERT INTO oxite_Blogs_Post (BlogID, PostID, CreatorUserID, Title, Body, BodyShort, State, Slug, CommentingDisabled, CreatedDate, ModifiedDate, PublishedDate) VALUES
	(
		@BlogID,
		@PostID,
		@User1ID,
		'World.Hello()',
		'<p>Welcome to Oxite! This is a sample application targeting developers built on <a href="http://asp.net/mvc">ASP.NET MVC</a>. Make any changes you like. If you build a feature you think other developers would be interested in and would like to share your code go to the <a href="http://www.codeplex.com/oxite">Oxite Code Plex project</a> to see how you can contribute.</p><p>To get started, sign in with "Admin" and "pa$$w0rd" and click on the Admin tab.</p><p>For more information about <a href="http://oxite.net">Oxite</a> visit the default <a href="/About">About</a> page.</p>',
		'<p>Welcome to Oxite! This is a sample application targeting developers built on <a href="http://asp.net/mvc">ASP.NET MVC</a>. Make any changes you like. If you build a feature you think other developers would be interested in and would like to share your code go to the <a href="http://www.codeplex.com/oxite">Oxite Code Plex project</a> to see how you can contribute.</p><p>To get started, sign in with "Admin" and "pa$$w0rd" and click on the Admin tab.</p><p>For more information about <a href="http://oxite.net">Oxite</a> visit the default <a href="/About">About</a> page.</p>',
		1,
		'Hello-World',
		0,
		getUtcDate(),
		getUtcDate(),
		getUtcDate()
	)

DECLARE @TagID uniqueidentifier
SET @TagID = newid()

IF NOT EXISTS(SELECT * FROM oxite_Tag WHERE TagID = @TagID)
	INSERT INTO
		oxite_Tag
	(
		ParentTagID,
		TagID,
		TagName,
		CreatedDate
	)
	VALUES
	(
		@TagID,
		@TagID,
		'Oxite',
		getUtcDate()
	)

IF NOT EXISTS(SELECT * FROM oxite_Blogs_PostTagRelationship WHERE TagID = @TagID AND PostID = @PostID)
	INSERT INTO
		oxite_Blogs_PostTagRelationship
	(
		TagID,
		PostID
	)
	VALUES
	(
		@TagID,
		@PostID
	)
