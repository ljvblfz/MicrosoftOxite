using Oxite.Controllers;
//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
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
            FakePostService postService = new FakePostService();

            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            CommentController controller = new CommentController(postService, null, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.List();

            Assert.Equal(2, result.List.Count);
            Assert.Same(postService.AllComments[0], result.List[0]);
            Assert.Same(postService.AllComments[1], result.List[1]);
        }

        [Fact]
        public void ListSetsHomePageContainer()
        {
            FakePostService postService = new FakePostService();

            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            CommentController controller = new CommentController(postService, null, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.List();

            Assert.IsType<HomePageContainer>(result.Container);
        }

        [Fact]
        public void ListByTagReturnsListOfComments()
        {
            FakePostService postService = new FakePostService();
            FakeTagService tagService = new FakeTagService();

            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            tagService.StoredTags.Add("test", new Tag());

            CommentController controller = new CommentController(postService, tagService, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.ListByTag(new Tag() { Name = "test" });

            Assert.Equal(2, result.List.Count);
            Assert.Same(postService.AllComments[0], result.List[0]);
            Assert.Same(postService.AllComments[1], result.List[1]);
        }

        [Fact]
        public void ListByTagSetsTagAsContainer()
        {
            FakePostService postService = new FakePostService();
            FakeTagService tagService = new FakeTagService();

            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            tagService.StoredTags.Add("test", new Tag());

            CommentController controller = new CommentController(postService, tagService, null) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.ListByTag(new Tag() { Name = "test" });

            Assert.Same(tagService.StoredTags["test"], result.Container);
        }

        [Fact]
        public void ListByTagReturnsNullOnBadTag()
        {
            FakeTagService tagService = new FakeTagService();

            CommentController controller = new CommentController(null, tagService, null);

            Assert.Null(controller.ListByTag(new Tag() { Name = "test" }));
        }

        [Fact]
        public void ListByAreaReturnsListOfComments()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();

            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.ListByArea(new Area() { Name = "test" });

            Assert.Equal(2, result.List.Count);
            Assert.Same(postService.AllComments[0], result.List[0]);
            Assert.Same(postService.AllComments[1], result.List[1]);
        }

        [Fact]
        public void ListByAreaSetsAreaAsContainer()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.ListByArea(new Area() { Name = "test" });
            
            Assert.Same(areaService.StoredAreas["test"], result.Container);
        }

        [Fact]
        public void ListByAreaReturnsNullOnBadArea()
        {
            FakeAreaService areaService = new FakeAreaService();
            
            CommentController controller = new CommentController(null, null, areaService);

            Assert.Null(controller.ListByArea(new Area() { Name = "test" }));
        }

        [Fact]
        public void ListByPostReturnsListOfComments()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();

            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            postService.AddedPosts.Add(new Post() { Slug = "test" });

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.ListByPost(new PostAddress("test", "test"));

            Assert.Equal(2, result.List.Count);
            Assert.Same(postService.AllComments[0].Child, result.List[0].Child);
            Assert.Same(postService.AllComments[1].Child, result.List[1].Child);
            Assert.Same(postService.AddedPosts[0], result.List[0].Parent);
            Assert.Same(postService.AddedPosts[0], result.List[1].Parent);
        }

        [Fact]
        public void ListByPostSetsPostAsContainer()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();

            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });
            postService.AllComments.Add(new ParentAndChild<PostBase, Comment>() { Parent = new Post(), Child = new Comment() });

            postService.AddedPosts.Add(new Post() { Slug = "test" });

            areaService.StoredAreas.Add("test", new Area());

            CommentController controller = new CommentController(postService, null, areaService) { ControllerContext = new System.Web.Mvc.ControllerContext() { RouteData = new System.Web.Routing.RouteData() } };

            OxiteModelList<ParentAndChild<PostBase, Comment>> result = controller.ListByPost(new PostAddress("test", "test"));

            Assert.Same(postService.AddedPosts[0], result.Container);
        }

        [Fact]
        public void ListByPostReturnsNullOnBadPostAddress()
        {
            FakePostService postService = new FakePostService();

            CommentController controller = new CommentController(postService, null, null);

            Assert.Null(controller.ListByPost(new PostAddress("test", "test")));
        }
    }
}
