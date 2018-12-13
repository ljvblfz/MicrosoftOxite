//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Blogs.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.ViewModels
{
    public class Last3HeadlinesViewModel
    {
        public Last3HeadlinesViewModel(IEnumerable<Post> posts)
        {
            Posts = posts;
        }

        public IEnumerable<Post> Posts { get; private set; }
    }
}