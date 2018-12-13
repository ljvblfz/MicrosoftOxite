//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public class ExtendedPropertyComparer : IEqualityComparer<ExtendedProperty>
    {
        #region IEqualityComparer<ExtendedProperty> Members

        public bool Equals(ExtendedProperty x, ExtendedProperty y)
        {
            return string.Compare(x.Name, y.Name, true) == 0;
        }

        public int GetHashCode(ExtendedProperty obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
