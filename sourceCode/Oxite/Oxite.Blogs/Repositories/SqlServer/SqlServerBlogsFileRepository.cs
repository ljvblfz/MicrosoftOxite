//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Files.Models;

namespace Oxite.Modules.Blogs.Repositories.SqlServer
{
    public class SqlServerBlogsFileRepository : IBlogsFileRepository
    {
        private readonly OxiteBlogsDataContext context;

        public SqlServerBlogsFileRepository(OxiteBlogsDataContext context)
        {
            this.context = context;
        }

        public File GetFile(string url)
        {
            return (
                from f in context.oxite_Files
                where string.Compare(f.Url, url, true) == 0
                select projectFile(f)
                ).FirstOrDefault();
        }

        public IEnumerable<File> GetFiles(Post post)
        {
            return (
                from pfr in context.oxite_Blogs_PostFileRelationships
                join f in context.oxite_Files on pfr.FileID equals f.FileID
                where pfr.PostID == post.ID
                select projectFile(f)
                ).ToArray();
        }

        private static File projectFile(oxite_File f)
        {
            return new File(f.FileID, f.TypeName, f.MimeType, new Uri(f.Url), f.Length);
        }
    }
}