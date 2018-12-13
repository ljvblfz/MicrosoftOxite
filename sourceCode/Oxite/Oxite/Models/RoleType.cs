//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Models
{
    [Flags]
    public enum RoleType : byte
    {
        NotSet = 0,
        Site = 1,
        Blog = 2,
        Post = 4,
        Page = 8
    }
}
