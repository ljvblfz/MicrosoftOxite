//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Comments.Infrastructure
{
    public interface ICommentedEntity
    {
        IEnumerable<Comment> GetComments();
    }
}
