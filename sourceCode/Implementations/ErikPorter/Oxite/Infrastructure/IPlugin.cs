//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Oxite.Models;

namespace Oxite.Infrastructure
{
    public interface IPlugin : INamedEntity
    {
        Guid ID { get; }
        string Category { get; }
        new string DisplayName { get; set; }
        bool Enabled { get; set; }
        NameValueCollection Settings { get; }
        IList<Type> BackgroundServices { get; }
    }
}
