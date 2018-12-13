//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Infrastructure;
using Oxite.Tests.Fakes;
using Xunit;

namespace Oxite.Tests.Filters
{
    public class FilterRegistryItemTests
    {
        private FilterRegistryContext GetFakeContext()
        {
            return new FilterRegistryContext(new ControllerContext(new FakeHttpContext("~/"), new RouteData(), new FakeController()), new FakeActionDescriptor());
        }

        [Fact]
        public void MatchMatches()
        {
            FilterRegistryItem item = new FilterRegistryItem(new [] { new FakeFilterCriteria() { IsMatch = true}}, typeof(FakeActionFilter));

            Assert.True(item.Match(this.GetFakeContext()));
        }

        [Fact]
        public void MatchDoesntMatchOnFailedSingleCriteria()
        {
            FilterRegistryItem item = new FilterRegistryItem(new [] { new FakeFilterCriteria() { IsMatch = false }}, typeof(FakeActionFilter));

            Assert.False(item.Match(this.GetFakeContext()));
        }

        [Fact]
        public void MatchDoesntMatchOnFailedMultipleCriteria()
        {
            FilterRegistryItem item = new FilterRegistryItem(new [] { new FakeFilterCriteria() { IsMatch = true }, new FakeFilterCriteria() { IsMatch = false }}, typeof(FakeActionFilter));

            Assert.False(item.Match(this.GetFakeContext()));
        }

        [Fact]
        public void MatchMatchesOnEmptyCriteriaSet()
        {
            FilterRegistryItem item = new FilterRegistryItem(Enumerable.Empty<IFilterCriteria>(), typeof(FakeActionFilter));

            Assert.True(item.Match(null));
        }
    }
}
