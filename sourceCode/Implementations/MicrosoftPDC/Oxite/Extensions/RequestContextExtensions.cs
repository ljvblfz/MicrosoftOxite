// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;

namespace Oxite.Extensions
{
    public static class RequestContextExtensions
    {
        public static void PermanentRedirect(this RequestContext requestContext, string url)
        {
            string destinationUrl = new UrlHelper(requestContext).Content(url);

            requestContext.HttpContext.Response.StatusCode = 301;
            requestContext.HttpContext.Response.StatusDescription = "Moved Permanently";
            requestContext.HttpContext.Response.AddHeader("content-length", "0");
            requestContext.HttpContext.Response.AddHeader("Location", destinationUrl);
        }
    }
}