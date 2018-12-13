DECLARE @Site1ID uniqueidentifier
DECLARE @Language1ID uniqueidentifier
DECLARE @Area1ID uniqueidentifier
DECLARE @Post1ID uniqueidentifier, @Post2ID uniqueidentifier
DECLARE @Tag1ID uniqueidentifier
DECLARE @User1ID uniqueidentifier, @User2ID uniqueidentifier, @Role1ID uniqueidentifier, @Role2ID uniqueidentifier
DECLARE @StringResourceKeyNewComment nvarchar(max)

--Sites
SET @Site1ID = '4F36436B-0782-4a94-BB4C-FD3916734C03'

IF NOT EXISTS(SELECT * FROM oxite_Site WHERE SiteHost = 'http://localhost:30913')
BEGIN
	INSERT INTO
		oxite_Site
	(
		SiteID,
		SiteHost,
		SiteName,
		SiteDisplayName,
		SiteDescription,
		LanguageDefault,
		TimeZoneOffset,
		PageTitleSeparator,
		FavIconUrl,
		CommentStateDefault,
		IncludeOpenSearch,
		AuthorAutoSubscribe,
		PostEditTimeout,
		GravatarDefault,
		SkinsPath,
		SkinsScriptsPath,
		SkinsStylesPath,
		Skin,
		AdminSkin,
		ServiceRetryCountDefault,
		HasMultipleAreas,
		RouteUrlPrefix,
		CommentingDisabled
	)
	VALUES
	(
		@Site1ID,
		'http://localhost:30913',
		'Oxite',
		'Oxite Sample',
		'This is the Oxite Sample description',
		'en',
		-8,
		' - ',
		'/Content/icons/flame.ico',
		'PendingApproval',
		1,
		1,
		24,
		'http://mschnlnine.vo.llnwd.net/d1/oxite/gravatar.jpg',
		'/Skins',
		'/Scripts',
		'/Styles',
		'Default',
		'Admin',
		10,
		0,
		'',
		0
	)
END

--PostViewType
IF NOT EXISTS(SELECT * FROM oxite_PostViewType WHERE PostViewTypeName = 'Post-Web')
BEGIN
	INSERT INTO
		oxite_PostViewType
	(
		PostViewTypeID,
		PostViewTypeName
	)
	VALUES
	(
		'A38FD696-22E2-4612-A392-E73FCAABB61D',
		'Post-Web'
	)
END

IF NOT EXISTS(SELECT * FROM oxite_PostViewType WHERE PostViewTypeName = 'Post-RSS')
BEGIN
	INSERT INTO
		oxite_PostViewType
	(
		PostViewTypeID,
		PostViewTypeName
	)
	VALUES
	(
		'6A945758-48C3-4e43-A1E3-AED601F5F022',
		'Post-RSS'
	)
END

IF NOT EXISTS(SELECT * FROM oxite_PostViewType WHERE PostViewTypeName = 'Post-ATOM')
BEGIN
	INSERT INTO
		oxite_PostViewType
	(
		PostViewTypeID,
		PostViewTypeName
	)
	VALUES
	(
		'E3080032-0203-42c4-8B36-76A8168FB474',
		'Post-ATOM'
	)
END

--Languages
IF NOT EXISTS(SELECT * FROM oxite_Language WHERE LanguageName = 'en')
BEGIN
	SET @Language1ID = newid()
	
	INSERT INTO oxite_Language (LanguageID, LanguageName, LanguageDisplayName) VALUES (@Language1ID, 'en', 'English')
END
ELSE
	SELECT @Language1ID = LanguageID FROM oxite_Language WHERE LanguageName = 'en'

--Users
IF NOT EXISTS(SELECT * FROM oxite_User WHERE Username = 'Admin')
BEGIN
	SET @User1ID = newid()
	INSERT INTO
		oxite_User
	(
		UserID,
		Username,
		DisplayName,
		Email,
		HashedEmail,
		Password,
		PasswordSalt,
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
		'BQWPtrSvaXLSzkU6vOM4XeV/080hsgtsVIjLEPFny7k=',
		'NaCl',
		@Language1ID,
		1
	)
END
ELSE
BEGIN
	SELECT @User1ID = UserID FROM oxite_User WHERE Username = 'Admin'
END

IF NOT EXISTS(SELECT * FROM oxite_User WHERE Username = 'Anonymous')
BEGIN
	SET @User2ID = newid()
	INSERT INTO
		oxite_User
	(
		UserID,
		Username,
		DisplayName,
		Email,
		HashedEmail,
		Password,
		PasswordSalt,
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
		'',
		'',
		@Language1ID,
		1
	)
END
ELSE
BEGIN
	SELECT @User2ID = UserID FROM oxite_User WHERE Username = 'Anonymous'
END

