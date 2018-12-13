//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Configuration;

namespace Oxite.Infrastructure
{
    public interface IModulesLoaded
    {
        IOxiteModule Load(OxiteConfigurationSection config, OxiteModuleConfigurationElement module);
        IEnumerable<IOxiteModule> GetModules();
        IEnumerable<T> GetModules<T>() where T : IOxiteModule;
        void UnloadModules();
    }
}
