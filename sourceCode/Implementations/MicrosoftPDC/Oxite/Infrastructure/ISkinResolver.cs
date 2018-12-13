//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public interface ISkinResolver
    {
        void Resolve(SkinResolverContext context, IList<string> skinPaths);
    }
}
