CREATE TABLE [dbo].[oxite_Site]
(
[SiteID] [uniqueidentifier] NOT NULL,
[SiteHost] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SiteName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SiteDisplayName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SiteDescription] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LanguageDefault] [varchar] (8) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TimeZoneOffset] [float] NOT NULL,
[PageTitleSeparator] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[FavIconUrl] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CommentStateDefault] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[IncludeOpenSearch] [bit] NOT NULL,
[AuthorAutoSubscribe] [bit] NOT NULL,
[PostEditTimeout] [smallint] NOT NULL,
[GravatarDefault] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SkinsPath] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SkinsScriptsPath] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[SkinsStylesPath] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Skin] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AdminSkin] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ServiceRetryCountDefault] [tinyint] NOT NULL,
[HasMultipleBlogs] [bit] NOT NULL,
[RouteUrlPrefix] [varchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CommentingDisabled] [bit] NOT NULL,
[PluginsPath] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]


