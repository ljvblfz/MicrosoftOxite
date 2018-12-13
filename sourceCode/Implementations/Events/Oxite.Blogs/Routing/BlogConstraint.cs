//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Modules.Blogs.Services;

namespace Oxite.Modules.Blogs.Routing
{
    public class BlogConstraint : IRouteConstraint
    {
        private readonly IUnityContainer container;

        public BlogConstraint(IUnityContainer container)
        {
            this.container = container;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            IBlogService blogService = container.Resolve<IBlogService>();
            string blogName = values[parameterName].ToString();

            return blogService.GetBlogExists(blogName);
        }
    }
}
