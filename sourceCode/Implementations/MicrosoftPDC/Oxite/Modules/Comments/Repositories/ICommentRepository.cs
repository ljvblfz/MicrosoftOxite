//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Comments.Models;

namespace Oxite.Modules.Comments.Repositories
{
    public interface ICommentRepository
    {
        Comment GetComment(Guid commentID);
        void ChangeState(Guid commentID, EntityState state);
    }
}
