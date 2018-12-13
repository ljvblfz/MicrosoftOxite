//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Tests.Fakes;
using Oxite.WebServices;
using Xunit;

namespace Oxite.Tests.RouteHandlers
{
    public class MetaWeblogServiceTests
    {
        #region NewPost

        [Fact]
        public void NewPostAddsToService()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();
            Guid areaID = Guid.NewGuid();
            areaService.StoredAreas.Add("test", new Oxite.Models.Area() { ID = areaID });

            MetaWeblogAPI service = new MetaWeblogAPI(postService, areaService, userService, null, null);

            Post newPost = new Post() { title = "Test", description = "A Test", dateCreated = DateTime.Now, mt_basename = "test" };
            string postIDString = service.NewPost(areaID.ToString(), "test", "test", newPost, false);

            Assert.NotNull(postIDString);
            Assert.Equal(1, postService.AddedPosts.Count);
            Assert.Equal(postService.AddedPosts[0].ID.ToString(), postIDString);
            Assert.Equal("Test", postService.AddedPosts[0].Title);
            Assert.Equal("A Test", postService.AddedPosts[0].Body);
            Assert.Equal(newPost.dateCreated, postService.AddedPosts[0].Created);
            Assert.Equal("test", postService.AddedPosts[0].Slug);
            Assert.Equal(Oxite.Models.EntityState.Normal, postService.AddedPosts[0].State);
        }

        [Fact]
        public void NewPostSetsPublishDateIfPublishTrue()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();
            Guid areaID = Guid.NewGuid();

            areaService.StoredAreas.Add("test", new Oxite.Models.Area() { ID = areaID });

            MetaWeblogAPI service = new MetaWeblogAPI(postService, areaService, userService, null, null);

            Post newPost = new Post() { title = "Test", description = "A Test", dateCreated = DateTime.Now, mt_basename = "test" };

            service.NewPost(areaID.ToString(), "test", "test", newPost, true);

