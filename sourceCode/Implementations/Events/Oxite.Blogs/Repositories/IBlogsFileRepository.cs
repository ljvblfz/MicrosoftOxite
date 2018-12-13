using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IBlogsFileRepository
    {
        File GetFile(Guid postID, string fileUrl);
        IEnumerable<File> GetFiles(Guid postID);
        File Save(Guid postID, File file);
        bool Remove(Guid postID, Guid fileID);
    }
}