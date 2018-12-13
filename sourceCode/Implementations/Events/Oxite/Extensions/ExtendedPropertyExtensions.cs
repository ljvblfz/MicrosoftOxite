//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;

namespace Oxite.Extensions
{
    public static class ExtendedPropertyExtensions
    {
        public static T GetValue<T>(this IEnumerable<ExtendedProperty> extendedProperties, string name)
        {
            ExtendedProperty extendedProperty = extendedProperties.FirstOrDefault(ep => string.Compare(ep.Name, name, true) == 0);

            if (extendedProperty != null)
            {
                if (extendedProperty.Type != typeof(T)) throw new InvalidOperationException(string.Format("The type requested ({0}) does not match the actual type ({1}) of the extended property '{2)'", typeof(T).FullName, extendedProperty.Type.FullName, extendedProperty.Name));

                return (T)extendedProperty.Value;
            }

            return default(T);
        }
    }
}