--UserLanguages
IF NOT EXISTS(SELECT * FROM oxite_UserLanguage WHERE UserID = @User1ID AND LanguageID = @Language1ID)
	INSERT INTO oxite_UserLanguage (UserID, LanguageID) VALUES (@User1ID, @Language1ID)
IF NOT EXISTS(SELECT * FROM oxite_UserLanguage WHERE UserID = @User2ID AND LanguageID = @Language1ID)
	INSERT INTO oxite_UserLanguage (UserID, LanguageID) VALUES (@User2ID, @Language1ID)

--Roles
IF NOT EXISTS(SELECT * FROM oxite_Role WHERE RoleName = 'SiteOwner')
BEGIN
	SET @Role1ID = newid()
	INSERT INTO
		oxite_Role
	(
		ParentRoleID,
		RoleID,
		RoleName
	)
	VALUES
	(
		@Role1ID,
		@Role1ID,
		'SiteOwner'
	)
END
ELSE
BEGIN
	SELECT @Role1ID = RoleID FROM oxite_Role WHERE RoleName = 'SiteOwner'
END

IF NOT EXISTS(SELECT * FROM oxite_Role WHERE RoleName = 'AreaOwner')
BEGIN
	SET @Role2ID = newid()
	INSERT INTO
		oxite_Role
	(
		ParentRoleID,
		RoleID,
		RoleName
	)
	VALUES
	(
		@Role2ID,
		@Role2ID,
		'AreaOwner'
	)
END
ELSE
BEGIN
	SELECT @Role2ID = RoleID FROM oxite_Role WHERE RoleName = 'AreaOwner'
END

--UserRoleRelationships
IF NOT EXISTS(SELECT * FROM oxite_UserRoleRelationship WHERE UserID = @User1ID AND RoleID = @Role1ID)
	INSERT INTO
		oxite_UserRoleRelationship
	(
		UserID,
		RoleID
	)
	VALUES
	(
		@User1ID,
		@Role1ID
	)

--StringResources
SET @StringResourceKeyNewComment = 'Oxite.4F36436B07824A94BB4CFD3916734C03.Messages.NewComment'
IF NOT EXISTS(SELECT * FROM oxite_StringResource WHERE StringResourceKey = @StringResourceKeyNewComment)
	INSERT INTO
		oxite_StringResource
	(
		StringResourceKey,
		[Language],
		Version,
		StringResourceValue,
		CreatorUserID,
		[CreatedDate]
	)
	VALUES
	(
		@StringResourceKeyNewComment,
		'en',
		1,
		'<h1>New Comment on {Site.Name}</h1>
<h2>{User.Name} commented on ''{Post.Title}'' at {Post.Published}</h2>
<p>{Comment.Body}</p>
<a href="{Comment.Permalink}">{Comment.Permalink}</a>',
		@User1ID,
		getUtcDate()
	)
IF NOT EXISTS(SELECT * FROM oxite_StringResourceVersion WHERE StringResourceKey = @StringResourceKeyNewComment AND [Language] = 'en' AND Version = 1)
	INSERT INTO
		oxite_StringResourceVersion
	(
		StringResourceKey,
		[Language],
		Version,
		StringResourceValue,
		CreatorUserID,
		[CreatedDate],
		State
	)
	SELECT
		StringResourceKey,
		[Language],
		Version,
		StringResourceValue,
		CreatorUserID,
		[CreatedDate],
		1 AS State
	FROM
		oxite_StringResource
	WHERE
		StringResourceKey = @StringResourceKeyNewComment

--Areas
IF NOT EXISTS(SELECT * FROM oxite_Area WHERE SiteID = @Site1ID AND AreaName = 'Blog')
BEGIN
	SET @Area1ID = '66F2AF76-8F03-4621-8114-CAA137AFF185'
	INSERT INTO
		[oxite_Area]
	(
		SiteID,
		AreaID,
		AreaName,
		DisplayName,
		Description,
		CommentingDisabled,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@Site1ID,
		@Area1ID,
		'Blog',
		'Oxite Sample',
		'This is the Oxite Sample description',
		0,
		getUtcDate(),
		getUtcDate()
	)
END
ELSE
BEGIN
	SELECT @Area1ID = [AreaID] FROM [oxite_Area] WHERE SiteID = @Site1ID AND [AreaName] = 'Blog'
END

--AreaRoleRelationships
IF NOT EXISTS(SELECT * FROM oxite_AreaRoleRelationship WHERE AreaID = @Area1ID AND RoleID = @Role1ID)
	INSERT INTO
		oxite_AreaRoleRelationship
	(
		AreaID,
		RoleID
	)
	VALUES
	(
		@Area1ID,
		@Role1ID
	)
