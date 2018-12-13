//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;

namespace Oxite.Modules.Conferences.Repositories.SqlServer
{
    public class SqlServerConferencesFileRepository : IConferencesFileRepository
    {
        private readonly OxiteConferencesDataContext context;

        public SqlServerConferencesFileRepository(OxiteConferencesDataContext context)
        {
            this.context = context;
        }

        #region IConferencesFileRepository Members

        public File GetFile(Guid scheduleItemID, string fileUrl)
        {
            return
                getFilesQuery(scheduleItemID)
                .Where(f => f.Url == fileUrl)
                .Select(f => projectFile(f))
                .FirstOrDefault();
        }

        public IEnumerable<File> GetFiles(Guid scheduleItemID)
        {
            return
                getFilesQuery(scheduleItemID)
                .Select(f => projectFile(f))
                .ToArray();
        }

        public File Save(Guid scheduleItemID, File file)
        {
            oxite_File fileToSave = null;
            oxite_Conferences_ScheduleItemFileRelationship scheduleItemFile;

            if (file.ID != Guid.Empty)
                fileToSave = context.oxite_Files.FirstOrDefault(f => f.FileID == file.ID);

            if (fileToSave == null)
            {
                context.oxite_Files.InsertOnSubmit(fileToSave = new oxite_File { FileID = file.ID != Guid.Empty ? file.ID : Guid.NewGuid() });

                scheduleItemFile = new oxite_Conferences_ScheduleItemFileRelationship() { ScheduleItemID = scheduleItemID, FileID = fileToSave.FileID };
            }
            else
            {
                scheduleItemFile = context.oxite_Conferences_ScheduleItemFileRelationships.FirstOrDefault(pfr => pfr.FileID == fileToSave.FileID);

                if (scheduleItemFile != null && scheduleItemFile.ScheduleItemID != scheduleItemID)
                {
                    context.oxite_Conferences_ScheduleItemFileRelationships.DeleteOnSubmit(scheduleItemFile);
                    scheduleItemFile = new oxite_Conferences_ScheduleItemFileRelationship() { ScheduleItemID = scheduleItemID, FileID = fileToSave.FileID };
                }
                else
                    scheduleItemFile = null;
            }

            if (scheduleItemFile != null)
                context.oxite_Conferences_ScheduleItemFileRelationships.InsertOnSubmit(scheduleItemFile);

            fileToSave.Length = file.SizeInBytes;
            fileToSave.MimeType = file.MimeType;
            fileToSave.TypeName = file.TypeName;
            fileToSave.Url = file.Url.ToString();

            context.SubmitChanges();

            return projectFile(fileToSave);
        }

        public bool Remove(Guid scheduleItemID, Guid fileID)
        {
            bool removedFile = false;
            oxite_File foundFile = context.oxite_Files.FirstOrDefault(f => f.FileID == fileID);

            if (foundFile != null)
            {
                oxite_Conferences_ScheduleItemFileRelationship foundScheduleItemFile = context.oxite_Conferences_ScheduleItemFileRelationships.FirstOrDefault(sifr => sifr.ScheduleItemID == scheduleItemID && sifr.FileID == fileID);

                if (foundScheduleItemFile != null)
                {
                    context.oxite_Files.DeleteOnSubmit(foundFile);

                    context.SubmitChanges();

                    removedFile = true;
                }
            }

            return removedFile;
        }

        #endregion

        private IQueryable<oxite_File> getFilesQuery(Guid scheduleItemID)
        {
            return
                from sifr in context.oxite_Conferences_ScheduleItemFileRelationships
                join f in context.oxite_Files on sifr.FileID equals f.FileID
                where sifr.ScheduleItemID == scheduleItemID
                select f;
        }

        private static File projectFile(oxite_File f)
        {
            return new File(f.FileID, f.TypeName, f.MimeType, new Uri(f.Url), f.Length);
        }
    }
}