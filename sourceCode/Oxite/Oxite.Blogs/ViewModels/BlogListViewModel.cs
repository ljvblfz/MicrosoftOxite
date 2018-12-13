//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.ViewModels
{
    public class BlogListViewModel
    {
        public BlogListViewModel(IEnumerable<Blog> blogs)
        {
            Blogs = blogs;
        }

        public IEnumerable<Blog> Blogs { get; private set; }
    }
}
