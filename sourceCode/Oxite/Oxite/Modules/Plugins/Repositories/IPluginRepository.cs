//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Modules.Plugins.Repositories
{
    public interface IPluginRepository
    {
        IEnumerable<Plugin> GetPlugins();
        Plugin GetPlugin(Guid pluginID);
        void Remove(Plugin plugin);
        Plugin Save(Plugin plugin);
        void SetEnabled(Plugin plugin, bool enabled);
    }
}
