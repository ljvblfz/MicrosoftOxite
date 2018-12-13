//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Threading;
using Microsoft.Practices.Unity;
using Oxite.Services;

namespace Oxite.Infrastructure
{
    public class BackgroundServiceExecutor
    {
        private readonly Timer timer;
        private readonly IUnityContainer container;
        private readonly Guid pluginID;
        private readonly Type type;

        public BackgroundServiceExecutor(IUnityContainer container, Guid pluginID, Type type)
        {
            this.timer = new Timer(timerCallback);
            this.container = container;
            this.pluginID = pluginID;
            this.type = type;
        }

        public void Start()
        {
            IBackgroundService backgroundService = (IBackgroundService)container.Resolve(type);
            IPlugin plugin = getPlugin();
            TimeSpan interval = getInterval(plugin);

            if (interval.TotalSeconds > 10)
            {
#if DEBUG
                if (plugin.Enabled)
                {
                    backgroundService.Run(plugin.Settings);
                }
#endif

                timer.Change(interval, new TimeSpan(0, 0, 0, 0, -1));
            }
        }

        public void Stop()
        {
            lock (timer)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
                timer.Dispose();
            }
        }

        private void timerCallback(object state)
        {
            lock (timer)
            {
                IBackgroundService backgroundService = (IBackgroundService)container.Resolve(type);
                IPlugin plugin = getPlugin();
                TimeSpan interval = getInterval(plugin);

                if (plugin.Enabled)
                {
                    try
                    {
                        backgroundService.Run(plugin.Settings);
                    }
                    catch
                    {
                    }
                }

                timer.Change(interval, new TimeSpan(0, 0, 0, 0, -1));
            }

            //TODO: (erikpo) Once background services have a cancel state and timeout interval, check their state and cancel if appropriate
        }

        private IPlugin getPlugin()
        {
            IPluginService pluginService = container.Resolve<IPluginService>();
            IPlugin plugin = pluginService.GetPlugin(pluginID);

            return plugin;
        }

        private TimeSpan getInterval(IPlugin plugin)
        {
            return TimeSpan.FromTicks(long.Parse(plugin.Settings["Interval"]));
        }
    }
}