//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Files.Models;

namespace Oxite.Modules.Blogs.Repositories
{
    public interface IBlogsFileRepository
    {
        File GetFile(string url);
        IEnumerable<File> GetFiles(Post post);
    }
}