using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using MIXVideos.Oxite.CachingRepositories;
using Oxite.Models;

namespace MIXVideos.Oxite.Test
{
    public class CachingPostRepositoryTests
    {
        #region GetPost(Guid id)
        [Fact]
        public void GetPostByIDCachesForOneMinute()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            repository.GetPost(Guid.NewGuid());

            Assert.Equal(TimeSpan.FromMinutes(1), cache.Cache.Single().Duration);
        }

        [Fact]
        public void GetPostByIDReturnsPostFromInner()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            Guid postID = Guid.NewGuid();

            Post actual = repository.GetPost(postID);

            Assert.Equal(postID, actual.ID);
        }

        [Fact]
        public void GetPostByIDSetsPostInCache()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            Guid postID = Guid.NewGuid();

            Post actual = repository.GetPost(postID);

            Assert.Equal(postID, ((Post)cache.Cache.Single().Item).ID);
        }

        [Fact]
        public void GetPostByIDSetsCacheKey()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            Guid postID = Guid.NewGuid();

            Post actual = repository.GetPost(postID);

            string expectedKey = string.Format("Post:ID:{0}", postID.ToString());

            Assert.Equal(expectedKey, cache.Cache.Single().Key);
        }

        [Fact]
        public void GetPostByIDReturnsItemFromCache()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            Guid postID = Guid.NewGuid();
            string expectedKey = string.Format("Post:ID:{0}", postID.ToString());

            cache.Cache.Add(new FakeCache.FakeCacheEntry() { Item = new Post() { ID = postID }, Key = expectedKey });

            Post actual = repository.GetPost(postID);

            Assert.Same(cache.Cache.Single().Item, actual);
        } 
        #endregion

        #region GetPost(string areaName, string slug)

        [Fact]
        public void GetPostByNameReturnsInnerPost()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            Post post = repository.GetPost("area", "slug");

            Assert.Equal("area", post.Area.Name);
            Assert.Equal("slug", post.Slug);
        }

        [Fact]
        public void GetPostByNameCachesInnerPost()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            repository.GetPost("area", "slug");

            Assert.Equal("area", ((Post)cache.Cache.Single().Item).Area.Name);
            Assert.Equal("slug", ((Post)cache.Cache.Single().Item).Slug);
            Assert.Equal("Post:Area:area:Slug:slug", cache.Cache.Single().Key);
            Assert.Equal(TimeSpan.FromMinutes(1), cache.Cache.Single().Duration);
        }

        [Fact]
        public void GetPostReturnsItemFromCache()
        {
            FakeCache cache = new FakeCache();
            FakePostRepository inner = new FakePostRepository();

            CachingPostRepository repository = new CachingPostRepository(inner, cache);

            Post cachedPost = new Post();

            cache.Add("Post:Area:area:Slug:slug", cachedPost, TimeSpan.FromMinutes(1));

            Post actualPost = repository.GetPost("area", "slug");

            Assert.Same(cachedPost, actualPost);
        }

        #endregion
    }
}
