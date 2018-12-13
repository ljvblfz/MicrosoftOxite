//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;
using System;

namespace Oxite.Modules.Conferences.Models
{
    public class Event : EntityBase, INamedEntity
    {
        public Event(Guid id, string name, string displayName, short year)
            : base(id)
        {
            Name = name;
            DisplayName = displayName;
            Year = year;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public short Year { get; private set; }
    }
}
