//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Oxite.BackgroundServices
{
    public class BackgroundServicesExecutor
    {
        private readonly List<BackgroundServiceExecutor> executors;

        public BackgroundServicesExecutor(IUnityContainer container)
        {
            executors = new List<BackgroundServiceExecutor>(5);

            foreach (IBackgroundService backgroundService in container.ResolveAll<IBackgroundService>())
                executors.Add(new BackgroundServiceExecutor(container, backgroundService.GetType()));
        }

        public void Start()
        {
            foreach (BackgroundServiceExecutor executor in executors)
            {
                executor.Start();
            }
        }

        public void Stop()
        {
            foreach (BackgroundServiceExecutor executor in executors)
            {
                executor.Stop();
            }
        }
    }
}
