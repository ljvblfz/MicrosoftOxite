//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;

namespace Oxite.Modules.Blogs.Repositories.SqlServer
{
    public class SqlServerBlogsFileRepository : IBlogsFileRepository
    {
        private readonly OxiteBlogsDataContext context;

        public SqlServerBlogsFileRepository(OxiteBlogsDataContext context)
        {
            this.context = context;
        }

        #region IBlogsFileRepository Members

        public File GetFile(Guid postID, string fileUrl)
        {
            return
                getFilesQuery(postID)
                .Where(f => f.Url == fileUrl)
                .Select(f => projectFile(f))
                .FirstOrDefault();
        }

        public IEnumerable<File> GetFiles(Guid postID)
        {
            return
                getFilesQuery(postID)
                .Select(f => projectFile(f))
                .ToArray();
        }

        public File Save(Guid postID, File file)
        {
            oxite_File fileToSave = null;
            oxite_Blogs_PostFileRelationship postFile;

            if (file.ID != Guid.Empty)
                fileToSave = context.oxite_Files.FirstOrDefault(f => f.FileID == file.ID);

            if (fileToSave == null)
            {
                context.oxite_Files.InsertOnSubmit(
                    fileToSave = new oxite_File {FileID = file.ID != Guid.Empty ? file.ID : Guid.NewGuid()});

                postFile = new oxite_Blogs_PostFileRelationship() {PostID = postID, FileID = fileToSave.FileID};
            }
            else
            {
                postFile = context.oxite_Blogs_PostFileRelationships.FirstOrDefault(pfr => pfr.FileID == fileToSave.FileID);

                if (postFile != null && postFile.PostID != postID)
                {
                    context.oxite_Blogs_PostFileRelationships.DeleteOnSubmit(postFile);
                    postFile = new oxite_Blogs_PostFileRelationship() { PostID = postID, FileID = fileToSave.FileID };
                }
                else
                    postFile = null;
            }

            if (postFile != null)
                context.oxite_Blogs_PostFileRelationships.InsertOnSubmit(postFile);

            fileToSave.Length = file.SizeInBytes;
            fileToSave.MimeType = file.MimeType;
            fileToSave.TypeName = file.TypeName;
            fileToSave.Url = file.Url.ToString();

            context.SubmitChanges();

            return projectFile(fileToSave);
        }

        public bool Remove(Guid postID, Guid fileID)
        {
            bool removedFile = false;
            oxite_File foundFile = context.oxite_Files.FirstOrDefault(f => f.FileID == fileID);

            if (foundFile != null)
            {
                oxite_Blogs_PostFileRelationship foundPostFile = context.oxite_Blogs_PostFileRelationships.FirstOrDefault(pfr => pfr.PostID == postID && pfr.FileID == fileID);

                if (foundPostFile != null)
                {
                    context.oxite_Files.DeleteOnSubmit(foundFile);

                    context.SubmitChanges();

                    removedFile = true;
                }
            }

            return removedFile;
        }

        #endregion

        private IQueryable<oxite_File> getFilesQuery(Guid postID)
        {
            return
                from pfr in context.oxite_Blogs_PostFileRelationships
                join f in context.oxite_Files on pfr.FileID equals f.FileID
                where pfr.PostID == postID
                select f;
        }

        private static File projectFile(oxite_File f)
        {
            return new File(f.FileID, f.TypeName, f.MimeType, new Uri(f.Url), f.Length);
        }
    }
}