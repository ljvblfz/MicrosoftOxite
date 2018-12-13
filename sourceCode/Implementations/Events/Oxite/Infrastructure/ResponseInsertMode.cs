//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Infrastructure
{
    public enum ResponseInsertMode
    {
        ReplaceWith = 0,
        InsertBefore = 1,
        InsertAfter = 2,
        AppendTo = 3,
        PrependTo = 4,
        Wrap = 5,
        Remove = 6
    }
}
