//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Linq;
using Oxite.Controllers;
using Oxite.Models;
using Oxite.Tests.Fakes;
using Oxite.ViewModels;
using Xunit;

namespace Oxite.Tests.Controllers
{
    public class CommentControllerTests
    {
        [Fact]
        public void ListReturnsListOfAllComments()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            CommentController controller = new CommentController(postService, commentService, null, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.List();

            Assert.Equal(2, result.Items.Count());
            Assert.Same(commentService.AllComments[0], result.Items.ElementAt(0));
            Assert.Same(commentService.AllComments[1], result.Items.ElementAt(1));
        }

        [Fact]
        public void ListSetsHomePageContainer()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            CommentController controller = new CommentController(postService, commentService, null, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.List();

            Assert.IsType<HomePageContainer>(result.Container);
        }

        [Fact]
        public void ListByTagReturnsListOfComments()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();
            FakeTagService tagService = new FakeTagService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            tagService.StoredTags.Add("test", new Tag());

            CommentController controller = new CommentController(postService, commentService, tagService, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.ListByTag(new Tag() { Name = "test" });

            Assert.Equal(2, result.Items.Count());
            Assert.Same(commentService.AllComments[0], result.Items.ElementAt(0));
            Assert.Same(commentService.AllComments[1], result.Items.ElementAt(1));
        }

        [Fact]
        public void ListByTagSetsTagAsContainer()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();
            FakeTagService tagService = new FakeTagService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            tagService.StoredTags.Add("test", new Tag());

            CommentController controller = new CommentController(postService, commentService, tagService, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.ListByTag(new Tag() { Name = "test" });

            Assert.Same(tagService.StoredTags["test"], result.Container);
        }

        [Fact]
        public void ListByTagReturnsNullOnBadTag()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakeTagService tagService = new FakeTagService();

            CommentController controller = new CommentController(null, commentService, tagService, null);

            Assert.Null(controller.ListByTag(new Tag() { Name = "test" }));
        }

        [Fact]
        public void ListByAreaReturnsListOfComments()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, commentService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.ListByArea(new Area() { Name = "test" });

            Assert.Equal(2, result.Items.Count());
            Assert.Same(commentService.AllComments[0], result.Items.ElementAt(0));
            Assert.Same(commentService.AllComments[1], result.Items.ElementAt(1));
        }

        [Fact]
        public void ListByAreaSetsAreaAsContainer()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, commentService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.ListByArea(new Area() { Name = "test" });
            
            Assert.Same(areaService.StoredAreas["test"], result.Container);
        }

        [Fact]
        public void ListByAreaReturnsNullOnBadArea()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakeAreaService areaService = new FakeAreaService();

            CommentController controller = new CommentController(null, commentService, null, areaService);

            Assert.Null(controller.ListByArea(new Area() { Name = "test" }));
        }

        [Fact]
        public void ListByPostReturnsListOfComments()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            postService.AddedPosts.Add(new Post() { Slug = "test" });

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, commentService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.ListByPost(0, 50, new PostAddress("test", "test"), null);

            Assert.Equal(2, result.Items.Count());
            Assert.Same(commentService.AllComments[0], result.Items.ElementAt(0));
            Assert.Same(commentService.AllComments[1], result.Items.ElementAt(1));
            Assert.Same(postService.AddedPosts[0], result.Items.ElementAt(0).Parent);
            Assert.Same(postService.AddedPosts[0], result.Items.ElementAt(1).Parent);
        }

        [Fact]
        public void ListByPostSetsPostAsContainer()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();

            commentService.AllComments.Add(new Comment());
            commentService.AllComments.Add(new Comment());

            postService.AddedPosts.Add(new Post() { Slug = "test" });

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, commentService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteViewModelItems<Comment> result = controller.ListByPost(0, 50, new PostAddress("test", "test"), null);

            Assert.Same(postService.AddedPosts[0], result.Container);
        }

        [Fact]
        public void ListByPostReturnsNullOnBadPostAddress()
        {
            FakeCommentService commentService = new FakeCommentService();
            FakePostService postService = new FakePostService();

            CommentController controller = new CommentController(postService, commentService, null, null);

            Assert.Null(controller.ListByPost(0, 50, new PostAddress("test", "test"), null));
        }
    }
}
