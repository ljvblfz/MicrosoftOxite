//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Threading;
using Microsoft.Practices.Unity;
using Oxite.Configuration;

namespace Oxite.Infrastructure
{
    public class InProcessBackgroundServiceExecutor : IBackgroundServiceExecutor
    {
        private readonly IUnityContainer container;
        private readonly Type backgroundServiceType;
        private readonly OxiteModuleConfigurationElement moduleConfiguration;
        private readonly TimeSpan interval;
        private readonly Timer timer;
        private IBackgroundService current;

        public InProcessBackgroundServiceExecutor(IUnityContainer container, Type backgroundServiceType, OxiteModuleConfigurationElement moduleConfiguration)
        {
            this.container = container;
            this.backgroundServiceType = backgroundServiceType;
            this.moduleConfiguration = moduleConfiguration;
            interval = TimeSpan.FromMinutes(1);
            timer = new Timer(timerCallback);
        }

        public BackgroundServiceExecutorStatus Status { get; private set; }

        public void Start()
        {
            lock (timer)
            {
                if (Status == BackgroundServiceExecutorStatus.Paused || Status == BackgroundServiceExecutorStatus.Stopped)
                {
                    if (Status == BackgroundServiceExecutorStatus.Stopped)
                    {
                        Status = BackgroundServiceExecutorStatus.Starting;

                        current = (IBackgroundService) container.Resolve(backgroundServiceType);
                        current.Initialize(moduleConfiguration);
                    }

                    Status = BackgroundServiceExecutorStatus.Running;

                    current.Run(moduleConfiguration);

                    timer.Change(interval, new TimeSpan(0, 0, 0, 0, -1));
                }
            }
        }

        public void Pause()
        {
            lock (timer)
            {
                if (Status == BackgroundServiceExecutorStatus.Running)
                {
                    //TODO: (erikpo) Instead of calling Stop, fire a pause event to the currently running background service to give it a chance to hault without stopping

                    Stop();
                }
            }
        }

        public void Stop()
        {
            lock (timer)
            {
                if (Status != BackgroundServiceExecutorStatus.Stopped)
                {
                    timer.Change(Timeout.Infinite, Timeout.Infinite);
                    timer.Dispose();

                    current = null;

                    Status = BackgroundServiceExecutorStatus.Stopped;
                }
            }
        }

        private void timerCallback(object state)
        {
            lock (timer)
            {
                //TODO: (erikpo) Instead of eating the exception, log it
                try
                {
                    current.Run(moduleConfiguration);
                }
                catch { }

                timer.Change(interval, new TimeSpan(0, 0, 0, 0, -1));
            }

            //TODO: (erikpo) Once background services have a cancel state and timeout interval, check their state and cancel if appropriate
        }
    }
}