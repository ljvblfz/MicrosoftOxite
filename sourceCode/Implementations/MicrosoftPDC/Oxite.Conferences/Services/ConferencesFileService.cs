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
using Oxite.Modules.Conferences.Models;
using Oxite.Modules.Conferences.Repositories;

namespace Oxite.Modules.Conferences.Services
{
    public class ConferencesFileService : IConferencesFileService
    {
        private readonly IConferencesFileRepository repository;

        public ConferencesFileService(IConferencesFileRepository repository)
        {
            this.repository = repository;
        }

        #region IConferencesFileService Members

        public File GetFile(ScheduleItem scheduleItem, FileAddress fileAddress)
        {
            //TODO: (erikpo) Add caching

            return repository.GetFile(scheduleItem.ID, fileAddress.Url);
        }

        public IEnumerable<File> GetFiles(ScheduleItem scheduleItem)
        {
            //TODO: (erikpo) Add caching

            return repository.GetFiles(scheduleItem.ID);
        }

        public ModelResult<File> AddFile(ScheduleItem scheduleItem, FileInput fileInput)
        {
            File file = repository.Save(scheduleItem.ID, new File(Guid.Empty, fileInput.TypeName, fileInput.MimeType, new Uri(fileInput.Url), fileInput.SizeInBytes));

            //TODO: (erikpo) Invalidate caching

            return new ModelResult<File>(file, null);
        }

        public ModelResult<File> AddFile(ScheduleItem scheduleItem, FileContentInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public ModelResult<File> EditFile(ScheduleItem scheduleItem, File fileToEdit, FileInput fileInput)
        {
            File file = repository.Save(scheduleItem.ID, new File(fileToEdit.ID, fileInput.TypeName, fileInput.MimeType, new Uri(fileInput.Url), fileInput.SizeInBytes));

            //TODO: (erikpo) Invalidate caching

            return new ModelResult<File>(file, null);
        }

        public ModelResult<File> EditFile(ScheduleItem scheduleItem, File fileToEdit, FileContentInput fileInput)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveFile(ScheduleItem scheduleItem, File fileToRemove)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (scheduleItem != null && fileToRemove != null && repository.Remove(scheduleItem.ID, fileToRemove.ID))
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