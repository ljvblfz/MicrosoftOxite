//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.Services
{
    public interface IBlogsFileService
    {
        File GetFile(Post post, FileAddress fileAddress);
        IEnumerable<File> GetFiles(Post post);
        ModelResult<File> AddFile(Post post, FileInput fileInput);
        ModelResult<File> AddFile(Post post, FileContentInput fileInput);
        ModelResult<File> EditFile(Post post, File fileToEdit, FileInput fileInput);
        ModelResult<File> EditFile(Post post, File fileToEdit, FileContentInput fileInput);
        bool RemoveFile(Post post, File fileToRemove);
    }
}
