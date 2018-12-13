//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Repositories
{
    public interface IExtendedPropertyRepository
    {
        IEnumerable<ExtendedProperty> GetExtendedProperties(Guid siteID, IExtendedPropertyStore[] scopeItems);
        void Remove(Guid siteID, IExtendedPropertyStore[] scopeItems);
        void Save(Guid siteID, string name, Type type, object value, IExtendedPropertyStore[] scopeItems);
    }
}
