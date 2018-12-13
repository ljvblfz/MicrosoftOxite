//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Oxite.Infrastructure;

namespace Oxite.Models
{
    public class Plugin : IPlugin, INamedEntity
    {
        public Plugin(Guid id, string category, string name, NameValueCollection settings)
        {
            ID = id;
            Category = category;
            Name = name;
            Settings = settings;
            BackgroundServices = new List<Type>(5);
        }

        #region IPlugin Members

        public Guid ID
        {
            get;
            private set;
        }

        public string Category
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        public bool Enabled
        {
            get;
            set;
        }

        public NameValueCollection Settings
        {
            get;
            private set;
        }

        public IList<Type> BackgroundServices
        {
            get;
            private set;
        }

        #endregion
    }
}
