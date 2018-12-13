//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Models;

namespace Oxite.Modules.Plugins.Repositories
{
    public interface IPluginRepository
    {
        IQueryable<Plugin> GetPlugins(Guid siteID);
        Plugin GetPlugin(Guid siteID, Guid pluginID);
        void Remove(Guid siteID, Guid pluginID);
        Plugin Save(Plugin plugin);
        void SetEnabled(Guid siteID, Guid pluginID, bool enabled);
    }
}
