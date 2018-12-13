//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;

namespace Oxite.Modules.FormsAuthentication.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string SignIn(this UrlHelper urlHelper, string returnUrl)
        {
            return urlHelper.RouteUrl("SignIn", new { ReturnUrl = returnUrl });
        }

        public static string SignOut(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("SignOut");
        }
    }
}
