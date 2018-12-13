# Microsoft Oxite

Original website: [http://www.codeplex.com/oxite](http://www.codeplex.com/oxite), current jump to [https://archive.codeplex.com/?p=oxite](https://archive.codeplex.com/?p=oxite).

**Oxite is an open source, web standards compliant, blog engine built on ASP.NET MVC.**

Update: Note that while Oxite is still being actively worked on for our own sites ([http://live.visitmix.com](http://live.visitmix.com/), [http://microsoftpdc.com](http://microsoftpdc.com/) and others), development of this project as a standalone blog engine is not being done. You should check out [http://orchardproject.net/](http://orchardproject.net/) as it is actively working to provide a set of features that encompasses and exceeds the original Oxite plan.

## Features

* [Modules](https://oxite.codeplex.com/wikipage?title=Modules&referringTitle=Home)
* Commenting
* Comment moderation
* Tagging
* Content pages
* RSS and ATOM feeds everywhere (All up, Blog, Tag, Comment, etc)
* Trackbacks/Pingbacks
* Email subscriptions
* Sitemaps
* Search
* [Skinning](https://oxite.codeplex.com/wikipage?title=Skinning&referringTitle=Home)
* [Plugins](https://oxite.codeplex.com/wikipage?title=Plugins&referringTitle=Home)
* Metaweblog API support (Windows Live Writer, etc)
* Background services
* Web admin
  * All up dashboard
  * Add/Edit Posts/Pages
  * Manage site settings
  * Add/Edit Areas (Blogs)
  * BlogML Import
* Runs on SQL Server 2005 and above
* SQL Scripts to create/update your database
* Multiple sites stored in a single database
* Dependency Injection so parts of Oxite can be replaced

# Getting Started With Oxite

* [Setting Up Oxite](https://oxite.codeplex.com/wikipage?title=gettingstarted&referringTitle=Home)
* [How to Skin Oxite](https://oxite.codeplex.com/wikipage?title=skinning&referringTitle=Home)
* [Available Skins for Oxite](https://oxite.codeplex.com/wikipage?title=skins&referringTitle=Home)
* [Oxite Architecture](https://oxite.codeplex.com/wikipage?title=architecture&referringTitle=Home)
* [Frequently Asked Questions](https://oxite.codeplex.com/wikipage?title=FAQ&referringTitle=Home)
* [List of sites running Oxite](https://oxite.codeplex.com/wikipage?title=oxitesites&referringTitle=Home)
* [Oxite on VisitMix.com](http://visitmix.com/lab/oxite)
* [ASP.NET MVC](http://asp.net/mvc)

## What this is

This is a simple blog engine written using ASP.NET MVC, and is designed with a few main goals:

1. To exist as a base for our visitmix.com site and for our personal blogs (and for the blogs of other folks as well!)
2. To provide an example of 'core blog functionality' in a reusable fashion. Blogs are simple and well understood by many developers, but the set of basic functions that a blog needs to implement (trackbacks, rss, comments, etc.) are fairly complex. Hopefully this code helps.
3. To provide real-world code written using ASP.NET MVC that produces both valid and semantically correct markup

Oxite supports all the features we consider essential to a blog engine, including the MetaWebLog API (to allow you to use LiveWriter or similar tools to add/edit your posts), trackbacks, pingbacks, Sitemaps (for search engines), RSS and ATOM feeds (at the site, blog, tag and post level ... plus feeds of all new comments... great for the site owner), tags, integrated search, web based admin features (including editing posts, your site settings, etc.), email subscriptions for new comments, basic support to publish 'pages' (non-blog content) and more.

This code is the foundation of a real project of ours, [MIX Online](http://visitmix.com/). We also created this project to be the base of our own personal blogs as well so you'll probably see our blogs on the [list of sites running Oxite](https://oxite.codeplex.com/wikipage?title=oxitesites&referringTitle=Home) soon.

There are many different ways to architect and implement a site using ASP.NET MVC, and this isn't supposed to be the reference application for ASP.NET MVC. For that, check out [Rob Conery's MVC Storefront application](http://blog.wekeroad.com/mvc-storefront/) (there are others, [start here](http://asp.net/mvc)). Our team has read a great deal of the available guidance and has worked hard to produce an application that follows many of the best practices recommended by the ALT.NET community around ASP.NET MVC.

## What this isn't

1. This isn't a finished product. It has no install, it has no documentation. It is our code, exactly as we are using it. If you'd like something that is farther along and has all the support, documentation and handy installation features you'd expect from a finished product, then you might want to check out [BlogEngine.NET](http://www.codeplex.com/blogengine), [SubText](http://subtextproject.com/) or [dasBlog](http://www.codeplex.com/dasBlog) ... we are fans of all of these blogging engines... none of which use MVC at the moment but are far more 'finished'.
2. [BlogSvc](http://www.codeplex.com/blogsvc) is a .NET, ASP.NET MVC blogging engine that a few people have told us about. We haven't looked at it, but if you are looking for an ASP.NET MVC based blogging engine then you may wish to check it out.
3. This isn't for non-developers. This follows somewhat from the previous point, but to be clear... if you aren't familar with a web.config and with some database poking, then this isn't the project for you. You might have more luck with the other blog engines mentioned in the previous point, but for a non-dev I'd probably suggest you check out [Graffiti](http://graffiticms.com/).

## About us

Oxite is a project built by the team behind [Channel 9](http://channel9.msdn.com/) (and [Channel 8](http://channel8.msdn.com/), [Channel 10](http://on10.net/), [TechNet Edge](http://edge.technet.com/), [Mix Online](http://visitmix.com/)):

* [Erik Porter](http://erikporter.com/),
* [Nathan Heskew](http://nathan.heskew.com/),
* [Mike Sampson](http://sampy.com/) and
* [Duncan Mackenzie](http://duncanmackenzie.net/)

You can find out more about our team and our projects in [our various posts and videos on Channel 9](http://channel9.msdn.com/tags/evnet/).