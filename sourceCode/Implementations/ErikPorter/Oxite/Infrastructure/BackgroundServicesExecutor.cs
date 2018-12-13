//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;

namespace Oxite.Infrastructure
{
    public class BackgroundServicesExecutor
    {
        private readonly List<BackgroundServiceExecutor> executors;

        public BackgroundServicesExecutor(IUnityContainer container)
        {
            executors = new List<BackgroundServiceExecutor>(2);

            foreach (IPlugin plugin in container.Resolve<IPluginRegistry>().GetPlugins())
                foreach (Type type in plugin.BackgroundServices)
                    executors.Add(new BackgroundServiceExecutor(container, plugin.ID, type));
        }

        public void Start()
        {
            foreach (BackgroundServiceExecutor executor in executors)
                executor.Start();
        }

        public void Stop()
        {
            foreach (BackgroundServiceExecutor executor in executors)
                executor.Stop();
        }
    }
}
