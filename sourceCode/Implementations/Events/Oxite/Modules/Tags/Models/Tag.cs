//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Modules.Tags.Models
{
    public class Tag : INamedEntity, ICacheEntity
    {
        private bool hasBeenFilled;

        public Tag(Guid id)
        {
            ID = id;
        }

        public Tag(string name)
        {
            Name = name;
        }

        public Tag(Guid id, string name, DateTime created)
            : this(name)
        {
            ID = id;
            Created = created;
        }

        public Guid ID { get; private set; }
        public DateTime Created { get; private set; }

        internal void Fill(string name, DateTime created)
        {
            if (!hasBeenFilled)
            {
                Name = name;
                Created = created;

                hasBeenFilled = true;
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(DisplayName) && Name != DisplayName)
                return string.Format("{0} ({1})", DisplayName, Name);

            if (!string.IsNullOrEmpty(Name))
                return Name;

            return ID.ToString();
        }

        #region INamedEntity Members

        public string Name { get; private set; }
        public string DisplayName { get; protected set; }

        #endregion

        #region ICacheEntity Members

        public string GetCacheItemKey()
        {
            return string.Format("Tag:{0:N}", ID);
        }

        public IEnumerable<ICacheEntity> GetCacheDependencyItems()
        {
            //TODO: (erikpo) Not sure what should be returned here, but empty, it should not be.

            return Enumerable.Empty<ICacheEntity>();
        }

        #endregion
    }
}
