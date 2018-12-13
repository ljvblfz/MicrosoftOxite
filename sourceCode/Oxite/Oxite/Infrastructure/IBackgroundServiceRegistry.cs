//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Configuration;

namespace Oxite.Infrastructure
{
    public interface IBackgroundServiceRegistry
    {
        void Clear();
        void Add<TExecutor, TService>(OxiteModuleConfigurationElement moduleConfiguration, string name, TimeSpan defaultInterval)
            where TExecutor : class, IBackgroundServiceExecutor
            where TService : IBackgroundService;
        IEnumerable<IBackgroundServiceExecutor> GetBackgroundServices();
    }
}