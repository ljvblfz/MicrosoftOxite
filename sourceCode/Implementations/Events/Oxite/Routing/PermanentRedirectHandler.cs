// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web;
using System.Web.Routing;
using Oxite.Extensions;

namespace Oxite.Routing
{
    public class PermanentRedirectHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            RequestContext requestContext = new RequestContext(new HttpContextWrapper(context), new RouteData());

            requestContext.PermanentRedirect(context.Request.Url.ToString().TrimEnd('/'));
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}