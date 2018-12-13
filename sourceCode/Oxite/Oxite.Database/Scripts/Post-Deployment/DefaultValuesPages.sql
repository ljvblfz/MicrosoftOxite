DECLARE @SiteID uniqueidentifier
SET @SiteID = '4F36436B-0782-4a94-BB4C-FD3916734C03'
DECLARE @User1ID uniqueidentifier
SET @User1ID = '655D3498-03DB-4075-A80E-6514EC6BB6E2'
DECLARE @PageID uniqueidentifier, @ContentItemID uniqueidentifier
SET @PageID = 'D2D69195-F49A-4f68-AA3C-D7B4CF69F695'
SET @ContentItemID = '3B6322C8-59AB-46ee-8A10-68A8513DEAD5'

IF NOT EXISTS(SELECT * FROM oxite_CMS_Page WHERE PageID = @PageID)
BEGIN
	INSERT INTO oxite_CMS_Page
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
		'',
		'About',
		'Sample about page for Oxite',
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
	<li>Adding UI for managing the creation of new blogs, setting up users and user permissions, etc. &nbsp;If you decided to use Oxite to host many blogs together on one site, or to use the same Oxite database to run many sites (yes, it can do both of those!) then it would be nice to have some UI for managing all those contributors.</li>
	<li>And whatever great idea you have!</li>
</ul>
<h3>Getting the code, reporting bugs, and contributing to this project</h3>
<p><a href="http://codeplex.com/Oxite">Oxite is hosted on CodePlex</a>, so you can grab the latest code from there (you can <a href="http://www.codeplex.com/oxite/SourceControl/ListDownloadableCommits.aspx">see all of our check-ins</a> and also <a href="http://www.codeplex.com/oxite/Release/ProjectReleases.aspx">specific releases</a> when we feel like significant changes have been made), read discussions, file bugs and even submit suggestions for changes. &nbsp;If you''ve made some code changes that you feel should make it back into the Oxite code, then CodePlex is the place to tell us about it!</p>
<h3>About us</h3>
<p>Oxite is a project built by the team behind <a href="http://channel9.msdn.com/">Channel 9</a> (and <a href="http://channel8.msdn.com/">Channel 8</a>, <a href="http://on10.net/">Channel 10</a>, <a href="http://edge.technet.com/">TechNet Edge</a>, <a href="http://visitmix.com/">Mix Online</a>): Erik Porter, Nathan Heskew, Mike Sampson and Duncan Mackenzie. &nbsp;You can find out more about our team and our projects in our <a href="http://channel9.msdn.com/tags/evnet/">various posts and videos on Channel 9</a>.</p>
<p><a href="http://validator.w3.org/check?uri=referer"><img src="/Content/images/valid-xhtml10-blue.png" alt="Valid XHTML 1.0 Strict" height="31" width="88" /></a> <a href="http://jigsaw.w3.org/css-validator/"><img style="border:0;width:88px;height:31px" src="/Content/images/vcss-blue.gif" alt="Valid CSS!" /></a> <a href="http://validator.w3.org/feed/check.cgi"><img src="/Content/images/valid-rss.png" alt="[Valid RSS]" title="Validate my RSS feed" /></a></p>',
		1,
		@User1ID,
		getUtcDate(),
		getUtcDate()
	)
END