//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Modules.Blogs.ViewModels
{
    public class BlogAdminDataViewModel
    {
        public BlogAdminDataViewModel(IEnumerable<Post> posts, IEnumerable<PostComment> comments)
        {
            Posts = posts;
            Comments = comments;
        }

        public IEnumerable<Post> Posts { get; private set; }
        public IEnumerable<PostComment> Comments { get; private set; }
    }
}
