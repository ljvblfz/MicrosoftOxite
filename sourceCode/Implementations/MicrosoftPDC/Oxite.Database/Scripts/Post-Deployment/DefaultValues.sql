--Sites
DECLARE @SiteID uniqueidentifier
SET @SiteID = '4F36436B-0782-4a94-BB4C-FD3916734C03'

IF NOT EXISTS(SELECT * FROM oxite_Site WHERE SiteHost = 'http://microsoftpdc-int.com')
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
		'http://microsoftpdc-int.com',
		'PDC09',
		'Microsoft PDC09',
		'',
		'en',
		-8,
		' :: ',
		'/Content/icons/frog.ico',
		'Normal',
		1,
		1,
		24,
		'/Content/images/gravatar_anon.gif',
		'/Skins',
		'/Scripts',
		'/Styles',
		'PDC09',
		'Admin',
		10,
		0,
		'',
		0,
		'/Plugins'
	)
END

--Modules
IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'AspNetCache')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'AspNetCache',
		0,
		'Oxite.Modules.AspNetCache.AspNetCacheModule, Oxite',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Membership')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Membership',
		1,
		'Oxite.Modules.Membership.MembershipModule, Oxite',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'FormsAuthentication')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'FormsAuthentication',
		2,
		'Oxite.Modules.FormsAuthentication.FormsAuthenticationModule, Oxite',
		1,
		1
	)
END



IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Tags')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Tags',
		4,
		'Oxite.Modules.Tags.TagsModule, Oxite',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Files')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Files',
		5,
		'Oxite.Modules.Files.FilesModule, Oxite',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Comments')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Comments',
		6,
		'Oxite.Modules.Comments.CommentsModule, Oxite',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Core')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Core',
		7,
		'Oxite.Modules.Core.OxiteModule, Oxite',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Plugins')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Plugins',
		8,
		'Oxite.Modules.Plugins.PluginsModule, Oxite',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Blogs')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Blogs',
		9,
		'Oxite.Modules.Blogs.BlogsModule, Oxite.Blogs',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Conferences')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Conferences',
		10,
		'Oxite.Modules.Conferences.ConferencesModule, Oxite.Conferences',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'CMS')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'CMS',
		11,
		'Oxite.Modules.CMS.CMSModule, Oxite.CMS',
		1,
		1
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Bing')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Bing',
		12,
		'Oxite.Modules.Bing.BingModule, Oxite.Bing',
		1,
		0
	)
END

IF NOT EXISTS(SELECT * FROM oxite_Module WHERE ModuleName = 'Site')
BEGIN
	INSERT INTO
		oxite_Module
	(
		SiteID,
		ModuleName,
		ModuleOrder,
		ModuleType,
		Enabled,
		IsSystem
	)
	VALUES
	(
		@SiteID,
		'Site',
		13,
		'OxiteSite.App_Code.Modules.OxiteSite.OxiteSiteModule',
		1,
		0
	)
END

--PostViewType
IF NOT EXISTS(SELECT * FROM oxite_Blogs_PostViewType WHERE PostViewTypeName = 'Post-Web')
BEGIN
	INSERT INTO
		oxite_Blogs_PostViewType
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

IF NOT EXISTS(SELECT * FROM oxite_Blogs_PostViewType WHERE PostViewTypeName = 'Post-RSS')
BEGIN
	INSERT INTO
		oxite_Blogs_PostViewType
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

IF NOT EXISTS(SELECT * FROM oxite_Blogs_PostViewType WHERE PostViewTypeName = 'Post-ATOM')
BEGIN
	INSERT INTO
		oxite_Blogs_PostViewType
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

--Users
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
	
	/*INSERT INTO
		oxite_UserModuleData
	(
		UserID,
		ModuleName,
		Data
	)
	VALUES
	(
		@User1ID,
		'LiveID',
		'00037FFF81CF8E22'
	)*/
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

--Roles
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

--SiteRoleUserRelationships
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

--Blogs
DECLARE @BlogID uniqueidentifier
SET @BlogID = '66F2AF76-8F03-4621-8114-CAA137AFF185'

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
		'BehindTheScenes',
		'Behind the Scenes',
		'This is the Oxite Sample description',
		0,
		getUtcDate(),
		getUtcDate()
	)

--Posts
DECLARE @PostID uniqueidentifier
SET @PostID = 'C5FA9D2A-24B2-45e2-955A-92E88A34260C'

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

