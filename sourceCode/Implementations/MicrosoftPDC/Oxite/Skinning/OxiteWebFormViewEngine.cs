//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Oxite.Infrastructure;

namespace Oxite.Skinning
{
    public class OxiteWebFormViewEngine : WebFormViewEngine, IOxiteViewEngine
    {
        // format is ":ViewCacheEntry:{cacheType}:{prefix}:{name}:{controllerName}:{skinName}"
        private const string CacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}";
        private const string CacheKeyPrefixMaster = "Master";
        private const string CacheKeyPrefixPartial = "Partial";
        private const string CacheKeyPrefixView = "View";

        private static readonly string[] _emptyLocations = new string[0];

        private string rootPath;

        #region IOxiteViewEngine Members

        public void SetRootPath(string rootPath)
        {
            SetRootPath(rootPath, false);
        }

        public void SetRootPath(string rootPath, bool onlySearchRootPathForPartialViews)
        {
            if (rootPath.EndsWith("/"))
                rootPath = rootPath.Substring(0, rootPath.Length - 1);
            bool layer = false;
            bool hacks = false;

            layer = rootPath.Contains("Layers");
            hacks = rootPath.Contains("IE6");


            if (!layer && !hacks)
            {
                MasterLocationFormats = new []
                {
                    rootPath + "/Views/{1}/{0}.master",
                    rootPath + "/Views/Shared/{0}.master",
                    // Conference default layer
                    "~/Skins/Conferences" + "/Views/{1}/{0}.master",
                    "~/Skins/Conferences" + "/Views/Shared/{0}.master"
                };
                ViewLocationFormats = new[]
                                          {
                                              rootPath + "/Views/{1}/{0}.aspx",
                                              rootPath + "/Views/Shared/{0}.aspx",
                                              // Conference default layer
                                              "~/Skins/Conferences" + "/Views/{1}/{0}.aspx",
                                              "~/Skins/Conferences" + "/Views/Shared/{0}.aspx"
                                          };
                PartialViewLocationFormats = !onlySearchRootPathForPartialViews
                    ? new[]
                    {
                        rootPath + "/Views/{1}/{0}.ascx",
                        rootPath + "/Views/Shared/{0}.ascx",
                        // Conference default layer
                        "~/Skins/Conferences" + "/Views/{1}/{0}.ascx",
                        "~/Skins/Conferences" + "/Views/Shared/{0}.ascx",
                    }
                    : new[] { rootPath + "/{0}.ascx" };
            }
            else
            {
                MasterLocationFormats = new[]
                {
                    rootPath + "/Views/{1}/{0}.master",
                    rootPath + "/Views/Shared/{0}.master"
                };
                ViewLocationFormats = new[]
                  {
                      rootPath + "/Views/{1}/{0}.aspx",
                      rootPath + "/Views/Shared/{0}.aspx"
                  };
                PartialViewLocationFormats = !onlySearchRootPathForPartialViews
                    ? new[]
                    {
                        rootPath + "/Views/{1}/{0}.ascx",
                        rootPath + "/Views/Shared/{0}.ascx"
                    }
                    : new[] { rootPath + "/{0}.ascx" };


                if (layer)
                {
                    rootPath = rootPath.Substring(0, rootPath.IndexOf("/Layers"));
                }
                else 
                {
                    rootPath = rootPath.Substring(0, rootPath.IndexOf("/Hacks"));
                }

            }



            this.rootPath = rootPath;
        }

        public string Skin { get; set; }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (String.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("Argument cannot be null or empty", "viewName");
            }

            string[] viewLocationsSearched;
            string[] masterLocationsSearched;

            var controllerName = controllerContext.RouteData.GetRequiredString("controller");

            // This isn't an ideal way to resolve the master name, but it is preferable to creating
            // new ViewResult overloads or passing master names into each controller action. Feel
            // free to revisit this hash map strategy if the number of .Master pages grows
            masterName = ResolveMasterName(masterName, viewName, controllerName);

            var viewPath = GetPath(controllerContext, ViewLocationFormats,
                                   "ViewLocationFormats", viewName, controllerName, CacheKeyPrefixView, useCache,
                                   out viewLocationsSearched);

            var masterPath = GetPath(controllerContext, MasterLocationFormats,
                                     "MasterLocationFormats", masterName, controllerName, CacheKeyPrefixMaster,
                                     useCache, out masterLocationsSearched);

            if (String.IsNullOrEmpty(viewPath) || (String.IsNullOrEmpty(masterPath) && !String.IsNullOrEmpty(masterName)))
            {
                return new ViewEngineResult(viewLocationsSearched.Union(masterLocationsSearched));
            }

