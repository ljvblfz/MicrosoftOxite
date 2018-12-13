//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Routing;
using Oxite.Infrastructure;
using Oxite.Tests.Fakes;
using Xunit;

namespace Oxite.Tests.Infrastructure
{
    public class DataFormatCriteriaTests
    {
        [Fact]
        public void MatchMatchesPassedDataFormat()
        {
            DataFormatFilterCriteria criteria = new DataFormatFilterCriteria("RSS");

            RouteData routeData = new RouteData();

            routeData.Values.Add("dataFormat", "RSS");

            FilterRegistryContext context = new FilterRegistryContext(new System.Web.Mvc.ControllerContext(new FakeHttpContext("~/"), routeData, new FakeController()), new FakeActionDescriptor());

            Assert.True(criteria.Match(context));
        }

        [Fact]
        public void MatchDoesNotMatchWithNoDataFormat()
        {
            DataFormatFilterCriteria criteria = new DataFormatFilterCriteria("RSS");

            RouteData routeData = new RouteData();

            FilterRegistryContext context = new FilterRegistryContext(new System.Web.Mvc.ControllerContext(new FakeHttpContext("~/"), routeData, new FakeController()), new FakeActionDescriptor());

            Assert.False(criteria.Match(context));
        }
    }
}
