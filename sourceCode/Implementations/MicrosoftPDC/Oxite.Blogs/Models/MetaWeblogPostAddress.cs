//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Blogs.Models
{
    public class MetaWeblogPostAddress
    {
        public MetaWeblogPostAddress(Guid postID)
        {
            PostID = postID;
        }

        public Guid PostID { get; private set; }
    }
}
