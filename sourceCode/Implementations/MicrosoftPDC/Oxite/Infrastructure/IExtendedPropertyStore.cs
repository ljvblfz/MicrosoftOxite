//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public interface IExtendedPropertyStore
    {
        IEnumerable<ExtendedProperty> ExtendedProperties { get; }
        string ScopeType { get; }
        string ScopeKey { get; }
    }
}
