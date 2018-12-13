//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Threading;

namespace Oxite.BackgroundServices
{
//    public class BackgroundServiceExecutor
//    {
//        private readonly BackgroundServiceBase backgroundServiceInstance;
//        private readonly Timer timer;

//        public BackgroundServiceExecutor(BackgroundServiceBase instance)
//        {
//            this.backgroundServiceInstance = instance;
//            this.timer = new Timer(timerCallback);
//        }

//        public void Start()
//        {
//#if DEBUG
//            ((BackgroundServiceBase)backgroundServiceInstance.Clone()).Run();
//#endif

//            timer.Change(backgroundServiceInstance.Interval, new TimeSpan(0, 0, 0, 0, -1));
//        }

//        public void Stop()
//        {
//            lock (timer)
//            {
//                timer.Change(Timeout.Infinite, Timeout.Infinite);
//                timer.Dispose();
//            }
//        }

//        private void timerCallback(object state)
//        {
//            lock (timer)
//            {
//                //TODO: (erikpo) Instead of eating the exception, log it
//                try
//                {
//                    ((BackgroundServiceBase)backgroundServiceInstance.Clone()).Run();
//                }
//                catch { }

//                timer.Change(backgroundServiceInstance.Interval, new TimeSpan(0, 0, 0, 0, -1));
//            }

//            //TODO: (erikpo) Once background services have a cancel state and timeout interval, check their state and cancel if appropriate
//        }
//    }
}
