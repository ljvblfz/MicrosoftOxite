//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Models;

namespace Oxite.Routing
{
    public class OpenSearchConstraint : IRouteConstraint
    {
        private readonly IUnityContainer container;

        public OpenSearchConstraint(IUnityContainer container)
        {
            this.container = container;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            Site site = container.Resolve<Site>();

            if (site != null)
                return site.IncludeOpenSearch;

            return false;
        }
    }
}
