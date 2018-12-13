//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Specialized;
using Oxite.Infrastructure;

namespace Oxite.BackgroundServices
{
    //public abstract class BackgroundServiceBase : IOxiteModule, ICloneable
    //{
    //    protected readonly OxiteModuleConfigurationElement config;

    //    public BackgroundServiceBase(OxiteModuleConfigurationElement config)
    //    {
    //        this.config = config;

    //        load();
    //    }

    //    #region IOxiteModule Members

    //    public string Name
    //    {
    //        get;
    //        private set;
    //    }

    //    public bool Enabled
    //    {
    //        get;
    //        private set;
    //    }

    //    public NameValueCollection Settings
    //    {
    //        get;
    //        private set;
    //    }

    //    public abstract void Start();

    //    public abstract void Stop();

    //    #endregion

    //    #region ICloneable Members

    //    public abstract object Clone();

    //    #endregion

    //    public bool ExecuteOnAll
    //    {
    //        get;
    //        protected set;
    //    }

    //    public TimeSpan Interval
    //    {
    //        get;
    //        protected set;
    //    }

    //    public virtual void Run()
    //    {
    //    }

    //    private void load()
    //    {
    //        Name = config.Name;
    //        Enabled = config.Enabled;

    //        if (config.Properties != null && config.Properties.Count > 0)
    //        {
    //            Settings = new NameValueCollection(config.Properties.Count);

    //            foreach (OxiteModulePropertyConfigurationElement propertyElement in config.Properties)
    //                Settings[propertyElement.Name] = propertyElement.Value;
    //        }
    //        else
    //            Settings = new NameValueCollection();

    //        bool? executeOnAll = null;
    //        string executeOnAllString = Settings["ExecuteOnAll"];
    //        if (!string.IsNullOrEmpty(executeOnAllString))
    //        {
    //            bool executeOnAllValue;

    //            if (bool.TryParse(executeOnAllString, out executeOnAllValue))
    //                executeOnAll = executeOnAllValue;
    //        }
    //        ExecuteOnAll = executeOnAll.GetValueOrDefault(true);
    //    }
    //}
}
