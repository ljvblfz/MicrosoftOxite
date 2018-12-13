//---------------------------------------------------------------------
// <copyright file="OxiteViewModel.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.Skinning
{
    using System.Web.Mvc;
    using Infrastructure;

    public class OxiteWebFormViewEngine : WebFormViewEngine, IOxiteViewEngine
    {
        private string rootPath;

        #region IOxiteViewEngine Members

        public void SetRootPath(string rootPath)
        {
            SetRootPath(rootPath, false);
        }

        public void SetRootPath(string rootPath, bool onlySearchRootPathForPartialViews)
        {
            if (rootPath.EndsWith("/"))
            {
                rootPath = rootPath.Substring(0, rootPath.Length - 1);
            }

            MasterLocationFormats = new []
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
                : new [] { rootPath + "/{0}.ascx" };

            this.rootPath = rootPath;
        }

        public virtual FileEngineResult FindScriptsFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
            {
                fileName = "/" + fileName;
            }

            return FindFile("/Scripts" + fileName);
        }

        public virtual FileEngineResult FindStylesFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
            {
                fileName = "/" + fileName;
            }

            return FindFile("/Styles" + fileName);
        }

        public virtual FileEngineResult FindFile(string fileName)
        {
            if (!fileName.StartsWith("/"))
            {
                fileName = "/" + fileName;
            }

            fileName = rootPath + fileName;

            if (VirtualPathProvider.FileExists(fileName))
            {
                return new FileEngineResult(fileName, this);
            }

            return new FileEngineResult(new [] { fileName });
        }

        #endregion
    }
}
