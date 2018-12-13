//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Models
{
    public class Module
    {
        public Module(string name, string type, byte order, bool enabled, bool isSystem)
        {
            Name = name;
            Type = type;
            Order = order;
            Enabled = enabled;
            IsSystem = isSystem;
        }

        public string Name { get; private set; }
        public string Type { get; private set; }
        public byte Order { get; private set; }
        public bool Enabled { get; private set; }
        public bool IsSystem { get; private set; }
    }
}
