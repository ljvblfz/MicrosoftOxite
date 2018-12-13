//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Oxite(this UrlHelper urlHelper)
        {
            return "http://oxite.net";
        }

        public static string AbsolutePath(this UrlHelper urlHelper, string relativeUrl)
        {
            Uri url = urlHelper.RequestContext.HttpContext.Request.Url;
            UriBuilder uriBuilder = new UriBuilder(url.Scheme, url.Host, url.Port) { Path = relativeUrl };

            string path = uriBuilder.Uri.ToString();

            if (path.EndsWith("/"))
                path = path.Substring(0, path.Length - 1);

            return path;
        }

        public static string AppPath(this UrlHelper urlHelper, string relativePath)
        {
            if (relativePath == null) return null;

            if (relativePath.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) || relativePath.StartsWith("https://", StringComparison.InvariantCultureIgnoreCase))
                return relativePath;

            return VirtualPathUtility.ToAbsolute(relativePath, urlHelper.RequestContext.HttpContext.Request.ApplicationPath);
        }

        public static string CssPath(this UrlHelper urlHelper, string relativePath, ViewContext viewContext)
        {
            string path = relativePath;

            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
                path = "/" + path;

            urlHelper.FilePath(viewContext, (ve, p) => ve.FindStylesFile(p), ref path);

            return path;
        }

        public static string ImagePath(this UrlHelper urlHelper, string relativePath, ViewContext viewContext)
        {
            string path = relativePath;

            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
                path = "/" + path;

            urlHelper.FilePath(viewContext, (ve, p) => ve.FindImageFile(p), ref path);

            return path;
        }

        public static string ScriptPath(this UrlHelper urlHelper, string relativePath, ViewContext viewContext)
        {
            string path = relativePath;

            if (!string.IsNullOrEmpty(path) && !path.StartsWith("/"))
                path = "/" + path;

            urlHelper.FilePath(viewContext, (ve, p) => ve.FindScriptsFile(p), ref path);

            return path;
        }

        internal static void FilePath(this UrlHelper urlHelper, ViewContext viewContext, Func<IOxiteViewEngine, string, FileEngineResult> findFile, ref string path)
        {
            List<string> searchedLocations = new List<string>(50);

            foreach (IOxiteViewEngine viewEngine in (IEnumerable<IOxiteViewEngine>)viewContext.ViewData["OxiteViewEngines"])
            {
                FileEngineResult result = findFile(viewEngine, path);

                if (result.SearchedLocations.Count() > 0)
                    searchedLocations.AddRange(result.SearchedLocations);
                else
                {
                    path = urlHelper.AppPath(result.FilePath);
                    searchedLocations.Clear();

                    break;
                }
            }

            if (searchedLocations.Count > 0)
            {
                if ((bool)viewContext.ViewData["Debug"])
                {
                    StringBuilder locationsText = new StringBuilder();

                    foreach (string location in searchedLocations)
                    {
                        locationsText.AppendLine();
                        locationsText.Append(location);
                    }

                    throw new InvalidOperationException(string.Format("The file '{0}' could not be found. The following locations were searched:{1}", path, locationsText));
                }
                path = urlHelper.AppPath(searchedLocations.ElementAt(0));
            }
        }

        public static string Home(this UrlHelper urlHelper)
        {
            return urlHelper.AppPath("~/");
        }

        public static string OpenSearch(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("OpenSearch");
        }

        public static string OpenSearchOSDX(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("OpenSearchOSDX");
        }

        public static string SiteMapIndex(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("SiteMapIndex");
        }

        public static string SiteMap(this UrlHelper urlHelper, int year, int month)
        {
            return urlHelper.RouteUrl("SiteMap", new { year, month });
        }

        public static string ComputeEmailHash(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("ComputeEmailHash");
        }

        public static string GetDateTime(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("GetDateTime");
        }

        public static string Site(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Site");
        }

        public static string ManageSite(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("ManageSite");
        }

        public static string ManageUsers(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("ManageUsers");
        }

        public static string UserChangePassword(this UrlHelper urlHelper, UserAuthenticated user)
        {
            return urlHelper.RouteUrl("UserChangePassword", new { userName = user.Name });
        }

        public static string UserRoles(this UrlHelper urlHelper, UserAuthenticated user)
        {
            return urlHelper.RouteUrl("UserRoles", new { userName = user.Name });
        }

        public static string UserFind(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("UserFind");
        }

        public static string UserAdd(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("UserAdd");
        }

        public static string UserEdit(this UrlHelper urlHelper, UserAuthenticated user)
        {
            return urlHelper.RouteUrl("UserEdit", new { userName = user.Name });
        }

        public static string UserRemove(this UrlHelper urlHelper, UserAuthenticated user)
        {
            return urlHelper.RouteUrl("UserRemove", new { userName = user.Name });
        }

        public static string RoleFind(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("RoleFind");
        }

        public static string RoleAdd(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("RoleAdd");
        }

        public static string RoleEdit(this UrlHelper urlHelper, Role role)
        {
            return urlHelper.RouteUrl("RoleEdit", new { roleName = role.Name });
        }

        public static string RoleRemove(this UrlHelper urlHelper, Role role)
        {
            return urlHelper.RouteUrl("RoleRemove", new { roleName = role.Name });
        }

        public static string Admin(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Admin");
        }

        public static string AdminUser(this UrlHelper urlHelper, UserAuthenticated user)
        {
            return urlHelper.RouteUrl("AdminUsersEdit", new { userID = user.ID });
        }

        public static string AdminUserChangePassword(this UrlHelper urlHelper, UserAuthenticated user)
        {
            return urlHelper.RouteUrl("AdminUsersChangePassword", new { userID = user.ID });
        }

        public static string CompressUrl(this UrlHelper urlHelper, string absoluteUrlEncoded)
        {
            string cacheKey = "tinyurl:" + absoluteUrlEncoded.ToLower();
            Cache cache = urlHelper.RequestContext.HttpContext.Cache;
            string url = (string)cache[cacheKey];

            //return absoluteUrlEncoded;

            if (string.IsNullOrEmpty(url))
            {
                try
                {
                    const string urlToSendTo = "http://is.gd/api.php?longurl={0}";
                    WebClient wc = new WebClient();

                    url = wc.DownloadString(string.Format(urlToSendTo, absoluteUrlEncoded));

                    cache.Add(cacheKey, url, null, DateTime.Now.AddMonths(1), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
                }
                catch
                {
                    url = absoluteUrlEncoded;
                }
            }

            return url;
        }
    }
}
