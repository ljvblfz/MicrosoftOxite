//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class ArchiveContainer : INamedEntity
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public ArchiveData ArchiveData { get; set; }

        public ArchiveContainer(ArchiveData archiveData)
        {
            ArchiveData = archiveData;
        }
    }
}