--Pages
DECLARE @PageID uniqueidentifier, @ContentItemID uniqueidentifier
SET @PageID = 'D2D69195-F49A-4f68-AA3C-D7B4CF69F695'
SET @ContentItemID = '87A41580-2BD1-4859-87E9-EDC97060FE96'

IF NOT EXISTS(SELECT * FROM oxite_CMS_Page WHERE PageID = @PageID)
BEGIN
	INSERT INTO
		oxite_CMS_Page
	(
		SiteID,
		PageID,
		TemplateName,
		Title,
		Description,
		Slug,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		@PageID,
		NULL,
		'Why PDC is for you',
		'This is the page that explains what PDC09 is all about',
		'About',
		getUtcDate()
	)
	
	INSERT INTO
		oxite_CMS_ContentItem
	(
		SiteID,
		PageID,
		ContentItemID,
		ContentItemName,
		ContentItemDisplayName,
		Body,
		Version,
		CreatorUserID,
		CreatedDate,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		@PageID,
		@ContentItemID,
		'Content',
		'Content',
		'<div id="hero"><img src="/Content/images/about.jpg"/>
	<p>Aenean cursus tortor sit amet metus viverra id enim populiidiv#primarynavadipiscing. Sed ullamcorper congue pretium potenti. Morbi div#primarynavvehicula elit eget dui commodo commodo sodales lacus div#primarynavvenenatis. Quisque dapibus lacus a velit convallis tincidunt div#primarynavmollis orci posuere. Etiam varius libero sit amet ante blandit div#primarynavtristique. Aenean placerat lectus at neque gravida vitae div#primarynavblandit tellus blandit Pellentesque lorem nulla, gravida nec div#primarynavegestas sed, commodo a felis. Mauris rutrum convallis div#primarynavtempor. Nunc auctor venenatis mauris, vitae gravida sem.</p>
</div>
  <h3>What you get for attending</h3>
  <img src="/Content/benefits.jpg" class="pullout"  />
  <ul>
    <li>Lorem ipsum dolor sit amet, consectetur adipisci ngelit. Morbi volutpat div#primarynavtellus velipsum mollis consec tetur etiam posuere</li>
    <li>Lorem ipsum dolor sit amet, consectetur adipisci ngelit. Morbi volutpat div#primarynavtellus velipsum mollis consec tetur etiam posuere</li>
    <li>Lorem ipsum dolor sit amet, consectetur adipisci ngelit. Morbi volutpat div#primarynavtellus velipsum mollis consec tetur etiam posuere</li>
    <li>Lorem ipsum dolor sit amet, consectetur adipisci ngelit. Morbi volutpat div#primarynavtellus velipsum mollis consec tetur etiam posuere</li>
  </ul>
  <p class="quote">Sed quis tempus tellus. Suspendisse potenti. Ut mauris velit, scelerisque ut tempus sit div#primarynavamet, pretium vitae ipsum.Phasellus risus justo, facilisis a tempor a, facilisis nec sapien. div#primarynavSed blandit massa quis mauris ultrices non scelerisque orci consequat. Curabitur ultricies div#primarynavneque vitae arcu elementum eu vestibulum diam</p>
  <h3>A Family of Conferences</h3>
  <a href="#">TechEd</a>
  <p>Lorem ipsum dolor sit amet, consectetur populii adipiscing elit. Morbi volutpat tellus vel ipsum mollis div#primarynavconsectetur. Etiam posuere, libero vel dapibus faucibus, magna tortor dictum diam, ac accumsan nisi arcu interdum nibh lorem ipsum dolor sit amet.</p>
  <a href="#">MIX10</a>
  <p>Lorem ipsum dolor sit amet, consectetur populii adipiscing elit. Morbi volutpat tellus vel ipsum mollis div#primarynavconsectetur. Etiam posuere, libero vel dapibus faucibus, magna tortor dictum diam, ac accumsan nisi arcu interdum nibh lorem ipsum dolor sit amet.</p>',
		1,
		@User1ID,
		getUtcDate(),
		getUtcDate()
	)
END

DECLARE @Page01ID uniqueidentifier, @ContentItem01ID uniqueidentifier
SET @Page01ID = '4B043C78-ABBF-483c-99D9-D74DB4561A8C'
SET @ContentItem01ID = '2183A983-1C3D-4810-9AA4-B1FE5CABDD54'

