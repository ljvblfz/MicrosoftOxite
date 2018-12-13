//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Specialized;
using Oxite.Services;

namespace Oxite.BackgroundServices
{
    public class BackgroundServiceBase : IBackgroundService
    {
        private NameValueCollection settings;
        private readonly IBackgroundServiceService backgroundServiceService;

        public BackgroundServiceBase() { }

        public BackgroundServiceBase(IBackgroundServiceService backgroundServiceService)
        {
            this.backgroundServiceService = backgroundServiceService;
        }

        #region IBackgroundService Members

        public Guid ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public NameValueCollection Settings
        {
            get
            {
                if (settings == null)
                {
                    settings = backgroundServiceService.LoadSettings(this);

                    if (settings.Count == 0)
                    {
                        OnInitializeSettings();

                        backgroundServiceService.Save(this);
                    }
                }

                return settings;
            }
            set { settings = value; }
        }

        public bool Enabled
        {
            get;
            set;
        }

        public bool ExecuteOnAll
        {
            get
            {
                return bool.Parse(GetSetting("ExecuteOnAll"));
            }
            set
            {
                SaveSetting("ExecuteOnAll", value.ToString());
            }
        }

        public TimeSpan Interval
        {
            get
            {
                return new TimeSpan(long.Parse(GetSetting("Interval")));
            }
            set
            {
                SaveSetting("Interval", value.Ticks.ToString());
            }
        }

        public virtual void Run()
        {
        }

        public void RefreshSettings()
        {
            IBackgroundService backgroundService = backgroundServiceService.GetBackgroundService(this.ID);

            if (backgroundService != null)
                Enabled = backgroundService.Enabled;

            settings = null;
        }

        #endregion

        protected virtual void OnInitializeSettings()
        {
        }

        protected string GetSetting(string name)
        {
            if (Array.IndexOf(Settings.AllKeys, name) == -1)
            {
                throw new ArgumentException(string.Format("No setting with name '{0}' could be found", name), "name");
            }

            return Settings[name];
        }

        protected void SaveSetting(string name, string value)
        {
            backgroundServiceService.SaveSetting(this, name, value);

            Settings[Name] = value;
        }
    }
}
