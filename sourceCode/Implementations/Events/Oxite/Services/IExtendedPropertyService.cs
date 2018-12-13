//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Infrastructure;

namespace Oxite.Services
{
    public interface IExtendedPropertyService
    {
        IEnumerable<ExtendedProperty> GetExtendedProperties(params IExtendedPropertyStore[] scopeItems);
        void SaveExtendedProperties(IEnumerable<ExtendedProperty> extendedProperties, params IExtendedPropertyStore[] scopeItems);
    }
}