IF NOT EXISTS(SELECT * FROM oxite_CMS_Page WHERE PageID = @Page01ID)
BEGIN
	INSERT INTO
		oxite_CMS_Page
	(
		SiteID,
		PageID,
		TemplateName,
		Title,
		Description,
		Slug,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		@Page01ID,
		NULL,
		'Registration',
		'PDC09 Event Registration',
		'Registration',
		getUtcDate()
	)
	
	INSERT INTO
		oxite_CMS_ContentItem
	(
		SiteID,
		PageID,
		ContentItemID,
		ContentItemName,
		ContentItemDisplayName,
		Body,
		Version,
		CreatorUserID,
		CreatedDate,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		@Page01ID,
		@ContentItem01ID,
		'Content',
		'Content',
		'<div id="hero"><img src="/Content/images/registration.jpg" />
	<h2>PDC 2009 Event Registration<br /> is still open</h2>
	<a href="/register.aspx"><img src="/Skins/PDC09/Styles/images/btn_register_now.gif" alt="Register Now" /></a>
				</div>
	<h3>Event Information</h3>
	<div id="eventinfo">
		<p>Workshops: Date, 2009 | Conference: Date, 2009</p>
		<address>
			Los Angeles Convention Center<br />
			123 My Street</br>
			Los Angeles, California 90210
		</address>
		<a href="#">Directions</a>
		<p>Phone: 702.414.1000 or 877.883.1111</p>
	</div>
	<div id="eventpricing">
		<table>
			<thead>
				<tr>
					<td>Event Pricing (all prices in USD):</td>
					<td>&nbsp;</td>
				</tr>
			</thead>
			<tbody>
				<tr>
					<td>Conference registration:</td>
					<td>$1395</td>
				</tr>
				<tr>
					<td>Workshops Registration (additional cost):</td>
					<td>$295</td>
				</tr>
				<tr>
					<td>Academic Discounted Registration*:</td>
					<td>$595</td>
				</tr>
			</tbody>
		</table>
		<p class="footnote">
			* <a href="mailto:">Email</a> the PDC registration team for details
		</p>
	</div>
	<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ullamcorper augue non neque placerat vehicula. Maecenas cursus tincidunt erat quis ornare. Integer suscipit tellus id lectus facilisis sed pulvinar sapien congue. Duis posuere massa in eros lacinia et dapibus est rutrum. Aliquam in pharetra nibh. Ut in vestibulum mi. Sed porta dolor eget velit lobortis eget tempus arcu tristique. Nulla facilisi. Nullam vel quam non turpis viverra vehicula et vel velit. Nullam sed arcu turpis. Donec malesuada mollis pharetra. Quisque aliquam velit ut leo aliquet quis sodales libero accumsan. </p>
	<h3>Registration Contact Information</h3>
	<p>If you have any questions regarding registration, lodging, or information appearing on this web site or need additional assistance, please contact the PDC09 Registration Team:</p>
	<table id="contact">
		<tr>
			<td>United States Telephone:</td>
			<td>1.877.795.4686</td>
		</tr>
		<tr>
			<td>Worldwide Telephone:</td>
			<td>1.206.957.4798</td>
		</tr>
		<tr>
			<td>Worldwide Fax:</td>
			<td>1.206.783.5594</td>
		</tr>
		<tr>
			<td>Email:</td>
			<td><a href="mailto:pdc09@ustechs.com">PDC09@ustechs.com</a></td>
		</tr>
	</table>
	<p>Registration hours are Monday through Friday, 8:00am to 5:00pm, Pacific Time</p>
	<h3>Event Cancellation Policy</h3>
	<p>Vivamus euismod, urna quis sagittis aliquam, lectus felis consectetur ante, ut volutpat neque justo ac felis. Vestibulum interdum luctus sem ut fermentum. Pellentesque accumsan tincidunt nunc, in interdum odio facilisis ac. Phasellus eros neque, volutpat vitae pretium in, ultrices et libero. Cras vitae arcu risus, quis aliquam ante. Cras vitae erat ut elit pellentesque dapibus. Donec eget magna velit. Suspendisse sed mauris sed leo aliquet tristique. Duis vehicula laoreet neque eget pretium. Curabitur sem lacus, aliquet vitae laoreet eu, faucibus non augue. Proin tincidunt sollicitudin nulla, vitae molestie mi sodales aliquet. Donec condimentum diam sed nisi ornare sit amet vehicula lorem ullamcorper. Nullam lacinia varius commodo. Suspendisse et purus lectus. Phasellus vestibulum faucibus feugiat. </p>',
		1,
		@User1ID,
		getUtcDate(),
		getUtcDate()
	)
