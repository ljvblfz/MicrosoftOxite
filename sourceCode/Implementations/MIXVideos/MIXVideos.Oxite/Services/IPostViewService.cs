//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace MIXVideos.Oxite.Services
{
    public interface IPostViewService
    {
        string[] GetViewTypes();
        void AddView(Guid postID, string viewType);
    }
}
