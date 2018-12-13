//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Filters;
using Oxite.Tests.Fakes;
using Xunit;

namespace Oxite.Tests.Filters
{
    public class AuthorizationFilterTests
    {
        [Fact]
        public void OnAuthorizationDoesNotTouchResultIfRequestIsAuthenticated()
        {
            AuthorizationContext context = new AuthorizationContext() { HttpContext = new FakeHttpContext("~/Secure") };
            (context.HttpContext.Request as FakeHttpRequest).RequestIsAuthenticated = true;

            AuthorizationFilter filter = new AuthorizationFilter(new RouteCollection());

            filter.OnAuthorization(context);

            Assert.Null(context.Result);
        }

        [Fact]
        public void OnAuthorizationRedirectsToSignInWithReturnUrlIfRequestIsNotAuthenticated()
        {
            AuthorizationContext context = new AuthorizationContext() { HttpContext = new FakeHttpContext(new Uri("http://foo.com/Secure"),"~/Secure") };

            RouteCollection routes = new RouteCollection();
            routes.Add("SignIn", new Route("SignIn", null));

            AuthorizationFilter filter = new AuthorizationFilter(routes);

            filter.OnAuthorization(context);

            RedirectResult result = context.Result as RedirectResult;
            Assert.NotNull(result);
            Assert.Equal("/SignIn?ReturnUrl=%2FSecure", result.Url);
        }
    }
}