END    

DECLARE @Page02ID uniqueidentifier, @ContentItem02ID uniqueidentifier
SET @Page02ID = 'B7FE56DF-0DE4-469a-8CF7-AD74DFE41AD5'
SET @ContentItem02ID = 'F9FCD02E-DF4E-40dd-8E28-7D90010468D1'

IF NOT EXISTS(SELECT * FROM oxite_CMS_Page WHERE PageID = @Page02ID)
BEGIN
	INSERT INTO
		oxite_CMS_Page
	(
		SiteID,
		PageID,
		TemplateName,
		Title,
		Description,
		Slug,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		@Page02ID,
		NULL,
		'Sessions',
		'Session Browser',
		'Sessions',
		getUtcDate()
	)
	
	INSERT INTO
		oxite_CMS_ContentItem
	(
		SiteID,
		PageID,
		ContentItemID,
		ContentItemName,
		ContentItemDisplayName,
		Body,
		Version,
		CreatorUserID,
		CreatedDate,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		@Page02ID,
		@ContentItem02ID,
		'SessionsDescription',
		'Content',
		'<p>Azure services enable developers to easily create or extend their applications and services. From consumer-targeted applications and social networking web sites to enterprise class applications and services, these services make it easy for you to give your applications and services the most compelling experiences and features.</p>
    <p>Learn about the Azure services that enable developers to easily create or extend their applications and vices to the most compelling experiences and features.</p>
    <p>From consumer-targeted applications and social networking web sites to enterprise class applications and services, these services make it easy for you to give your applications and services the most compelling experiences and features.</p>',
		1,
		@User1ID,
		getUtcDate(),
		getUtcDate()
	)
END

DECLARE @ContentItem2ID uniqueidentifier
SET @ContentItem2ID = '09E83210-7715-4e1f-A29B-A777B56F63E1'

IF NOT EXISTS(SELECT * FROM oxite_CMS_ContentItem WHERE ContentItemID = @ContentItem2ID)
BEGIN
INSERT INTO
		oxite_CMS_ContentItem
	(
		SiteID,
		PageID,
		ContentItemID,
		ContentItemName,
		ContentItemDisplayName,
		Body,
		Version,
		CreatorUserID,
		CreatedDate,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		NULL,
		@ContentItem2ID,
		'CallToAction',
		'Call To Action',
		'<h3>Register Today SAVE $100</h3>
<p>Register today to receive an additional $500 off your registration to PDC09!</p>
<a class="button" href="#"><span>Register for PDC09</span></a>
<p>Log in and start adding the latest PDC sessions to your PDC09 calendar.</p>
<a class="button" href="#"><span>Sign in now</span></a>',
		1,
		@User1ID,
		getUtcDate(),
		getUtcDate()
	)
END

DECLARE @ContentItem3ID uniqueidentifier
SET @ContentItem3ID = '1C8E4AC1-A17A-48bb-A841-450032CA077A'

IF NOT EXISTS(SELECT * FROM oxite_CMS_ContentItem WHERE ContentItemID = @ContentItem3ID)
BEGIN

INSERT INTO
		oxite_CMS_ContentItem
	(
		SiteID,
		PageID,
		ContentItemID,
		ContentItemName,
		ContentItemDisplayName,
		Body,
		Version,
		CreatorUserID,
		CreatedDate,
		PublishedDate
	)
	VALUES
	(
		@SiteID,
		NULL,
		@ContentItem3ID,
		'Sponsors',
		'Sponsors',
		'<ul>
	<li><a href="#"><img src="/content/images/sponsors_devexpress.gif" alt="Devexpress" /></a></li>
</ul>',
		1,
		@User1ID,
		getUtcDate(),
		getUtcDate()
	)
END

--Conferences
DECLARE @EventID uniqueidentifier
SET @EventID = '9820AE2C-1514-4f44-BC13-0C7F04AB9691'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_Event WHERE EventID = @EventID)
	INSERT INTO
		oxite_Conferences_Event
	(
		EventID,
		EventName,
		EventDisplayName,
		Year
	)
	VALUES
	(
		@EventID,
		'PDC09',
		'Microsoft PDC09',
		2009
	)
	
