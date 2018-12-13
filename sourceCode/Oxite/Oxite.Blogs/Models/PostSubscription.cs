//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Blogs.Models
{
    public class PostSubscription : EntityBase
    {
        public PostSubscription(Guid id, PostSmall post, string userName, string userEmail)
            : base(id)
        {
            Post = post;
            UserName = userName;
            UserEmail = userEmail;
        }

        public PostSmall Post { get; private set; }
        public string UserName { get; private set; }
        public string UserEmail { get; private set; }
    }
}
