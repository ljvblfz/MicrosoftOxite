//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Mvc;

namespace Oxite.Infrastructure
{
    public interface ISkinResolverRegistry
    {
        ISkinResolver Default { get; set; }
        void Add(ISkinResolver skinResolver);
        IEnumerable<IOxiteViewEngine> GenerateViewEngines(SkinResolverContext context, string skin);
    }
}
