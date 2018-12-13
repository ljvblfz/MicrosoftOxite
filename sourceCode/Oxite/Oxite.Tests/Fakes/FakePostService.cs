//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Tests.Fakes
{
    public class FakePostService : IPostService
    {
        public List<Post> AddedPosts { get; set; }
        public List<Guid> RemovedPosts { get; set; }
        public Trackback AddedTrackback { get; set; }

        public Dictionary<int, List<Post>> PostPages { get; set; }

        public int LastRequestedPageSize { get; set; }
        public DateTime LastRequestedSinceDate { get; set; }

        public FakePostService()
        {
            AddedPosts = new List<Post>();
            RemovedPosts = new List<Guid>();
            PostPages = new Dictionary<int, List<Post>>();
        }

        public Post GetPost(PostAddress postAddress)
        {
            return AddedPosts.Where(p => p.Slug == postAddress.Slug).FirstOrDefault();
        }

        public Post GetPost(Guid id)
        {
            return AddedPosts.Where(p => p.ID == id).FirstOrDefault();
        }

        public ModelResult<Post> AddPost(Area area, ImportPostInput postInput)
        {
            throw new NotImplementedException();
        }

        public ModelResult<Post> AddPost(PostInput postInput, EntityState state, User creator)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary(typeof(Post), new ValidationState());

            Post post = new Post(null, null, null, false, DateTime.MinValue, creator, Guid.NewGuid(), null, DateTime.MinValue, null, null, state, null, null, null);

            AddedPosts.Add(post);

            return new ModelResult<Post>(post, validationState);
        }

        public void AddPostWithoutTrackbacks(Post post, User currentUser, out ValidationStateDictionary validationState, out Post newPost)
        {
            throw new NotImplementedException();
        }

        public ValidationStateDictionary AddPost(Post post)
        {
            post.ID = Guid.NewGuid();

            AddedPosts.Add(post);

            return new ValidationStateDictionary(typeof(Post), new ValidationState());
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize)
        {
            LastRequestedPageSize = pageSize;
            if (PostPages.ContainsKey(pageIndex))
                return new PageOfItems<Post>(PostPages[pageIndex], 0, PostPages.Count, PostPages.Count);

            return null;
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, DateTime? sinceDate)
        {
            LastRequestedSinceDate = sinceDate ?? default(DateTime);

            return GetPosts(pageIndex, pageSize);
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, AreaAddress areaAddress, DateTime? sinceDate)
        {
            return new PageOfItems<Post>(AddedPosts.Where(p => p.Area.Name == areaAddress.AreaName), pageIndex, pageSize, AddedPosts.Count);
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, Tag tag)
        {
            return null;
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, Tag tag, DateTime? sinceDate)
        {
            return null;
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, SearchCriteria criteria)
        {
            return null;
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, SearchCriteria criteria, DateTime? sinceDate)
        {
            return null;
        }

        public IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, ArchiveData archive)
        {
            return null;
        }

        public IPageOfItems<Post> GetPostsByFileType(int pageIndex, int pageSize, string typeName)
        {
            throw new NotImplementedException();
        }

        public IList<Post> GetPosts(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Post GetRandomPost()
        {
            throw new NotImplementedException();
        }

        public IList<DateTime> GetPostDateGroups()
        {
            throw new NotImplementedException();
        }

        public ModelResult<Post> EditPost(PostAddress postAddress, PostInput postInput, EntityState state, User creator)
        {
            return new ModelResult<Post>(new ValidationStateDictionary(typeof(PostInput), new ValidationState()));
        }

        public ValidationStateDictionary EditPost(Post post)
        {
            return new ValidationStateDictionary();
        }

        public void RemovePost(PostAddress postAddress)
        {
            throw new NotImplementedException();
        }

        public void RemovePost(Guid postID)
        {
            RemovedPosts.Add(postID);
        }

        public void RemoveAll(AreaAddress areaAddress)
        {
            throw new NotImplementedException();
        }

        public ValidationStateDictionary AddTrackback(Post post, Trackback trackback)
        {
            AddedTrackback = trackback;
            return null;
        }

        public ValidationStateDictionary EditTrackback(Trackback trackback)
        {
            AddedTrackback = trackback;
            return null;
        }

        public void RemoveFile(FileAddress fileAddress)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PostSubscription> GetSubscriptions(Post post)
        {
            throw new NotImplementedException();
        }

        #region Unimplemented IPostService Members

        public IPageOfItems<Post> GetPostsWithDrafts(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives(AreaAddress areaAddress)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPostService Members

        public IPageOfItems<Post> GetPostsWithDrafts(int pageIndex, int pageSize, AreaAddress areaAddress)
        {
            throw new NotImplementedException();
        }

        public IPageOfItems<Post> GetPostsWithDrafts(int pageIndex, int pageSize, MetaWeblogAreaAddress areaAddress)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
