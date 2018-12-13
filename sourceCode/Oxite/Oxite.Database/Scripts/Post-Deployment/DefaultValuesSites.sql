DECLARE @SiteID uniqueidentifier
SET @SiteID = '4F36436B-0782-4a94-BB4C-FD3916734C03'

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
		HasMultipleBlogs,
		RouteUrlPrefix,
		CommentingDisabled,
		PluginsPath
	)
	VALUES
	(
		@SiteID,
		'http://localhost:30913',
		'Oxite',
		'Oxite Sample',
		'This is the Oxite Sample description',
		'en',
		-8,
		' - ',
		'/Content/icons/flame.ico',
		'Normal',
		1,
		1,
		24,
		'http://mschnlnine.vo.llnwd.net/d1/oxite/gravatar.jpg',
		'/Skins',
		'/Scripts',
		'/Styles',
		'',
		'Admin',
		10,
		0,
		'',
		0,
		'/Plugins'
	)
END