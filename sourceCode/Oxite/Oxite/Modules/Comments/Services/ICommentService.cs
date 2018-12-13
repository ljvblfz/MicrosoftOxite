//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Modules.Comments.Infrastructure;

namespace Oxite.Modules.Comments.Services
{
    public interface ICommentService
    {
        void FillComments(ICommentedEntity entity);
    }
}