            Assert.True(DateTime.Today < postService.AddedPosts[0].Published);
        }

        [Fact]
        public void NewPostSetsPublishDateToNullIfPublishFalse()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();
            Guid areaID = Guid.NewGuid();

            areaService.StoredAreas.Add("test", new Oxite.Models.Area() { ID = areaID });

            MetaWeblogAPI service = new MetaWeblogAPI(postService, areaService, userService, null, null);

            Post newPost = new Post() { title = "Test", description = "A Test", dateCreated = DateTime.Now, mt_basename = "test" };

            service.NewPost(areaID.ToString(), "test", "test", newPost, false);

            Assert.Null(postService.AddedPosts[0].Published);
        }

        [Fact]
        public void NewPostFaultsForNonParsableBlogID()
        {
            FakeUserService userService = new FakeUserService();
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, userService, null, null);

            Assert.Throws<FormatException>(() => service.NewPost("xxx", "test", "test", null, false));
        }

        [Fact]
        public void NewPostAddsPassedCategoriesAsTags()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();
            Guid areaID = Guid.NewGuid();
            Guid tag1ID = Guid.NewGuid();
            Guid tag2ID = Guid.NewGuid();
            areaService.StoredAreas.Add("test", new Oxite.Models.Area() { ID = areaID });

            Post newPost = new Post()
                           {
                               categories = new[] { "Test1", "Test2" },
                               title = "Test",
                               description = "A Test",
                               dateCreated = DateTime.Now,
                               mt_basename = "test"
                           };

            MetaWeblogAPI service = new MetaWeblogAPI(postService, areaService, userService, null, null);

            service.NewPost(areaID.ToString(), "test", "test", newPost, false);

            Assert.Equal(1, postService.AddedPosts.Count);

            Oxite.Models.Post savedPost = postService.AddedPosts[0];
            Assert.Equal(2, savedPost.Tags.Count);
            Assert.Contains("Test1", savedPost.Tags.Select(t => t.Name));
            Assert.Contains("Test2", savedPost.Tags.Select(t => t.Name));
        }

        [Fact]
        public void NewPostAddsExcerptAsBodyShort()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();
            Guid areaID = Guid.NewGuid();

            areaService.StoredAreas.Add("test", new Oxite.Models.Area() { ID = areaID });
            MetaWeblogAPI service = new MetaWeblogAPI(postService, areaService, userService, null, null);
            
            Post newPost = new Post() { mt_excerpt = "Preview", title = "Test", description = "A Test", dateCreated = DateTime.Now, mt_basename = "test" };
            service.NewPost(areaID.ToString(), "test", "test", newPost, false);

            Assert.Equal(newPost.mt_excerpt, postService.AddedPosts[0].BodyShort);
        }

        [Fact]
        public void NewPostCreatesSlugForEntry()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();
            FakeRegularExpressions regularExpressions = new FakeRegularExpressions();
            Guid areaID = Guid.NewGuid();
            areaService.StoredAreas.Add("test", new Oxite.Models.Area() { ID = areaID });

            Post newPost = new Post() { title = "This is a test", description = "A Test", dateCreated = DateTime.Now, mt_basename = "" };

            MetaWeblogAPI service = new MetaWeblogAPI(postService, areaService, userService, null, regularExpressions);

            service.NewPost(areaID.ToString(), "test", "test", newPost, false);

            Oxite.Models.Post savedPost = postService.AddedPosts[0];
            Assert.Equal("This-is-a-test", savedPost.Slug);
        }

        [Fact]
        public void NewPostFaultsOnNullUser()
        {
            FakeUserService userService = new FakeUserService();

            Guid areaID = Guid.NewGuid();

            MetaWeblogAPI service = new MetaWeblogAPI(null, null, userService, null, null);

            Assert.Throws<ArgumentException>(() => service.NewPost(Guid.NewGuid().ToString(), null, null, new Post(), false));
        }

        [Fact]
        public void NewPostFaultsOnBadUsernamePassword()
        {
            FakeUserService userService = new FakeUserService();

            MetaWeblogAPI service = new MetaWeblogAPI(null, null, userService, null, null);

            userService.Authenticate = false;

            Assert.Throws<System.Security.Authentication.InvalidCredentialException>(() => service.NewPost(Guid.NewGuid().ToString(), "test", "test", null, false));
        }

        [Fact]
        public void NewPostSetsCreatorToUser()
        {
            FakePostService postService = new FakePostService();
            FakeAreaService areaService = new FakeAreaService();
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();
            Guid areaID = Guid.NewGuid();

            areaService.StoredAreas.Add("test", new Oxite.Models.Area() { ID = areaID });

            MetaWeblogAPI service = new MetaWeblogAPI(postService, areaService, userService, null, null);
            
            service.NewPost(areaID.ToString(), "test", "test", new Post(), false);

            Oxite.Models.Post newPost = postService.AddedPosts[0];
            Assert.Equal("test", newPost.Creator.Name);
        }

        #endregion

        #region EditPost

        [Fact]
        public void EditPostFaultsOnInvalidEntryID()
        {
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.EditPost(null, null, null, null, false));
        }

        [Fact]
        public void EditPostSavesChangesToTextFields()
        {
            FakePostService postService = new FakePostService();
            FakeUserService userService = new FakeUserService();

            Guid postID = Guid.NewGuid();

            postService.AddedPosts.Add(new Oxite.Models.Post()
                {
                    ID = postID,
                });

            Post newPost = new Post()
                           {
                               title = "PostTitle",
                               description = "PostDescription",
                               mt_excerpt = "PostBodyShort",
                               mt_basename = "PostSlug"
                           };

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            bool success = service.EditPost(postID.ToString(), "test", "test", newPost, false);

            Assert.True(success);
            Oxite.Models.Post edited = postService.AddedPosts[0];
            Assert.Equal(newPost.title, edited.Title);
            Assert.Equal(newPost.description, edited.Body);
            Assert.Equal(newPost.mt_excerpt, edited.BodyShort);
            Assert.Equal(newPost.mt_basename, edited.Slug);
        }

        [Fact]
        public void EditPostPublishesIfPublishIsTrue()
        {
            FakePostService postService = new FakePostService();
            FakeUserService userService = new FakeUserService();

            Guid postID = Guid.NewGuid();

            postService.AddedPosts.Add(new Oxite.Models.Post()
            {
                ID = postID,
            });

            Post newPost = new Post()
            {
                title = "PostTitle",
                description = "PostDescription",
                mt_excerpt = "PostBodyShort",
                mt_basename = "PostSlug"
            };

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            service.EditPost(postID.ToString(), "test", "test", newPost, true);

            Assert.True(DateTime.Today < postService.AddedPosts[0].Published);
        }

        [Fact]
        public void EditPostEditsTagList()
        {
            FakePostService postService = new FakePostService();
            FakeUserService userService = new FakeUserService();
            Guid postID = Guid.NewGuid();

            postService.AddedPosts.Add(new Oxite.Models.Post()
                                     {
                                         ID = postID,
                                         Title = "PreTitle",
                                         Body = "PreBody",
                                         BodyShort = "PreBodyShort",
                                         Tags =
                                             new List<Oxite.Models.Tag>(new Oxite.Models.Tag[]
                                                               {
                                                                   new Oxite.Models.Tag() {Name = "Old1"},
                                                                   new Oxite.Models.Tag() {Name = "Both1"}
                                                               })
                                     });

            Post newPost = new Post()
                           {
                               categories = new[] { "New1", "Both1" },
                               title = "PostTitle",
                               description = "PostDescription",
                               mt_excerpt = "PostBodyShort"
                           };

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            service.EditPost(postID.ToString(), "test", "test", newPost, false);

            Oxite.Models.Post edited = postService.AddedPosts[0];
            Assert.Equal(2, edited.Tags.Count());
            Assert.Contains("New1", edited.Tags.Select(t => t.Name));
            Assert.Contains("Both1", edited.Tags.Select(t => t.Name));
        }

        [Fact]
        public void EditPostFaultsOnNullUser()
        {
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.EditPost(Guid.NewGuid().ToString(), null, null, null, false));
        }

        [Fact]
        public void EditPostDoesntTouchAlreadyPublishedEntrysPublishDate()
        {
            FakePostService postService = new FakePostService();
            FakeUserService userService = new FakeUserService();

            Guid postID = Guid.NewGuid();
            DateTime publishedDate = DateTime.Now.AddDays(-1);

            postService.AddedPosts.Add(new Oxite.Models.Post(publishedDate)
            {
                ID = postID
            });

            Post newPost = new Post()
            {
                title = "PostTitle",
                description = "PostDescription",
                mt_excerpt = "PostBodyShort",
                mt_basename = "PostSlug"
            };

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            service.EditPost(postID.ToString(), "test", "test", newPost, true);

            Assert.Equal(publishedDate, postService.AddedPosts[0].Published);
        }

        #endregion

        #region GetPost

        [Fact]
        public void GetPostFaultsOnBadID()
        {
            FakePostService postService = new FakePostService();
            FakeUserService userService = new FakeUserService();

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            Assert.Throws<ArgumentOutOfRangeException>(() => service.GetPost(Guid.NewGuid().ToString(), "test", "test"));
        }

        [Fact]
        public void GetPostReturnsPost()
        {
            FakePostService postService = new FakePostService();
            FakeUserService userService = new FakeUserService();

            DateTime now = DateTime.Now;

            Oxite.Models.Post fakePost = new Oxite.Models.Post(DateTime.Now)
                                {
                                    Title = "Title",
                                    Body = "Body",
                                    Created = DateTime.Now,
                                    ID = Guid.NewGuid(),
                                    Creator = new Oxite.Models.User() { DisplayName="Test User", Name = "user" },
                                    BodyShort = "Excerpt",
                                    Slug = "Slug"
                                };
            fakePost.Area = new Oxite.Models.Area() { ID = Guid.NewGuid(), Name = "Blog1", DisplayName = "Blog One" };
            fakePost.Tags =
                new List<Oxite.Models.Tag>(new[]
                                  {
                                      new Oxite.Models.Tag() {ID = Guid.NewGuid(), Name = "Tag1"},
                                      new Oxite.Models.Tag() {ID = Guid.NewGuid(), Name = "Tag2"}
                                  });

            postService.AddedPosts.Add(fakePost);

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            Post post = service.GetPost(fakePost.ID.ToString(), "test", "test");

            Assert.NotNull(post);
            Assert.Equal(fakePost.Title, post.title);
            Assert.Equal(fakePost.Body, post.description);
            Assert.Equal(fakePost.Created, post.dateCreated);
            Assert.Equal(fakePost.Creator.ID, new Guid(post.userid));
            Assert.Equal(fakePost.BodyShort, post.mt_excerpt);
            Assert.Equal(fakePost.Slug, post.mt_basename);
            foreach (string category in post.categories)
                Assert.Contains<string>(category, fakePost.Tags.Select(t => t.Name));
        }

        [Fact]
        public void GetPostFaultsOnNullUser()
        {
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.GetPost(Guid.NewGuid().ToString(), null, null));
        }

        #endregion

        #region GetUsersBlogs

        [Fact]
        public void GetUsersBlogsFaultsOnNullUser()
        {
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.GetUsersBlogs(null, null, null));
        }

        [Fact]
        public void GetUsersBlogsReturnsAllAreasForValidUser()
        {
            FakeAreaService areaService = new FakeAreaService();
            FakeUserService userService = new FakeUserService();

            areaService.StoredAreas.Add("test1", new Oxite.Models.Area() { DisplayName = "Test One", ID = Guid.NewGuid() });
            areaService.StoredAreas.Add("test2", new Oxite.Models.Area() { DisplayName = "Test Two", ID = Guid.NewGuid() });

            MetaWeblogAPI service = new MetaWeblogAPI(null, areaService, userService, null, null);

            BlogInfo[] blogs = service.GetUsersBlogs(null, "test", "test");

            Assert.Equal(2, blogs.Length);
            Assert.Contains(areaService.StoredAreas["test1"].ID.ToString(), blogs.Select(b => b.blogid));
            Assert.Contains(areaService.StoredAreas["test2"].ID.ToString(), blogs.Select(b => b.blogid));
            Assert.Contains(areaService.StoredAreas["test1"].DisplayName, blogs.Select(b => b.blogName));
            Assert.Contains(areaService.StoredAreas["test2"].DisplayName, blogs.Select(b => b.blogName));
        }

        #endregion

        #region GetCategories

        [Fact]
        public void GetCategoriesReturnsAllTags()
        {
            FakeTagService tagService = new FakeTagService();
            FakeUserService userService = new FakeUserService();

            tagService.StoredTags.Add("test", new Oxite.Models.Tag() { Name = "test" });

            MetaWeblogAPI service = new MetaWeblogAPI(null, null, userService, tagService, null);

            CategoryInfo[] categories = service.GetCategories(null, "test", "test");

            Assert.Equal(1, categories.Length);
            Assert.Equal("test", categories[0].description);
        }

        [Fact]
        public void GetCategoriesFaultsOnNullUser()
        {
            FakeUserService userService = new FakeUserService();
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, userService, null, null);

            Assert.Throws<ArgumentException>(() => service.GetCategories(null, null, null));
        }

        #endregion

        #region GetRecentPosts

        [Fact]
        public void GetRecentPostsReturnsNumberOfPostsInBlog()
        {
            FakeUserService userService = new FakeUserService();
            FakePostService postService = new FakePostService();

            Guid areaID = Guid.NewGuid();

            Oxite.Models.Post post1 = new Oxite.Models.Post()
                             {
                                 ID = Guid.NewGuid(),
                                 Creator = new Oxite.Models.User(),
                                 Area = new Oxite.Models.Area() { ID = areaID },
                                 Slug = "Post1",
                                 Created = DateTime.Now,
                                 Tags = new List<Oxite.Models.Tag>()
                             };
            Oxite.Models.Post post2 = new Oxite.Models.Post()
                             {
                                 ID = Guid.NewGuid(),
                                 Creator = new Oxite.Models.User(),
                                 Area = new Oxite.Models.Area() { ID = areaID },
                                 Slug = "Post2",
                                 Created = DateTime.Now,
                                 Tags = new List<Oxite.Models.Tag>()
                             };

            postService.AddedPosts.AddRange(new[] { post1, post2 });

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            Post[] posts = service.GetRecentPosts(areaID.ToString(), "test", "test", 2);

            Assert.NotNull(posts);
            Assert.Equal(2, posts.Length);
            Assert.Contains<Guid>(post1.ID, posts.Select(p => new Guid(p.postid)));
            Assert.Contains<Guid>(post2.ID, posts.Select(p => new Guid(p.postid)));
        }

        [Fact]
        public void GetRecentPostsFaultsOnNullUser()
        {
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.GetRecentPosts(Guid.NewGuid().ToString(), null, null, 0));
        }

        #endregion

        #region NewMediaObject

        //[Fact]
        //public void NewMediaObjectSavesFile()
        //{
        //    FakeResourceRepository resourceRepository = new FakeResourceRepository();
        //    Guid userID = Guid.NewGuid();
        //    Guid areaID = Guid.NewGuid();

        //    RouteCollection routes = new RouteCollection();
        //    routes.MapRoute("UserFile", "Users/{username}/Files/{*filePath}");

        //    MetaWeblogService service = new MetaWeblogService(
        //        () => routes,
        //        new FakeHttpContextWrapper(new FakeHttpContext(new Uri("http://testhost/metaweblogapi"),
        //                                                       "~/metaweblogapi")),
        //        new FakeConfig(new FakeSite() {ID = Guid.NewGuid()}),
        //        new FakeAreaRepository(new FakeArea() {ID = areaID, Name = "Blog1"}),
        //        resourceRepository,
        //        new FakeMembershipRepository(new FakeUser() {ID = userID, Username = "User1"}),
        //        null,
        //        null);

        //    UrlData url = service.NewMediaObject(areaID.ToString(), "User1", null,
        //                                         new FileData()
        //                                         {bits = Enumerable.Empty<byte>().ToArray(), name = "test.txt"});

        //    Assert.Equal(true, resourceRepository.Saved);
        //    Assert.Equal("test.txt", resourceRepository.AddedFiles[0].Name);
        //    Assert.Equal(userID, resourceRepository.AddedUserFiles[0].Key);
        //    Assert.Equal("http://testhost/Users/User1/Files/test.txt", url.url);
        //}

        //[Fact]
        //public void NewMediaObjectFaultsForNonParsableBlogID()
        //{
        //    MetaWeblogService service = new MetaWeblogService(null, null, null, new FakeAreaRepository(), null,
        //                                                      new FakeMembershipRepository(new FakeUser()
        //                                                                                   {ID = Guid.NewGuid()}), null,
        //                                                      null);
        //    XmlRpcFaultException expectedFault = null;
        //    try
        //    {
        //        service.NewPost("asdf", null, null, default(Post), false);
        //    }
        //    catch (XmlRpcFaultException fault)
        //    {
        //        expectedFault = fault;
        //    }

        //    Assert.NotNull(expectedFault);
        //}

        //[Fact]
        //public void NewMediaObjectFaultsForNoBlogWithPassedBlogID()
        //{
        //    MetaWeblogService service = new MetaWeblogService(null, null, null, new FakeAreaRepository(), null,
        //                                                      new FakeMembershipRepository(new FakeUser()
        //                                                                                   {ID = Guid.NewGuid()}), null,
        //                                                      null);

        //    XmlRpcFaultException expectedFault = null;
        //    try
        //    {
        //        service.NewMediaObject(Guid.NewGuid().ToString(), null, null, default(FileData));
        //    }
        //    catch (XmlRpcFaultException fault)
        //    {
        //        expectedFault = fault;
        //    }
        //    Assert.NotNull(expectedFault);
        //}

        //[Fact]
        //public void NewMediaObjectFaultsOnNullUser()
        //{
        //    Guid areaID = Guid.NewGuid();
        //    MetaWeblogService service = new MetaWeblogService(null, null, null,
        //                                                      new FakeAreaRepository(new FakeArea()
        //                                                                             {ID = areaID, Name = "Blog1"}),
        //                                                      null, new FakeMembershipRepository(), null, null);

        //    XmlRpcFaultException expectedFault = null;
        //    try
        //    {
        //        service.NewMediaObject(areaID.ToString(), null, null, default(FileData));
        //    }
        //    catch (XmlRpcFaultException fault)
        //    {
        //        expectedFault = fault;
        //    }
        //    Assert.NotNull(expectedFault);
        //}

        //[Fact]
        //public void NewMediaObjectFaultsOnUserNotInAuthorRole()
        //{
        //    FakeMembershipRepository membershipRepository = new FakeMembershipRepository();
        //    FakeUser user = new FakeUser() {ID = Guid.NewGuid()};
        //    membershipRepository.Users.Add(user);
        //    FakeRole role = new FakeRole() {ID = Guid.NewGuid()};
        //    membershipRepository.Roles.Add(role);

        //    FakeArea area = new FakeArea() {ID = Guid.NewGuid(), Name = "Blog1", Created = DateTime.Now};
        //    FakeAreaRepository areaRepository = new FakeAreaRepository(area);

        //    membershipRepository.AreaRoles.Add(new KeyValuePair<Guid, Guid>(area.ID, role.ID));

        //    MetaWeblogService service = new MetaWeblogService(null, null, null, areaRepository, null,
        //                                                      membershipRepository, null, null);

        //    XmlRpcFaultException expectedException = null;
        //    try
        //    {
        //        service.NewMediaObject(area.ID.ToString(), null, null, default(FileData));
        //    }
        //    catch (XmlRpcFaultException fault)
        //    {
        //        expectedException = fault;
        //    }
        //    Assert.NotNull(expectedException);
        //}

        #endregion

        #region DeletePost

        [Fact]
        public void DeletePostDeletesPost()
        {
            Guid postID = Guid.NewGuid();

            FakePostService postService = new FakePostService();
            FakeUserService userService = new FakeUserService();

            postService.AddedPosts.Add(new Oxite.Models.Post() { ID = postID, State = Oxite.Models.EntityState.Normal });

            MetaWeblogAPI service = new MetaWeblogAPI(postService, null, userService, null, null);

            bool ret = service.DeletePost(null, postID.ToString(), "test", "test", false);

            Assert.True(ret);
            Assert.Equal(1, postService.RemovedPosts.Count);
            Assert.Equal(postID, postService.RemovedPosts[0]);
        }

        [Fact]
        public void DeletePostFaultsForNullUser()
        {
            MetaWeblogAPI service = new MetaWeblogAPI(null, null, null, null, null);

            Assert.Throws<ArgumentException>(() => service.DeletePost(Guid.NewGuid().ToString(), null, null, null, false));
        }

        #endregion
    }
}