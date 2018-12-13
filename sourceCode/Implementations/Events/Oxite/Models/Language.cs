//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Models
{
    public class Language : EntityBase, INamedEntity
    {
        public Language()
        {
        }

        public Language(Guid id)
            : base(id)
        {
        }

        public string Name { get; set; }
        public string DisplayName { get; set; }

        #region INamedEntity Members

        string INamedEntity.Name { get { return Name; } }

        string INamedEntity.DisplayName { get { return DisplayName; } }

        #endregion
    }
}
