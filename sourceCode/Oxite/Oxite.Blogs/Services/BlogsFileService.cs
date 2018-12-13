//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Repositories;
using Oxite.Modules.Files.Models;

namespace Oxite.Modules.Blogs.Services
{
    public class BlogsFileService : IBlogsFileService
    {
        private readonly IBlogsFileRepository repository;

        public BlogsFileService(IBlogsFileRepository repository)
        {
            this.repository = repository;
        }

        #region IFileService Members

        public File GetFile(string url)
        {
            return repository.GetFile(url);
        }

        public IEnumerable<File> GetFiles(Post post)
        {
            return repository.GetFiles(post);
        }

        public ModelResult<File> AddFile(Post post, FileInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public ModelResult<File> AddFile(Post post, FileContentInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public ModelResult<File> EditFile(Post post, File file, FileInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public ModelResult<File> EditFile(Post post, File file, FileContentInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveFile(Post post, File file)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
