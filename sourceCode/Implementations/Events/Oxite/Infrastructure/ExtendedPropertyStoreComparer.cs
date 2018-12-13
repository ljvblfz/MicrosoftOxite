//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public class ExtendedPropertyStoreComparer : IEqualityComparer<IExtendedPropertyStore>
    {
        #region IEqualityComparer<IExtendedPropertyStore> Members

        public bool Equals(IExtendedPropertyStore x, IExtendedPropertyStore y)
        {
            return x.ScopeType == y.ScopeType && string.Compare(x.ScopeKey, y.ScopeKey, true) == 0;
        }

        public int GetHashCode(IExtendedPropertyStore obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
