//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IPostRepository
    {
        IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, bool includeDrafts);
        IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, Tag tag);
        IPageOfItems<Post> GetPostsWithDrafts(PagingInfo pagingInfo, Blog blog);
        IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, Blog blog);
        IPageOfItems<Post> GetPostsByArchive(PagingInfo pagingInfo, int year, int month, int day);
        IPageOfItems<Post> GetPostsByFiles(PagingInfo pagingInfo, IEnumerable<Guid> files);
        IEnumerable<Post> GetPosts(DateRangeAddress dateRangeAddress);
        IEnumerable<DateTime> GetPostDateGroups();
        Post GetPost(string blogName, string slug);
        Post GetPost(Guid id);
        Post GetRandomPost();
        Post Save(Post post);
        bool RemovePost(Post post);
        void RemoveAllPosts(Blog blog);

        IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives();
        IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives(Blog blog);

        IEnumerable<PostSubscription> GetSubscriptions(Post post);
        void AddSubscription(Post post, Guid creatorUserID);
        void AddSubscription(Post post, PostComment comment);

        void SaveTrackback(Post post, Trackback trackback);

        PostTag Save(Post post, PostTag tag);
    }
}
