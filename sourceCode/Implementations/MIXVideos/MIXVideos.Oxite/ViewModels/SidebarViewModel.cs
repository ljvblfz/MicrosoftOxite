//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;

namespace MIXVideos.Oxite.ViewModels
{
    public class SidebarViewModel
    {
        private readonly Post post;
        private readonly IList<Post> posts;

        public SidebarViewModel(Post post, IList<Post> posts)
        {
            this.post = post;
            this.posts = posts;
        }

        public Post Post
        {
            get
            {
                return post;
            }
        }

        public IList<Post> Posts
        {
            get
            {
                return posts;
            }
        }
    }
}
