//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using System.Web.Routing;
using System.Web.Mvc;
using Oxite.Models;
using Oxite.Routing;
using Oxite.Services;

namespace Oxite.Infrastructure
{
    public class OxiteRegisterRoutes : IRegisterRoutes
    {
        private RouteCollection routes;
        private readonly AppSettingsHelper appSettings;
        private readonly Site site;
        private readonly IAreaService areaService;
        private readonly IRouteModifier routeModifier;

        public OxiteRegisterRoutes(AppSettingsHelper appSettings, Site site, IAreaService areaService, IRouteModifier routeModifier)
        {
            this.appSettings = appSettings;
            this.site = site;
            this.areaService = areaService;
            this.routeModifier = routeModifier;
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            this.routes = routes;

            string[] areas = areaService.GetAreas().Select(a => a.Name).ToArray();
            string areasConstraint =
                areas != null && areas.Length > 0
                    ? areas.Length > 1
                        ? string.Format("({0})", string.Join("|", areas)) : areas[0]
                    : "";

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string[] controllerNamespaces = appSettings.GetStringArray("ControllerNamespaces", ",", null);

            MapRoute(
                "RemoveComment",
                "Admin/{areaName}/{slug}/RemoveComment",
                new { controller = "Comment", action = "Remove", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "ApproveComment",
                "Admin/{areaName}/{slug}/Approve",
                new { controller = "Comment", action = "Approve", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "AddPostToSite",
                "Admin/AddPost",
                new { controller = "Post", action = "ItemAdd", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            MapRoute(
                "AddPostToArea",
                "Admin/{areaName}/Add",
                new { controller = "Post", action = "ItemAdd", validateAntiForgeryToken = true },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            MapRoute(
                "EditPost",
                "Admin/{areaName}/{slug}/Edit",
                new { controller = "Post", action = "ItemEdit", validateAntiForgeryToken = true },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            MapRoute(
                "RemovePost",
                "Admin/{areaName}/{slug}/Remove",
                new { controller = "Post", action = "Remove", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "FilesByPost",
                "Admin/{areaName}/{slug}/Files",
                new { controller = "File", action = "ListByPost" },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            MapRoute(
                "AddFileContentToPost",
                "Admin/{areaName}/{slug}/AddFileContent",
                new { controller = "File", action = "AddFileContentToPost", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "AddFileToPost",
                "Admin/{areaName}/{slug}/AddFile",
                new { controller = "File", action = "AddFileToPost", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "EditFileOnPost",
                "Admin/{areaName}/{slug}/EditFileOnPost",
                new { controller = "File", action = "EditFileOnPost", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "RemoveFileFromPost",
                "Admin/{areaName}/{slug}/RemoveFile",
                new { controller = "File", action = "RemoveFileFromPost", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "ManageSite",
                "Admin#manageSite",
                new { controller = "Site", action = "Dashboard" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            MapRoute(
                "ManageAreas",
                "Admin#manageAreas",
                new { controller = "Site", action = "Dashboard" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            MapRoute(
                "ManageUsers",
                "Admin#manageUsers",
                new { controller = "Site", action = "Dashboard" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            MapRoute(
                "Site",
                "Admin/Setup",
                new { controller = "Site", action = "Item", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            MapRoute(
                "Plugins",
                "Admin/Plugins",
                new { controller = "Plugin", action = "List" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "PluginsInstall",
                "Admin/Plugins/Install",
                new { controller = "Plugin", action = "Install" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "Plugin",
                "Admin/Plugins/{pluginID}",
                new { controller = "Plugin", action = "Item" },
                new { pluginID = new IsGuid() },
                controllerNamespaces
                );

            MapRoute(
                "Admin",
                "Admin",
                new { controller = "Site", action = "Dashboard" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "PostsWithDrafts",
                "Admin/Posts",
                new { controller = "Post", action = "ListWithDrafts" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "PageOfPostsWithDrafts",
                "Admin/Posts/page{pageNumber}",
                new { controller = "Post", action = "ListWithDrafts" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            MapRoute(
                "AllComments",
                "Admin/Comments",
                new { controller = "Comment", action = "ListForAdmin" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "PageOfAllComments",
                "Admin/Comments/page{pageNumber}",
                new { controller = "Comment", action = "ListForAdmin" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            MapRoute(
                "AllCommentsPermalink",
                "Admin/Comments#{comment}",
                null,
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            MapRoute(
                "AreaFind",
                "Admin/Areas",
                new { controller = "Area", action = "Find", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            MapRoute(
                "AreaAdd",
                "Admin/Areas/Add",
                new { controller = "Area", action = "ItemEdit", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            MapRoute(
                "AreaEdit",
                "Admin/Areas/{areaID}/Edit",
                new { controller = "Area", action = "ItemEdit", validateAntiForgeryToken = true },
                new { areaID = new IsGuid() },
                controllerNamespaces
                );

            MapRoute(
                "BlogML",
                "Admin/{areaName}/BlogML",
                new { controller = "Area", action = "BlogML", validateAntiForgeryToken = true },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            MapRoute(
                "AddPageToSite",
                "Admin/AddPage",
                new { controller = "Page", action = "ItemAdd", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            MapRoute(
                "AddPageToPage",
                "Admin/{*pagePath}",
                new { controller = "Page", action = "ItemAdd", validateAntiForgeryToken = true },
                new { pagePath = new IsPageMode(PageMode.Add) },
                controllerNamespaces
                );

            MapRoute(
                "EditPage",
                "Admin/{*pagePath}",
                new { controller = "Page", action = "ItemEdit", validateAntiForgeryToken = true },
                new { pagePath = new IsPageMode(PageMode.Edit) },
                controllerNamespaces
                );

            MapRoute(
                "RemovePage",
                "Admin/{*pagePath}",
                new { controller = "Page", action = "Remove", validateAntiForgeryToken = true },
                new { pagePath = new IsPageMode(PageMode.Remove), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "Posts",
                "{dataFormat}",
                new { controller = "Post", action = "List", dataFormat = "" },
                new { dataFormat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "PageOfPosts",
                "page{pageNumber}",
                new { controller = "Post", action = "List" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            MapRoute(
                "Comments",
                "Comments/{dataFormat}",
                new { controller = "Comment", action = "List" },
                new { dataFormat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            if (site.IncludeOpenSearch)
            {
                MapRoute(
                    "OpenSearch",
                    "OpenSearch.xml",
                    new { controller = "Utility", action = "OpenSearch" },
                    null,
                    controllerNamespaces
                    );

                MapRoute(
                    "OpenSearchOSDX",
                    "OpenSearch.osdx",
                    new { controller = "Utility", action = "OpenSearchOSDX" },
                    null,
                    controllerNamespaces
                    );
            }

            MapRoute(
                "PostsBySearch",
                "Search/{dataFormat}",
                new { controller = "Post", action = "ListBySearch", dataFormat = "" },
                new { dataformat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "PageOfPostsBySearch",
                "Search/page{pageNumber}",
                new { controller = "Post", action = "ListBySearch" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            MapRoute(
                "PostsByArea",
                "{areaName}/{dataFormat}",
                new { controller = "Post", action = "ListByArea", dataFormat = "" },
                new { areaName = areasConstraint, dataFormat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "PageOfPostsByArea",
                "{areaName}/page{pageNumber}",
                new { controller = "Post", action = "ListByArea" },
                new { areaName = areasConstraint, pageNumber = new IsInt() },
                controllerNamespaces
                );

            MapRoute(
                "CommentsByArea",
                "{areaName}/Comments/{dataFormat}",
                new { controller = "Comment", action = "ListByArea" },
                new { areaName = areasConstraint, dataFormat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "PostsByAreaArchive",
                "{areaName}/Archive/{*archiveData}",
                new { controller = "Post", action = "ListByArchive", archiveData = ArchiveData.DefaultString },
                new { areaName = areasConstraint, archiveData = new IsArchiveData() },
                controllerNamespaces
                );

            routes.MapRoute(
                routeModifier,
                "PostsByAreaAndTag",
                "{areaName}/Tags/{tagName}/{dataFormat}",
                new { controller = "Post", action = "ListByAreaAndTag", dataFormat = "" },
                new { areaName = areasConstraint, dataFormat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            routes.MapRoute(
                routeModifier,
                "PageOfPostsByAreaAndTag",
                "{areaName}/Tags/{tagName}/page{pageNumber}",
                new { controller = "Post", action = "ListByAreaAndTag" },
                new { areaName = areasConstraint, pageNumber = new IsInt() },
                controllerNamespaces
                );

            routes.MapRoute(
                routeModifier,
                "TagsByArea",
                "{areaName}/Tags",
                new { controller = "Tag", action = "CloudForArea" },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            MapRoute(
                "Rsd",
                "RSD",
                new { controller = "Area", action = "Rsd" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "AreaRsd",
                "{areaName}/RSD",
                new { controller = "Area", action = "Rsd" },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            MapRoute(
                "PostCommentPermalink",
                "{areaName}/{slug}#{comment}",
                null,
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            MapRoute(
                "PostCommentForm",
                "{areaName}/{slug}#comment",
                null,
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            MapRoute(
                "AddCommentToPostAsUser",
                "{areaName}/{slug}",
                new { controller = "Post", action = "Item", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST"), authenticated = new IsAuthenticated() },
                controllerNamespaces
                );

            MapRoute(
                "AddCommentToPost",
                "{areaName}/{slug}",
                new { controller = "Post", action = "Item" },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            MapRoute(
                "Post",
                "{areaName}/{slug}/{dataFormat}",
                new { controller = "Post", action = "Item", dataFormat = "" },
                new { areaName = areasConstraint, dataformat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "CommentsByPost",
                "{areaName}/{slug}/Comments/{dataFormat}",
                new { controller = "Comment", action = "ListByPost" },
                new { areaName = areasConstraint, dataformat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "ComputeHash",
                "ComputeHash",
                new { controller = "Utility", action = "ComputeHash" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "SignIn",
                "SignIn",
                new { controller = "User", action = "SignIn" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "SignOut",
                "SignOut",
                new { controller = "User", action = "SignOut" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "UserChangePassword",
                "Admin/ChangePassword",
                new { controller = "User", action = "ChangePassword", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            MapRoute(
                "Tags",
                "Tags",
                new { controller = "Tag", action = "Cloud" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "PostsByTag",
                "Tags/{tagName}/{dataFormat}",
                new { controller = "Post", action = "ListByTag", dataFormat = "" },
                new { dataFormat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "PageOfPostsByTag",
                "Tags/{tagName}/page{pageNumber}",
                new { controller = "Post", action = "ListByTag" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            MapRoute(
                "CommentsByTag",
                "Tags/{tagName}/Comments/{dataFormat}",
                new { controller = "Comment", action = "ListByTag" },
                new { dataFormat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            MapRoute(
                "PostsByArchive",
                "Archive/{*archiveData}",
                new { controller = "Post", action = "ListByArchive" },
                new { archiveData = new IsArchiveData() },
                controllerNamespaces
                );

            MapRoute(
                "Trackback",
                "{postID}/Trackback",
                new { controller = "Trackback", action = "Add" },
                new { postID = new IsGuid() },
                controllerNamespaces
                );

            MapRoute(
                "RobotsTxt",
                "robots.txt",
                new { controller = "Utility", action = "RobotsTxt" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "SiteMapIndex",
                "SiteMap",
                new { controller = "Utility", action = "SiteMapIndex" },
                null,
                controllerNamespaces
                );

            MapRoute(
                "SiteMap",
                "SiteMap/{year}/{month}",
                new { controller = "Utility", action = "SiteMap" },
                new
                {
                    year = new IsInt(DateTime.MinValue.Year, DateTime.MaxValue.Year),
                    month = new IsInt(DateTime.MinValue.Month, DateTime.MaxValue.Month)
                },
                controllerNamespaces
                );

            MapRoute(
                "PageAdd",
                "Add",
                new { controller = "Page", action = "Index", pagePath = string.Empty },
                null,
                controllerNamespaces
                );

            //INFO: (erikpo) This route must remain last
            MapRoute(
                "Page",
                "{*pagePath}",
                new { controller = "Page", action = "Item" },
                new { pagePath = new IsPagePath() },
                controllerNamespaces
                );
        }

        protected Route MapRoute(string name, string url, object defaults, object constraints, string[] namespaces)
        {
            return routes.MapRoute(routeModifier, name, url, defaults, constraints, namespaces);
        }
    }
}
