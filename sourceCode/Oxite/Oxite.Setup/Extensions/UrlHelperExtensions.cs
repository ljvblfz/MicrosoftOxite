//---------------------------------------------------------------------
// <copyright file="UrlHelperExtensions.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
using System.Web.Mvc;
using Oxite.Extensions;
using Oxite.Models;

namespace Oxite.Modules.Setup.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string SetupAdminSettings(this UrlHelper urlHelper)
        {
            return urlHelper.Action("AdminSettings");
        }

        public static string SetupBasicSettings(this UrlHelper urlHelper)
        {
            return urlHelper.Action("BasicSettings");
        }

        public static string SetupComplete(this UrlHelper urlHelper)
        {
            return urlHelper.Action("SetupComplete");
        }

        public static string SetupModules(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Modules");
        }

        public static string SetupStorage(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Storage");
        }

        public static string SetupSiteType(this UrlHelper urlHelper)
        {
            return urlHelper.Action("SiteType");
        }
    }
}
