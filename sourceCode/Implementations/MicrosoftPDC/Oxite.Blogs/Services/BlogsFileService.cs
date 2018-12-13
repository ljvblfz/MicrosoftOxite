//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Transactions;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Repositories;

namespace Oxite.Modules.Blogs.Services
{
    public class BlogsFileService : IBlogsFileService
    {
        private readonly IBlogsFileRepository repository;

        public BlogsFileService(IBlogsFileRepository repository)
        {
            this.repository = repository;
        }

        #region IBlogsFileService Members

        public File GetFile(Post post, FileAddress fileAddress)
        {
            //TODO: (erikpo) Add caching

            return repository.GetFile(post.ID, fileAddress.Url);
        }

        public IEnumerable<File> GetFiles(Post post)
        {
            //TODO: (erikpo) Add caching

            return repository.GetFiles(post.ID);
        }

        public ModelResult<File> AddFile(Post post, FileInput fileInput)
        {
            File file = repository.Save(post.ID, new File(Guid.Empty, fileInput.TypeName, fileInput.MimeType, new Uri(fileInput.Url), fileInput.SizeInBytes));

            //TODO: (erikpo) Invalidate caching

            return new ModelResult<File>(file, null);
        }

        public ModelResult<File> AddFile(Post post, FileContentInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public ModelResult<File> EditFile(Post post, File fileToEdit, FileInput fileInput)
        {
            File file = repository.Save(post.ID, new File(fileToEdit.ID, fileInput.TypeName, fileInput.MimeType, new Uri(fileInput.Url), fileInput.SizeInBytes));

            //TODO: (erikpo) Invalidate caching

            return new ModelResult<File>(file, null);
        }

        public ModelResult<File> EditFile(Post post, File fileToEdit, FileContentInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveFile(Post post, File fileToRemove)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (post != null && fileToRemove != null && repository.Remove(post.ID, fileToRemove.ID))
                {
                    //TODO: (erikpo) Invalidate cache

                    transaction.Complete();

                    return true;
                }

                transaction.Complete();
            }

            return false;
        }

        #endregion
    }
}
