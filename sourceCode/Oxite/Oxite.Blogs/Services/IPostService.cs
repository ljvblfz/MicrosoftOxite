//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;
using Oxite.Validation;

namespace Oxite.Modules.Blogs.Services
{
    public interface IPostService
    {
        IPageOfItems<Post> GetPosts(PagingInfo pagingInfo);
        IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, Tag tag);
        IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, Blog blog);
        IPageOfItems<Post> GetPosts(PagingInfo pagingInfo, ArchiveData archive);
        IPageOfItems<Post> GetPostsWithDrafts(PagingInfo pagingInfo);
        IPageOfItems<Post> GetPostsWithDrafts(PagingInfo pagingInfo, Blog blog);
        IEnumerable<Post> GetPosts(DateRangeAddress dateRangeAddress);
        IPageOfItems<Post> GetPostsByFileType(PagingInfo pagingInfo, string typeName);
        IEnumerable<DateTime> GetPostDateGroups();
        Post GetPost(string blogName, string postSlug);
        Post GetRandomPost();
        ValidationStateDictionary ValidatePostInput(PostInput postInput);
        ModelResult<Post> AddPost(PostInput postInput, EntityState state);
        ModelResult<Post> AddPost(Blog blog, PostInputForImport postInput);
        ModelResult<Post> EditPost(Post post, PostInput postInput, EntityState state);
        void Remove(Post post);
        void RemoveAll(Blog blog);

        IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives();
        IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives(Blog blog);

        ValidationStateDictionary AddTrackback(Post post, Trackback trackback);
        ValidationStateDictionary EditTrackback(Trackback trackback);

        Post GetPost(MetaWeblogPostAddress postAddress);
        void RemovePost(MetaWeblogPostAddress postAddress);
    }
}
