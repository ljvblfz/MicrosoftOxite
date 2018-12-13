//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Modules.Comments.Models;

namespace Oxite.Plugins.Models
{
    public class CommentSmallReadOnly
    {
        public CommentSmallReadOnly(CommentSmall commentSmall)
        {
            Created = commentSmall.Created;
            CreatorName = commentSmall.CreatorName;
            CreatorEmailHash = commentSmall.CreatorEmailHash;
            CreatorUrl = commentSmall.CreatorUrl;
        }

        public DateTime Created { get; private set; }
        public string CreatorName { get; private set; }
        public string CreatorEmailHash { get; private set; }
        public string CreatorUrl { get; private set; }
    }
}