            return new ViewEngineResult(CreateView(controllerContext, viewPath, masterPath), this);
        }

        // todo re-evaluate if master name resolution becomes problematic
        private static string ResolveMasterName(string masterName, string viewName, string controllerName)
        {
            var masterMap = new Dictionary<string, string>
                                {
                                    {"Home", "Home"},
                                    {"ScheduleItem/Item", "Session"}, {"RobotsTxt", ""},
                                    {"RSS",""}, {"ATOM", ""}, {"OpenSearch", ""}, {"SiteMapIndex", ""}, {"SiteMap",""}
                                };
            
            if(String.IsNullOrEmpty(masterName))
            {
                if(masterMap.ContainsKey(viewName))
                {
                    masterName = masterMap[viewName];
                } else if(String.IsNullOrEmpty(masterName))
                {
                    masterName = "Site";
                }
            }
            return masterName;
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (String.IsNullOrEmpty(partialViewName))
            {
                throw new ArgumentException("Argument cannot be null or empty", "partialViewName");
            }

            string[] searched;
            var controllerName = controllerContext.RouteData.GetRequiredString("controller");
            var partialPath = GetPath(controllerContext, PartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, controllerName, CacheKeyPrefixPartial, useCache, out searched);

            return String.IsNullOrEmpty(partialPath)
                       ? new ViewEngineResult(searched)
                       : new ViewEngineResult(CreatePartialView(controllerContext, partialPath), this);
        }

        private string CreateCacheKey(string prefix, string name, string controllerName, string skinName)
        {
            return String.Format(CultureInfo.InvariantCulture, CacheKeyFormat,
                                 GetType().AssemblyQualifiedName, prefix, name, controllerName, skinName);
        }

        private string GetPath(ControllerContext controllerContext, IEnumerable<string> locations, string locationsPropertyName, string name, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            searchedLocations = _emptyLocations;

            if (String.IsNullOrEmpty(name))
            {
                return String.Empty;
            }

            var viewLocations = GetViewLocations(locations);
            if (viewLocations.Count == 0)
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentUICulture,
                                                                  "{0} cannot be null or empty.", locationsPropertyName));
            }

            var nameRepresentsPath = IsSpecificPath(name);
            var cacheKey = CreateCacheKey(cacheKeyPrefix, name, (nameRepresentsPath) ? String.Empty : controllerName, Skin);
            if (useCache)
            {
                var cachedLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext, cacheKey);
                if(!String.IsNullOrEmpty(cachedLocation))
                {
                    return cachedLocation;
                }
            }

            return (nameRepresentsPath)
                       ? GetPathFromSpecificName(controllerContext, name, cacheKey, ref searchedLocations)
                       : GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, Skin,
                                                cacheKey, ref searchedLocations);
        }

        private string GetPathFromGeneralName(ControllerContext controllerContext, IList<ViewLocation> locations, string name, string controllerName, string areaName, string cacheKey, ref string[] searchedLocations)
        {
            var result = String.Empty;
            searchedLocations = new string[locations.Count];

            for (var i = 0; i < locations.Count; i++)
            {
                var location = locations[i];
                var virtualPath = location.Format(name, controllerName, areaName);

                if (FileExists(controllerContext, virtualPath))
                {
                    searchedLocations = _emptyLocations;
                    result = virtualPath;
                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
                    break;
                }

                searchedLocations[i] = virtualPath;
            }

            return result;
        }

        private string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            var result = name;

            if (!FileExists(controllerContext, name))
            {
                result = String.Empty;
                searchedLocations = new[] { name };
            }

            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
            return result;
        }

        private static List<ViewLocation> GetViewLocations(IEnumerable<string> viewLocationFormats)
        {
            var allLocations = new List<ViewLocation>();

            if (viewLocationFormats != null)
            {
                foreach (var viewLocationFormat in viewLocationFormats)
                {
                    allLocations.Add(new ViewLocation(viewLocationFormat));
                }
            }

            return allLocations;
        }

        private static bool IsSpecificPath(string name)
        {
            var c = name[0];
            return (c == '~' || c == '/');
        }

        private sealed class ViewLocation
        {
            private readonly string _virtualPathFormatString;

            public ViewLocation(string virtualPathFormatString)
            {
                _virtualPathFormatString = virtualPathFormatString;
            }

            public string Format(string viewName, string controllerName, string skinName)
            {
                var result = String.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName, skinName);
                return result;
            }
        }

        public virtual FileEngineResult FindScriptsFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;

            var result = FindFile("/Scripts" + fileName);
            return result;
        }

        public virtual FileEngineResult FindStylesFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;

            var result = FindFile("/Styles" + fileName);
            return result;
        }

        public virtual FileEngineResult FindImageFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;

            var result = FindFile("/Styles/i" + fileName);
            return result;
        }

        public virtual FileEngineResult FindFile(string fileName)
        {
            if (fileName.Contains("?"))
                fileName = fileName.Substring(0, fileName.IndexOf('?'));

            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;

            var originalFileName = fileName;
            fileName = rootPath + fileName;

            if (VirtualPathProvider.FileExists(fileName))
            {
                return new FileEngineResult(fileName, this);
            }

            fileName = "~/Skins/Conferences" + originalFileName;

            if (VirtualPathProvider.FileExists(fileName))
            {
                return new FileEngineResult(fileName, this);
            }

            return new FileEngineResult(new [] { fileName });
        }

        #endregion
    }
}
