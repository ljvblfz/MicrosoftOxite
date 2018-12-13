//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Oxite.Infrastructure;

namespace Oxite.Models
{
    [DataContract]
    public class EntityBase : ICacheEntity, IExtendedPropertyStore
    {
        public EntityBase()
        {
        }

        public EntityBase(Guid id)
        {
            ID = id;
        }

        public Guid ID { get; private set; }

        #region ICacheEntity Members

        public string GetCacheItemKey()
        {
            return string.Format("{0}:{1}", this.GetType().Name, ID.ToString("N"));
        }

        public IEnumerable<ICacheEntity> GetCacheDependencyItems()
        {
            return Enumerable.Empty<ICacheEntity>();
        }

        #endregion

        #region IExtendedPropertyStore Members

        public IEnumerable<ExtendedProperty> ExtendedProperties { get; private set; }

        public string ScopeType
        {
            get { return this.GetType().FullName; }
        }

        public string ScopeKey
        {
            get { return ID.ToString("N"); }
        }

        #endregion
    }
}
