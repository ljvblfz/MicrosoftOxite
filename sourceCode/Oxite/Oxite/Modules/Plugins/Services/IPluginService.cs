//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.Plugins.Models;
using Oxite.Plugins;
using Oxite.Validation;

namespace Oxite.Modules.Plugins.Services
{
    public interface IPluginService
    {
        IEnumerable<Plugin> GetPlugins();
        Plugin GetPlugin(Guid pluginID);
        ValidationStateDictionary ValidatePlugin(PluginContainer pluginContainer);
        Plugin InstallPlugin(PluginInstallInput pluginInstallInput, bool? overrideEnabled);
        ModelResult<Plugin> EditPlugin(Plugin plugin, PluginEditInput pluginEditInput, bool hasCodeChanges);
        void SetPluginEnabled(Plugin plugin, bool enabled);
        void UninstallPlugin(Plugin plugin);
        void ReloadPlugin(Plugin plugin);
        void ReorderPluginOperation(Plugin plugin, string operationType, string operation, int newSequenceNumber);
    }
}