DECLARE @Speaker1ID uniqueidentifier
SET @Speaker1ID = '8F523BFF-7636-4c8c-B5F7-1956DBC7FCEA'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_Speaker WHERE SpeakerID = @Speaker1ID)
	INSERT INTO
		oxite_Conferences_Speaker
	(
		SpeakerID,
		SpeakerName,
		SpeakerDisplayName,
		Bio
	)
	VALUES
	(
		@Speaker1ID,
		'Some1',
		'Some One',
		'Lorem Ipsum Guru - Lorem ipsum dolor sit amet, consectetur adipiscing elit. In posuere facilisis vulputate. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Fusce luctus nunc vitae lacus posuere eu sodales erat venenatis. Donec sollicitudin arcu eu purus sollicitudin a suscipit purus sollicitudin.'
	)
	
DECLARE @Speaker2ID uniqueidentifier
SET @Speaker2ID = '7E1854C8-3DF6-417a-AD0B-318F1E4DACF5'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_Speaker WHERE SpeakerID = @Speaker2ID)
	INSERT INTO
		oxite_Conferences_Speaker
	(
		SpeakerID,
		SpeakerName,
		SpeakerDisplayName,
		Bio
	)
	VALUES
	(
		@Speaker2ID,
		'Somebody',
		'Some Body',
		'The *real* Lorem Ipsum Guru - Lorem ipsum dolor sit amet, consectetur adipiscing elit. In posuere facilisis vulputate. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Fusce luctus nunc vitae lacus posuere eu sodales erat venenatis. Donec sollicitudin arcu eu purus sollicitudin a suscipit purus sollicitudin.'
	)

DECLARE @ScheduleItem1ID uniqueidentifier
SET @ScheduleItem1ID = '1746BDE8-4BCF-4d97-821B-6B675A06C7F3'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem1ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem1ID,
		'Advanced Lorem Ipsum',
		'This session really lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'11/17/2009 11:00:00',
		'11/17/2009 12:30:00',
		'AdvancedLoremIpsum',
		'04/01/2009 8:00:00',
		'04/01/2009 8:00:00'
	)

DECLARE @ScheduleItem2ID uniqueidentifier
SET @ScheduleItem2ID = '1059E0B4-9665-4fb0-BE31-75C8F062610A'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem2ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem2ID,
		'Lorem Ipsum and You',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'11/17/2009 11:00:00',
		'11/17/2009 12:30:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

DECLARE @ScheduleItem3ID uniqueidentifier
SET @ScheduleItem3ID = 'C83E9873-EB30-4bcc-A877-51F0FFE325E9'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem3ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem3ID,
		'Lorem Ipsum and You (II)',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'11/18/2009 11:00:00',
		'11/18/2009 12:30:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

DECLARE @ScheduleItem4ID uniqueidentifier
SET @ScheduleItem4ID = 'C133D75F-C189-4b18-9C92-83BEC3496E10'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem4ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem4ID,
		'Lorem Ipsum and You (III)',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'11/19/2009 11:00:00',
		'11/19/2009 12:30:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

DECLARE @ScheduleItem5ID uniqueidentifier
SET @ScheduleItem5ID = '48948EE0-02D7-4d86-B3FD-CB380A65E20A'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem5ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem5ID,
		'Lorem Ipsum and You (IV)',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'07/11/2009 13:00:00',
		'07/11/2009 14:00:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

DECLARE @ScheduleItem6ID uniqueidentifier
SET @ScheduleItem6ID = 'A12D7E0E-7CF8-4f49-9C7A-F538AC815C90'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem6ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem6ID,
		'Lorem Ipsum and You (V)',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'07/11/2009 13:00:00',
		'07/11/2009 14:00:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

DECLARE @ScheduleItem7ID uniqueidentifier
SET @ScheduleItem7ID = 'C97E2CE1-3087-498a-B894-E49FC1A9EF36'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem7ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem7ID,
		'Lorem Ipsum and You (VI)',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'07/11/2009 13:00:00',
		'07/11/2009 14:00:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

