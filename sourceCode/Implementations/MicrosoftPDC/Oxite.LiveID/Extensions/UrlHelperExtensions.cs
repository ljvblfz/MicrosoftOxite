//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Modules.LiveID.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Register(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("LiveIDRegister");
        }

        public static bool IsSignOutRoute(this UrlHelper urlHelper)
        {
            string controller = urlHelper.RequestContext.RouteData.GetRequiredString("controller");
            string action = urlHelper.RequestContext.RouteData.GetRequiredString("action");

            return
                string.Compare(controller, "User", true) == 0 &&
                string.Compare(action, "SignOutImage", true) == 0;
        }

        public static bool IsErrorRoute(this UrlHelper urlHelper)
        {
            return urlHelper.RequestContext.HttpContext.Request.Url.Query.Contains("aspxerrorpath");
        }


        public static bool IsRegisterRoute(this UrlHelper urlHelper)
        {
            string controller = urlHelper.RequestContext.RouteData.GetRequiredString("controller");
            string action = urlHelper.RequestContext.RouteData.GetRequiredString("action");

            return
                string.Compare(controller, "User", true) == 0 &&
                string.Compare(action, "Register", true) == 0;
        }
    }
}
