//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Blogs.Services
{
    public interface IPostService
    {
        IPageOfItems<Post> GetPosts(int pageIndex, int pageSize);
        IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, TagAddress tagAddress);
        IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, BlogAddress blogAddress);
        IPageOfItems<Post> GetPosts(int pageIndex, int pageSize, ArchiveData archive);
        IPageOfItems<Post> GetPostsWithDrafts(int pageIndex, int pageSize);
        IPageOfItems<Post> GetPostsWithDrafts(int pageIndex, int pageSize, BlogAddress blogAddress);
        IEnumerable<Post> GetPosts(DateRangeAddress dateRangeAddress);
        IPageOfItems<Post> GetPostsByFileType(int pageIndex, int pageSize, string typeName);
        IEnumerable<DateTime> GetPostDateGroups();
        Post GetPost(PostAddress postAddress);
        Post GetRandomPost();
        ValidationStateDictionary ValidatePostInput(PostInput postInput);
        ModelResult<Post> AddPost(PostInput postInput, EntityState state);
        ModelResult<Post> AddPost(Blog blog, PostInputForImport postInput);
        ModelResult<Post> EditPost(PostAddress postAddress, PostInput postInput, EntityState state);
        void RemovePost(PostAddress postAddress);
        void RemoveAll(BlogAddress blogAddress);

        IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives();
        IEnumerable<KeyValuePair<ArchiveData, int>> GetArchives(BlogAddress blogAddress);

        ValidationStateDictionary AddTrackback(Post post, Trackback trackback);
        ValidationStateDictionary EditTrackback(Trackback trackback);

        Post GetPost(MetaWeblogPostAddress postAddress);
        void RemovePost(MetaWeblogPostAddress postAddress);
    }
}