IF NOT EXISTS(SELECT * FROM oxite_AreaRoleRelationship WHERE AreaID = @Area1ID AND RoleID = @Role2ID)
	INSERT INTO
		oxite_AreaRoleRelationship
	(
		AreaID,
		RoleID
	)
	VALUES
	(
		@Area1ID,
		@Role2ID
	)

--Posts
IF NOT EXISTS(SELECT * FROM oxite_Post WHERE Title = 'World.Hello()')
BEGIN
	SET @Post1ID = newid()
	INSERT INTO oxite_Post (PostID, CreatorUserID, Title, Body, BodyShort, State, Slug, CommentingDisabled, [CreatedDate], [ModifiedDate], [PublishedDate], SearchBody) VALUES
	(
		@Post1ID,
		@User1ID,
		'World.Hello()',
		'Welcome to Oxite! &nbsp;This is a sample application targeting developers built on <a href="http://asp.net/mvc">ASP.NET MVC</a>. &nbsp;Make any changes you like. &nbsp;If you build a feature you think other developers would be interested in and would like to share your code go to the <a href="http://www.codeplex.com/oxite">Oxite Code Plex project</a> to see how you can contribute.<br /><br />To get started, sign in with "Admin" and "pa$$w0rd" and click on the Admin tab.<br /><br />For more information about <a href="http://oxite.net">Oxite</a> visit the default <a href="/About">About</a> page.',
		'Welcome to Oxite! &nbsp;This is a sample application targeting developers built on <a href="http://asp.net/mvc">ASP.NET MVC</a>. &nbsp;Make any changes you like. &nbsp;If you build a feature you think other developers would be interested in and would like to share your code go to the <a href="http://www.codeplex.com/oxite">Oxite Code Plex project</a> to see how you can contribute.<br /><br />To get started, sign in with "Admin" and "pa$$w0rd" and click on the Admin tab.<br /><br />For more information about <a href="http://oxite.net">Oxite</a> visit the default <a href="/About">About</a> page.',
		1,
		'Hello-World',
		0,
		getUtcDate(),
		getUtcDate(),
		getUtcDate(),
		''
	)
END
ELSE
BEGIN
	SELECT @Post1ID = PostID FROM oxite_Post WHERE Title = 'World.Hello()'
END