DECLARE @ScheduleItem8ID uniqueidentifier
SET @ScheduleItem8ID = 'BDD354BD-2B31-4fb9-8BDF-A75A1E6FEE7E'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem8ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem8ID,
		'Lorem Ipsum and You (VII)',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'07/11/2009 13:00:00',
		'07/11/2009 14:00:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

DECLARE @ScheduleItem9ID uniqueidentifier
SET @ScheduleItem9ID = '2EC18FB8-B9F6-48d1-8C02-FB62362CF56C'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem9ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem9ID,
		'Lorem Ipsum and You (VIII)',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'3125',
		'Session',
		'LI007',
		'07/11/2009 13:00:00',
		'07/11/2009 14:00:00',
		'LoremIpsumAndYou',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)
	
DECLARE @ScheduleItem10ID uniqueidentifier
SET @ScheduleItem10ID = 'BCB8CEAA-27B5-4fb4-A308-DD96E433BA5C'

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItem WHERE ScheduleItemID = @ScheduleItem10ID)
	INSERT INTO
		oxite_Conferences_ScheduleItem
	(
		EventID,
		ScheduleItemID,
		Title,
		Body,
		Location,
		Type,
		Code,
		StartTime,
		EndTime,
		Slug,
		CreatedDate,
		ModifiedDate
	)
	VALUES
	(
		@EventID,
		@ScheduleItem10ID,
		'Lorem Ipsum and You - A Workshop',
		'You will really like this lipsum - lorems the ipsum to sit the dolor amet. Si vetaet poetas nonummy volgus ipsum lorem, consequeter ut nihil anterferat illis compareat.',
		'TBD',
		'Workshop',
		'LI007',
		'11/16/2009 10:00:00',
		'11/16/2009 12:00:00',
		'LoremIpsumAndYouWorkshop',
		'04/01/2009 8:00:01',
		'04/01/2009 8:00:01'
	)

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem1ID AND SpeakerID = @Speaker1ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem1ID,
		@Speaker1ID
	)

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemFlag WHERE ScheduleItemID = @ScheduleItem1ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemFlag
	(
		ScheduleItemID,
		FlagType
	)
	VALUES
	(
		@ScheduleItem1ID,
		'featured'
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem2ID AND SpeakerID = @Speaker1ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem2ID,
		@Speaker1ID
	)
	
--IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem2ID AND SpeakerID = @Speaker2ID)
	--INSERT INTO
		--oxite_Conferences_ScheduleItemSpeakerRelationship
	--(
		--ScheduleItemID,
		--SpeakerID
	--)
	--VALUES
	--(
		--@ScheduleItem2ID,
		--@Speaker2ID
	--)
	
--IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem3ID AND SpeakerID = @Speaker2ID)
	--INSERT INTO
		--oxite_Conferences_ScheduleItemSpeakerRelationship
	--(
		--ScheduleItemID,
		--SpeakerID
	--)
	--VALUES
	--(
		--@ScheduleItem3ID,
		--@Speaker2ID
	--)

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemFlag WHERE ScheduleItemID = @ScheduleItem3ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemFlag
	(
		ScheduleItemID,
		FlagType
	)
	VALUES
	(
		@ScheduleItem3ID,
		'featured'
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem4ID AND SpeakerID = @Speaker2ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem4ID,
		@Speaker2ID
	)

IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemFlag WHERE ScheduleItemID = @ScheduleItem4ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemFlag
	(
		ScheduleItemID,
		FlagType
	)
	VALUES
	(
		@ScheduleItem4ID,
		'featured'
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem5ID AND SpeakerID = @Speaker2ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem5ID,
		@Speaker2ID
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem6ID AND SpeakerID = @Speaker2ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem6ID,
		@Speaker2ID
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem7ID AND SpeakerID = @Speaker2ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem7ID,
		@Speaker2ID
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem8ID AND SpeakerID = @Speaker2ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem8ID,
		@Speaker2ID
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem9ID AND SpeakerID = @Speaker2ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem9ID,
		@Speaker2ID
	)
	
IF NOT EXISTS(SELECT * FROM oxite_Conferences_ScheduleItemSpeakerRelationship WHERE ScheduleItemID = @ScheduleItem10ID AND SpeakerID = @Speaker1ID)
	INSERT INTO
		oxite_Conferences_ScheduleItemSpeakerRelationship
	(
		ScheduleItemID,
		SpeakerID
	)
	VALUES
	(
		@ScheduleItem10ID,
		@Speaker1ID
	)
