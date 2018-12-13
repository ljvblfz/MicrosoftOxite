//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;
using Oxite.Models;

namespace Oxite.Infrastructure
{
    public class OxiteContext
    {
        public OxiteContext(RequestContext requestContext, RouteCollection routes, Site site, IUser user)
        {
            HttpContext = requestContext.HttpContext;
            RequestContext = requestContext;
            Routes = routes;
            Site = site;
            User = user;

            string dataFormat = RequestContext.RouteData.Values["dataFormat"] as string;

            if (string.Compare(dataFormat, "RSS", true) == 0)
                RequestDataFormat = RequestDataFormat.RSS;
            else if (string.Compare(dataFormat, "ATOM", true) == 0)
                RequestDataFormat = RequestDataFormat.ATOM;
            else
                RequestDataFormat = RequestDataFormat.Web;
        }

        public OxiteContext(OxiteContext context)
            : this(context.RequestContext, context.Routes, context.Site, context.User)
        {
        }

        public HttpContextBase HttpContext { get; private set; }
        public RequestContext RequestContext { get; private set; }
        public RequestDataFormat RequestDataFormat { get; private set; }
        public RouteCollection Routes { get; private set; }
        public Site Site { get; private set; }
        public IUser User { get; private set; }
    }
}
