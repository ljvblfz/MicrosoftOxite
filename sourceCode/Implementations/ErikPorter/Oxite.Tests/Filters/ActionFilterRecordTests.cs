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
    public class ActionFilterRecordTests
    {
        private ActionFilterRegistryContext GetFakeContext()
        {
            return new ActionFilterRegistryContext(new ControllerContext(new FakeHttpContext("~/"), new RouteData(), new FakeController()), new FakeActionDescriptor());
        }

        [Fact]
        public void MatchMatches()
        {
            ActionFilterRecord record = new ActionFilterRecord(new [] { new FakeActionFilterCriteria() { IsMatch = true}}, typeof(FakeActionFilter));

            Assert.True(record.Match(this.GetFakeContext()));
        }

        [Fact]
        public void MatchDoesntMatchOnFailedSingleCriteria()
        {
            ActionFilterRecord record = new ActionFilterRecord(new [] { new FakeActionFilterCriteria() { IsMatch = false }},
                typeof(FakeActionFilter));

            Assert.False(record.Match(this.GetFakeContext()));
        }

        [Fact]
        public void MatchDoesntMatchOnFailedMultipleCriteria()
        {
            ActionFilterRecord record = new ActionFilterRecord(new [] { new FakeActionFilterCriteria() { IsMatch = true }, new FakeActionFilterCriteria() { IsMatch = false }},
                typeof(FakeActionFilter));

            Assert.False(record.Match(this.GetFakeContext()));
        }

        [Fact]
        public void MatchMatchesOnEmptyCriteriaSet()
        {
            ActionFilterRecord record = new ActionFilterRecord(Enumerable.Empty<IActionFilterCriteria>(), typeof(FakeActionFilter));

            Assert.True(record.Match(null));
        }
    }
}
