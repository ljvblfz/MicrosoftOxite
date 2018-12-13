//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public class ExtendedPropertyStoreBlank : IExtendedPropertyStore
    {
        private readonly string scopeType;
        private readonly string scopeKey;

        public ExtendedPropertyStoreBlank(string scopeType, string scopeKey)
        {
            this.scopeType = scopeType;
            this.scopeKey = scopeKey;
        }

        #region IExtendedPropertyStore Members

        public IEnumerable<ExtendedProperty> ExtendedProperties
        {
            get { return new List<ExtendedProperty>(); }
        }

        public string ScopeType
        {
            get { return scopeType; }
        }

        public string ScopeKey
        {
            get { return scopeKey; }
        }

        #endregion
    }
}
