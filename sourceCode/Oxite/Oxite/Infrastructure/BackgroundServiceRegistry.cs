//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Oxite.Configuration;

namespace Oxite.Infrastructure
{
    public class BackgroundServiceRegistry : IBackgroundServiceRegistry
    {
        private readonly IUnityContainer container;
        private readonly List<IBackgroundServiceExecutor> executors;

        public BackgroundServiceRegistry(IUnityContainer container)
        {
            this.container = container;
            executors = new List<IBackgroundServiceExecutor>();
        }

        #region IBackgroundServiceRegistry Members

        public void Clear()
        {
            executors.Clear();
        }

        public void Add<TExecutor, TService>(OxiteModuleConfigurationElement moduleConfiguration, string name, TimeSpan defaultInterval) where TExecutor : class, IBackgroundServiceExecutor where TService : IBackgroundService
        {
            executors.Add((TExecutor)Activator.CreateInstance(typeof(TExecutor), container, typeof(TService), moduleConfiguration));
        }

        public IEnumerable<IBackgroundServiceExecutor> GetBackgroundServices()
        {
            return executors;
        }

        #endregion
    }
}