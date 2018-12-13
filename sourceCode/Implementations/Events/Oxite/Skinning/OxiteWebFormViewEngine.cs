//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Infrastructure;

namespace Oxite.Skinning
{

    
    public class OxiteWebFormViewEngine : WebFormViewEngine, IOxiteViewEngine
    {
        private string rootPath;

        #region IOxiteViewEngine Members

        private static bool IsMobileDevice(HttpBrowserCapabilitiesBase browserInfo, string userAgent)
        {
            if (browserInfo.IsMobileDevice)
                return true;

            if ((userAgent.ToLower().Contains("windows phone"))
               || (userAgent.ToLower().Contains("windows mobile"))
               || (userAgent.ToLower().Contains("opera mobi"))
               || (userAgent.ToLower().Contains("ppc")))
                return true;

            return false;
        }

        private bool IsMobile(ControllerContext controllerContext)
        {

            return false;
            var request = controllerContext.HttpContext.Request;
            var browserInfo = request.Browser;
            bool isMobile = IsMobileDevice(browserInfo, request.UserAgent);
            string skin = request.Cookies.GetSelectedSkin(isMobile);

            return skin == "Mobile";
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            ViewEngineResult ver = null;

            if (IsMobile(controllerContext))
                ver = base.FindPartialView(controllerContext, "Mobile/" + partialViewName, useCache);

            if (ver == null || ver.View == null)
                ver = base.FindPartialView(controllerContext, partialViewName, useCache);

            return ver;
        }

        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            ViewEngineResult ver = null;

            if (IsMobile(controllerContext))
                ver = base.FindView(controllerContext, "Mobile/" + viewName, masterName, useCache);

            if (ver == null || ver.View == null)
                ver = base.FindView(controllerContext, viewName, masterName, useCache);

            return ver;
        }

        public void SetRootPath(string rootPath)
        {
            SetRootPath(rootPath, false);
        }

        public void SetRootPath(string rootPath, bool onlySearchRootPathForPartialViews)
        {
            if (rootPath.EndsWith("/"))
                rootPath = rootPath.Substring(0, rootPath.Length - 1);

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

            this.rootPath = rootPath;
        }

        public virtual FileEngineResult FindScriptsFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;

            return FindFile("/Scripts" + fileName);
        }

        public virtual FileEngineResult FindStylesFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;

            return FindFile("/Styles" + fileName);
        }

        public virtual FileEngineResult FindFile(string fileName)
        {


            if (fileName.Contains("?"))
                fileName = fileName.Substring(0, fileName.IndexOf('?'));

            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;




            fileName = rootPath + fileName;

            if (VirtualPathProvider.FileExists(fileName))
                return new FileEngineResult(fileName, this);

            return new FileEngineResult(new[] { fileName });
        }

        #endregion
    }
}
