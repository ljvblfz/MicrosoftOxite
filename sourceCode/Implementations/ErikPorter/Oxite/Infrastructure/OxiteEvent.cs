//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public class OxiteEvent
    {
        public OxiteEvent(string name)
        {
            Name = name;
            Methods = new List<Action<object>>(5);
        }

        public string Name { get; private set; }
        public IList<Action<object>> Methods { get; private set; }
    }
}
