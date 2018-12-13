//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Threading;
using Microsoft.Practices.Unity;

namespace Oxite.BackgroundServices
{
    public class BackgroundServiceExecutor
    {
        private readonly IUnityContainer container;
        private readonly Type type;
        private readonly Timer timer;

        public BackgroundServiceExecutor(IUnityContainer container, Type type)
        {
            this.container = container;
            this.type = type;
            this.timer = new Timer(timerCallback);
        }

        public void Start()
        {
            IBackgroundService backgroundService = (IBackgroundService)container.Resolve(type);

            backgroundService.RefreshSettings();

            //INFO: (erikpo) This check is just to make sure a value was provided for interval and that they can't put in an interval that will take down their server
            if (backgroundService.Interval.TotalSeconds > 10)
            {
#if DEBUG
                if (backgroundService.Enabled)
                {
                    backgroundService.Run();
                }
#endif

                timer.Change(backgroundService.Interval, new TimeSpan(0, 0, 0, 0, -1));
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

                backgroundService.RefreshSettings();

                if (backgroundService.Enabled)
                {
                    //TODO: (erikpo) Instead of eating the exception, log it
                    try
                    {
                        backgroundService.Run();
                    }
                    catch
                    {
                    }
                }

                timer.Change(backgroundService.Interval, new TimeSpan(0, 0, 0, 0, -1));
            }

            //TODO: (erikpo) Once background services have a cancel state and timeout interval, check their state and cancel if appropriate
        }
    }
}