IF NOT EXISTS(SELECT * FROM oxite_Post WHERE Title = 'About')
BEGIN
	SET @Post2ID = newid()
	INSERT INTO oxite_Post (PostID, CreatorUserID, Title, Body, BodyShort, State, Slug, CommentingDisabled, [CreatedDate], [ModifiedDate], [PublishedDate], SearchBody) VALUES
	(
		@Post2ID,
		@User1ID,
		'About',
		'<p>Welcome to the Oxite Sample! Since this is a sample, we thought we would use this handy about page to explain a few things about the code and about the thoughts that went into its creation.</p>
<h3>What is this?</h3>    
<p>This is a simple blog engine written using <a href="http://asp.net/mvc">ASP.NET MVC</a>, and is designed with two main goals:</p>
<ol class="normal">
	<li>To provide a sample of ''core blog functionality'' in a reusable fashion. &nbsp;Blogs are simple and well understood by many developers, but the set of basic functions that a blog needs to implement (trackbacks, rss, comments, etc.) are fairly complex.  Hopefully this code helps.</li>
	<li>To provide a real-world sample written using <a href="http://asp.net/mvc">ASP.NET MVC</a>.</li>
</ol>
<p>We aren''t a sample-building team (more on what we are in a bit). &nbsp;We couldn''t sit down and build this code base just to give out to folks, so this code is also the foundation of a real project of ours, <a href="http://visitmix.com">MIX Online</a>. &nbsp;We also created this project to be the base of our own personal blogs as well; check out <a href="http://www.codeplex.com/oxite/Wiki/View.aspx?title=oxitesites&amp;referringTitle=Home">this page on CodePlex to see a list of sites that use Oxite</a> (and use the comments area to tell us about your site).</p>
<h3>What this isn''t</h3>
<p>This is not an ''off the shelf'' blogging package. &nbsp;If you aren''t a developer and just want to get blogging then you should look at one of these great blogging products: <a href="http://graffiticms.com/">Graffiti</a>, <a href="http://subtextproject.com/">SubText</a>, <a href="http://www.dotnetblogengine.net/">Blog Engine .NET</a>, <a href="http://dasblog.info/">dasBlog</a> or a hosted service like <a href="http://wordpress.com/">Wordpress</a></p>
<p>Oxite is also not ready to be an enterprise blogging solution (for you and a thousand other bloggers at your company), although we did design it to be capable of hosting multiple blogs. &nbsp;For that type of solution, a set of provisioning tools to create new blogs would need to be added. &nbsp;Oxite is code though, so you can extend it and customize it to support whatever you need.</p>
<h3>Where to go from here (expanding on this sample)</h3>
<p>You can extend Oxite in whatever way you need or wish, but if you are looking for some ideas here are a few thoughts we''ve already had around new functionality:</p>
<ul>
	<li>Adding a rich-text-editor for post and page editing. &nbsp;We use <a href="http://writer.live.com">Windows Live Writer</a> to post and edit our blog posts, so this isn''t a real issue for our day to day use of the site, but adding an editor like <a href="http://developer.yahoo.com/yui/editor/">http://developer.yahoo.com/yui/editor/</a> would be great.</li>
	<li>Adding UI for managing the creation of new Areas, setting up users and user permissions, etc. &nbsp;If you decided to use Oxite to host many blogs together on one site, or to use the same Oxite database to run many sites (yes, it can do both of those!) then it would be nice to have some UI for managing all those contributors.</li>
	<li>And whatever great idea you have!</li>
</ul>
<h3>Getting the code, reporting bugs, and contributing to this project</h3>
<p><a href="http://codeplex.com/Oxite">Oxite is hosted on CodePlex</a>, so you can grab the latest code from there (you can <a href="http://www.codeplex.com/oxite/SourceControl/ListDownloadableCommits.aspx">see all of our check-ins</a> and also <a href="http://www.codeplex.com/oxite/Release/ProjectReleases.aspx">specific releases</a> when we feel like significant changes have been made), read discussions, file bugs and even submit suggestions for changes. &nbsp;If you''ve made some code changes that you feel should make it back into the Oxite code, then CodePlex is the place to tell us about it!</p>
<h3>About us</h3>
<p>Oxite is a project built by the team behind <a href="http://channel9.msdn.com/">Channel 9</a> (and <a href="http://channel8.msdn.com/">Channel 8</a>, <a href="http://on10.net/">Channel 10</a>, <a href="http://edge.technet.com/">TechNet Edge</a>, <a href="http://visitmix.com/">Mix Online</a>): Erik Porter, Nathan Heskew, Mike Sampson and Duncan Mackenzie. &nbsp;You can find out more about our team and our projects in our <a href="http://channel9.msdn.com/tags/evnet/">various posts and videos on Channel 9</a>.</p>
<p><a href="http://validator.w3.org/check?uri=referer"><img src="/Content/images/valid-xhtml10-blue.png" alt="Valid XHTML 1.0 Strict" height="31" width="88" /></a> <a href="http://jigsaw.w3.org/css-validator/"><img style="border:0;width:88px;height:31px" src="/Content/images/vcss-blue.gif" alt="Valid CSS!" /></a> <a href="http://validator.w3.org/feed/check.cgi"><img src="/Content/images/valid-rss.png" alt="[Valid RSS]" title="Validate my RSS feed" /></a></p>',
		'About',
		1,
		'About',
		0,
		getUtcDate(),
		getUtcDate(),
		getUtcDate(),
		''
	)
END
ELSE
BEGIN
	SELECT @Post2ID = PostID FROM oxite_Post WHERE Title = 'About'
END

--Update Post SearchBody
UPDATE
	oxite_Post
SET
	SearchBody = Title + ' ' + (SELECT DisplayName FROM oxite_User WHERE UserID = CreatorUserID) + ' ' + Body

--PostAreaRelationships
IF NOT EXISTS(SELECT * FROM [oxite_PostAreaRelationship] WHERE AreaID = @Area1ID AND PostID = @Post1ID)
	INSERT INTO
		[oxite_PostAreaRelationship]
	(
		PostID,
		AreaID
	)
	VALUES
	(
		@Post1ID,
		@Area1ID
	)

--PostRelationships
IF NOT EXISTS(SELECT * FROM oxite_PostRelationship WHERE ParentPostID = @Post2ID AND PostID = @Post2ID)
	INSERT INTO
		oxite_PostRelationship
	(
		SiteID,
		ParentPostID,
		PostID
	)
	VALUES
	(
		@Site1ID,
		@Post2ID,
		@Post2ID
	)

--Tags
IF NOT EXISTS(SELECT * FROM oxite_Tag WHERE TagName = 'Oxite')
BEGIN
	SET @Tag1ID = newid()
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
		@Tag1ID,
		@Tag1ID,
		'Oxite',
		getUtcDate()
	)
END
ELSE
BEGIN
	SELECT @Tag1ID = TagID FROM oxite_Tag WHERE TagName = 'Oxite'
END

--PostTagRelationships
IF NOT EXISTS(SELECT * FROM oxite_PostTagRelationship WHERE TagID = @Tag1ID AND PostID = @Post1ID)
	INSERT INTO
		oxite_PostTagRelationship
	(
		TagID,
		PostID
	)
	VALUES
	(
		@Tag1ID,
		@Post1ID
	)
