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
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Plugins.Services
{
    public interface IPluginService
    {
        IEnumerable<Plugin> GetPlugins();
        Plugin GetPlugin(PluginAddress pluginAddress);
        ValidationStateDictionary ValidatePlugin(PluginContainer pluginContainer);
        Plugin InstallPlugin(PluginInstallInput pluginInstallInput, bool? overrideEnabled);
        ModelResult<Plugin> EditPlugin(PluginAddress pluginAddress, PluginEditInput pluginEditInput, bool hasCodeChanges);
        void SetPluginEnabled(PluginAddress pluginAddress, bool enabled);
        void UninstallPlugin(PluginAddress pluginAddress);
        void ReloadPlugin(PluginAddress pluginAddress);
        void ReorderPluginOperation(Guid pluginID, string operationType, string operation, int newSequenceNumber);
    }
}
