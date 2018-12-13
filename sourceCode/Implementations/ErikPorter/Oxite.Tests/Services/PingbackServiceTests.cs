//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Web.Routing;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Tests.Fakes;
using Oxite.WebServices;
using Xunit;

namespace Oxite.Tests.Services
{
    public class PingbackServiceTests
    {
        [Fact]
        public void PingFaultsForNullSourceUri()
        {
            PingbackService service = new PingbackService(null, null);

            Assert.Throws<ArgumentNullException>(() => service.Ping(null, null));
        }

        [Fact]
        public void PingFaultsForNullTargetUri()
        {
            PingbackService service = new PingbackService(null, null);
            Assert.Throws<ArgumentNullException>(() => service.Ping("http://test", null));
        }

        [Fact]
        public void PingFaultsForNotFoundPost()
        {
            FakePostService postService = new FakePostService();
            RouteCollection routes = new RouteCollection();

            routes.Add("Post", new Route("{slug}/{areaName}", null));

            AbsolutePathHelper helper = new AbsolutePathHelper(new Site() { Host = new Uri("http://foo.com") }, routes);

            PingbackService service = new PingbackService(postService, helper);

            Assert.Throws<ArgumentException>(() =>service.Ping("http://test.com/foo", "http://foo.com/test/area"));
        }

        [Fact]
        public void PingCreatesNewTrackbackForNotFoundTrackback()
        {
            FakePostService postService = new FakePostService();
            postService.AddedPosts.Add(new Oxite.Models.Post() { Slug = "test" , Trackbacks = new List<Trackback>()});

            RouteCollection routes = new RouteCollection();
            routes.Add("Post", new Route("{slug}/{areaName}", null));

            AbsolutePathHelper helper = new AbsolutePathHelper(new Site() { Host = new Uri("http://foo.com") }, routes);

            PingbackService service = new PingbackService(postService, helper);

            string result = service.Ping("http://test.com/foo", "http://foo.com/test/area");

            Assert.NotNull(result);
            Assert.NotNull(postService.AddedTrackback);
            Assert.Equal("http://test.com/foo", postService.AddedTrackback.Url);
        }

        [Fact]
        public void PingReturnsSuccessIfTrackbackFound()
        {
            FakePostService postService = new FakePostService();
            postService.AddedPosts.Add(new Oxite.Models.Post() { Slug = "test", Trackbacks = new List<Trackback>(new[] { new Trackback() { Url = "http://test.com/foo" } }) });

            RouteCollection routes = new RouteCollection();
            routes.Add("Post", new Route("{slug}/{areaName}", null));

            AbsolutePathHelper helper = new AbsolutePathHelper(new Site() { Host = new Uri("http://foo.com") }, routes);

            PingbackService service = new PingbackService(postService, helper);

            string result = service.Ping("http://test.com/foo", "http://foo.com/test/area");

            Assert.Null(postService.AddedTrackback);
        }
    }
}
