//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using Oxite.Filters;
using Oxite.Results;
using Xunit;

namespace Oxite.Tests.Filters
{
    public class AtomResultActionFilterTests
    {
        [Fact]
        public void OnActionExecutedReplacesWithAtomResult()
        {
            AtomResultActionFilter filter = new AtomResultActionFilter();

            ActionExecutedContext context = new ActionExecutedContext()
            {
                Result = new ViewResult()
            };

            filter.OnActionExecuted(context);

            FeedResult result = context.Result as FeedResult;

            Assert.NotNull(result);
            Assert.Equal("ATOM", result.ViewName);
            Assert.False(result.IsClientCached);
        }
    }
}
