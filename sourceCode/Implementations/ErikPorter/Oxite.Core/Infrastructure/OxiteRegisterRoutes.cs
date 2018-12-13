//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Oxite.Models;
using Oxite.Routing;
using Oxite.Services;

namespace Oxite.Infrastructure
{
    public class OxiteRegisterRoutes : IRegisterRoutes
    {
        private IList<OxiteRoute> routes;
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

        public void RegisterRoutes(IList<OxiteRoute> routes)
        {
            this.routes = routes;

            string[] areas = areaService.GetAreas().Select(a => a.Name).ToArray();
            string areasConstraint =
                areas != null && areas.Length > 0
                    ? areas.Length > 1
                        ? string.Format("({0})", string.Join("|", areas)) : areas[0]
                    : "";

            string[] controllerNamespaces = appSettings.GetStringArray("ControllerNamespaces", ",", null);

            AddRoute(
                "RemoveComment",
                "Admin/{areaName}/{slug}/RemoveComment",
                new { controller = "Comment", action = "Remove", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "ApproveComment",
                "Admin/{areaName}/{slug}/Approve",
                new { controller = "Comment", action = "Approve", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "AddPostToSite",
                "Admin/AddPost",
                new { controller = "Post", action = "ItemAdd", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            AddRoute(
                "AddPostToArea",
                "Admin/{areaName}/Add",
                new { controller = "Post", action = "ItemAdd", validateAntiForgeryToken = true },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            AddRoute(
                "EditPost",
                "Admin/{areaName}/{slug}/Edit",
                new { controller = "Post", action = "ItemEdit", validateAntiForgeryToken = true },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            AddRoute(
                "RemovePost",
                "Admin/{areaName}/{slug}/Remove",
                new { controller = "Post", action = "Remove", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "FilesByPost",
                "Admin/{areaName}/{slug}/Files",
                new { controller = "File", action = "ListByPost" },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            AddRoute(
                "AddFileToPost",
                "Admin/{areaName}/{slug}/AddFile",
                new { controller = "File", action = "AddFileToPost", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "EditFileOnPost",
                "Admin/{areaName}/{slug}/EditFileOnPost",
                new { controller = "File", action = "AddFileToPost", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "RemoveFileFromPost",
                "Admin/{areaName}/{slug}/RemoveFile",
                new { controller = "File", action = "RemoveFileFromPost", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "ManageSite",
                "Admin#manageSite",
                new { controller = "Site", action = "Dashboard" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            AddRoute(
                "ManageAreas",
                "Admin#manageAreas",
                new { controller = "Site", action = "Dashboard" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            AddRoute(
                "ManageUsers",
                "Admin#manageUsers",
                new { controller = "Site", action = "Dashboard" },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            AddRoute(
                "Site",
                "Admin/Setup",
                new { controller = "Site", action = "Item", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            AddRoute(
                "Plugins",
                "Admin/Plugins",
                new { controller = "Plugin", action = "List" },
                null,
                controllerNamespaces
                );

            AddRoute(
                "PluginsInstall",
                "Admin/Plugins/Install",
                new { controller = "Plugin", action = "Install" },
                null,
                controllerNamespaces
                );

            AddRoute(
                "Plugin",
                "Admin/Plugins/{pluginID}",
                new { controller = "Plugin", action = "Item" },
                new { pluginID = new IsGuid() },
                controllerNamespaces
                );

            AddRoute(
                "Admin",
                "Admin",
                new { controller = "Site", action = "Dashboard" },
                null,
                controllerNamespaces
                );

            AddRoute(
                "PostsWithDrafts",
                "Admin/Posts",
                new { controller = "Post", action = "ListWithDrafts" },
                null,
                controllerNamespaces
                );

            AddRoute(
                "PageOfPostsWithDrafts",
                "Admin/Posts/page{pageNumber}",
                new { controller = "Post", action = "ListWithDrafts" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            AddRoute(
                "AllComments",
                "Admin/Comments",
                new { controller = "Comment", action = "ListForAdmin" },
                null,
                controllerNamespaces
                );

            AddRoute(
                "PageOfAllComments",
                "Admin/Comments/page{pageNumber}",
                new { controller = "Comment", action = "ListForAdmin" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            AddRoute(
                "AllCommentsPermalink",
                "Admin/Comments#{comment}",
                null,
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            AddRoute(
                "AreaFind",
                "Admin/Areas",
                new { controller = "Area", action = "Find", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            AddRoute(
                "AreaAdd",
                "Admin/Areas/Add",
                new { controller = "Area", action = "ItemEdit", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            AddRoute(
                "AreaEdit",
                "Admin/Areas/{areaID}/Edit",
                new { controller = "Area", action = "ItemEdit", validateAntiForgeryToken = true },
                new { areaID = new IsGuid() },
                controllerNamespaces
                );

            AddRoute(
                "BlogML",
                "Admin/{areaName}/BlogML",
                new { controller = "Area", action = "BlogML", validateAntiForgeryToken = true },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            AddRoute(
                "AddPageToSite",
                "Admin/AddPage",
                new { controller = "Page", action = "ItemAdd", validateAntiForgeryToken = true },
                null,
                controllerNamespaces
                );

            AddRoute(
                "AddPageToPage",
                "Admin/{*pagePath}",
                new { controller = "Page", action = "ItemAdd", validateAntiForgeryToken = true },
                new { pagePath = new IsPageMode(PageMode.Add) },
                controllerNamespaces
                );

            AddRoute(
                "EditPage",
                "Admin/{*pagePath}",
                new { controller = "Page", action = "ItemEdit", validateAntiForgeryToken = true },
                new { pagePath = new IsPageMode(PageMode.Edit) },
                controllerNamespaces
                );

            AddRoute(
                "RemovePage",
                "Admin/{*pagePath}",
                new { controller = "Page", action = "Remove", validateAntiForgeryToken = true },
                new { pagePath = new IsPageMode(PageMode.Remove), httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "Posts",
                "",
                new { controller = "Post", action = "List" },
                null,
                controllerNamespaces
                );

            AddRoute(
                "PostsByDataFormat",
                "{dataFormat}",
                new { controller = "Post", action = "List" },
                new { dataFormat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "PageOfPosts",
                "page{pageNumber}",
                new { controller = "Post", action = "List" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            AddRoute(
                "Comments",
                "Comments/{dataFormat}",
                new { controller = "Comment", action = "List" },
                new { dataFormat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            if (site.IncludeOpenSearch)
            {
                AddRoute(
                    "OpenSearch",
                    "OpenSearch.xml",
                    new { controller = "Utility", action = "OpenSearch", viewTracking = false },
                    null,
                    controllerNamespaces
                    );

                AddRoute(
                    "OpenSearchOSDX",
                    "OpenSearch.osdx",
                    new { controller = "Utility", action = "OpenSearchOSDX", viewTracking = false },
                    null,
                    controllerNamespaces
                    );
            }

            AddRoute(
                "PostsBySearch",
                "Search/{dataFormat}",
                new { controller = "Post", action = "ListBySearch", dataFormat = "" },
                new { dataformat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "PageOfPostsBySearch",
                "Search/page{pageNumber}",
                new { controller = "Post", action = "ListBySearch" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            AddRoute(
                "PostsByArea",
                "{areaName}/{dataFormat}",
                new { controller = "Post", action = "ListByArea", dataFormat = "" },
                new { areaName = areasConstraint, dataFormat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "PageOfPostsByArea",
                "{areaName}/page{pageNumber}",
                new { controller = "Post", action = "ListByArea" },
                new { areaName = areasConstraint, pageNumber = new IsInt() },
                controllerNamespaces
                );

            AddRoute(
                "CommentsByArea",
                "{areaName}/Comments/{dataFormat}",
                new { controller = "Comment", action = "ListByArea" },
                new { areaName = areasConstraint, dataFormat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "PostsByAreaArchive",
                "{areaName}/Archive/{*archiveData}",
                new { controller = "Post", action = "ListByArchive", archiveData = ArchiveData.DefaultString },
                new { areaName = areasConstraint, archiveData = new IsArchiveData() },
                controllerNamespaces
                );

            AddRoute(
                "Rsd",
                "RSD",
                new { controller = "Area", action = "Rsd", viewTracking = false },
                null,
                controllerNamespaces
                );

            AddRoute(
                "AreaRsd",
                "{areaName}/RSD",
                new { controller = "Area", action = "Rsd", viewTracking = false },
                new { areaName = areasConstraint },
                controllerNamespaces
                );

            AddRoute(
                "PostCommentPermalink",
                "{areaName}/{slug}#{comment}",
                new { viewTracking = false },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            AddRoute(
                "PostCommentForm",
                "{areaName}/{slug}#comment",
                new { viewTracking = false },
                new { routeDirection = new RouteDirectionConstraint(RouteDirection.UrlGeneration) },
                controllerNamespaces
                );

            AddRoute(
                "AddCommentToPostAsUser",
                "{areaName}/{slug}",
                new { controller = "Post", action = "Item", validateAntiForgeryToken = true },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST"), authenticated = new IsAuthenticated() },
                controllerNamespaces
                );

            AddRoute(
                "AddCommentToPost",
                "{areaName}/{slug}",
                new { controller = "Post", action = "Item" },
                new { areaName = areasConstraint, httpMethod = new HttpMethodConstraint("POST") },
                controllerNamespaces
                );

            AddRoute(
                "Post",
                "{areaName}/{slug}/{dataFormat}",
                new { controller = "Post", action = "Item", dataFormat = "" },
                new { areaName = areasConstraint, dataformat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "CommentsByPost",
                "{areaName}/{slug}/Comments/{dataFormat}",
                new { controller = "Comment", action = "ListByPost" },
                new { areaName = areasConstraint, dataformat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "ComputeHash",
                "ComputeHash",
                new { controller = "Utility", action = "ComputeHash", viewTracking = false },
                null,
                controllerNamespaces
                );

            AddRoute(
                "SignIn",
                "SignIn",
                new { controller = "User", action = "SignIn", viewTracking = false },
                null,
                controllerNamespaces
                );

            AddRoute(
                "SignOut",
                "SignOut",
                new { controller = "User", action = "SignOut", viewTracking = false },
                null,
                controllerNamespaces
                );

            AddRoute(
                "UserChangePassword",
                "Admin/ChangePassword",
                new { controller = "User", action = "ChangePassword", validateAntiForgeryToken = true, viewTracking = false },
                null,
                controllerNamespaces
                );

            AddRoute(
                "Tags",
                "Tags",
                new { controller = "Tag", action = "Cloud" },
                null,
                controllerNamespaces
                );

            AddRoute(
                "PostsByTag",
                "Tags/{tagName}/{dataFormat}",
                new { controller = "Post", action = "ListByTag", dataFormat = "" },
                new { dataFormat = "(|RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "PageOfPostsByTag",
                "Tags/{tagName}/page{pageNumber}",
                new { controller = "Post", action = "ListByTag" },
                new { pageNumber = new IsInt() },
                controllerNamespaces
                );

            AddRoute(
                "CommentsByTag",
                "Tags/{tagName}/Comments/{dataFormat}",
                new { controller = "Comment", action = "ListByTag" },
                new { dataFormat = "(RSS|ATOM)" },
                controllerNamespaces
                );

            AddRoute(
                "PostsByArchive",
                "Archive/{*archiveData}",
                new { controller = "Post", action = "ListByArchive" },
                new { archiveData = new IsArchiveData() },
                controllerNamespaces
                );

            AddRoute(
                "Trackback",
                "{postID}/Trackback",
                new { controller = "Trackback", action = "Add", viewTracking = false },
                new { postID = new IsGuid() },
                controllerNamespaces
                );

            AddRoute(
                "RobotsTxt",
                "robots.txt",
                new { controller = "Utility", action = "RobotsTxt", viewTracking = false },
                null,
                controllerNamespaces
                );

            AddRoute(
                "SiteMapIndex",
                "SiteMap",
                new { controller = "Utility", action = "SiteMapIndex", viewTracking = false },
                null,
                controllerNamespaces
                );

            AddRoute(
                "SiteMap",
                "SiteMap/{year}/{month}",
                new { controller = "Utility", action = "SiteMap", viewTracking = false },
                new
                {
                    year = new IsInt(DateTime.MinValue.Year, DateTime.MaxValue.Year),
                    month = new IsInt(DateTime.MinValue.Month, DateTime.MaxValue.Month)
                },
                controllerNamespaces
                );

            AddRoute(
                "PageAdd",
                "Add",
                new { controller = "Page", action = "Index", pagePath = string.Empty },
                null,
                controllerNamespaces
                );

            //INFO: (erikpo) This route must remain last
            AddRoute(
                "Page",
                "{*pagePath}",
                new { controller = "Page", action = "Item", viewTracking = false },
                null,
                controllerNamespaces
                );
        }

        protected void AddRoute(string name, string url, object defaults, object constraints, string[] namespaces)
        {
            routes.AddRoute(routeModifier, name, url, defaults, constraints, namespaces);
        }
    }
}
