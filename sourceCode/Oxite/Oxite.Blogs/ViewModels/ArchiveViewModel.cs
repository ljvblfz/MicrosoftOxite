//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.ViewModels
{
    public class ArchiveViewModel
    {
        public ArchiveViewModel(IEnumerable<KeyValuePair<ArchiveData, int>> archives, INamedEntity container)
        {
            Archives = archives;
            Container = container;
        }

        public IEnumerable<KeyValuePair<ArchiveData, int>> Archives { get; private set; }
        public INamedEntity Container { get; private set; }
    }
}
