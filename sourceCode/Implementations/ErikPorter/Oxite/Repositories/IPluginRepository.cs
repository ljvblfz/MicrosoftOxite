//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Repositories
{
    public interface IPluginRepository
    {
        IList<IPlugin> GetPlugins();
        IList<IPlugin> GetPluginsNotInstalled();
        IPlugin GetPlugin(Guid id);
        void Save(IPlugin plugin);
    }
}
