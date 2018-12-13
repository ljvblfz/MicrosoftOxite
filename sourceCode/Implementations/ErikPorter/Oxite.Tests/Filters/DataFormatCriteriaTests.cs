//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Routing;
using Oxite.ActionFilters;
using Oxite.Infrastructure;
using Oxite.Tests.Fakes;
using Xunit;

namespace Oxite.Tests.Filters
{
    public class DataFormatCriteriaTests
    {
        [Fact]
        public void MatchMatchesPassedDataFormat()
        {
            DataFormatCriteria criteria = new DataFormatCriteria("RSS");

            RouteData routeData = new RouteData();

            routeData.Values.Add("dataFormat", "RSS");

            ActionFilterRegistryContext context = new ActionFilterRegistryContext(new System.Web.Mvc.ControllerContext(new FakeHttpContext("~/"), routeData, new FakeController()), new FakeActionDescriptor());

            Assert.True(criteria.Match(context));
        }

        [Fact]
        public void MatchDoesNotMatchWithNoDataFormat()
        {
            DataFormatCriteria criteria = new DataFormatCriteria("RSS");

            RouteData routeData = new RouteData();

            ActionFilterRegistryContext context = new ActionFilterRegistryContext(new System.Web.Mvc.ControllerContext(new FakeHttpContext("~/"), routeData, new FakeController()), new FakeActionDescriptor());

            Assert.False(criteria.Match(context));
        }
    }
}
