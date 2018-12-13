//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Configuration;

namespace Oxite.Infrastructure
{
    public interface IBackgroundService
    {
        void Initialize(OxiteModuleConfigurationElement moduleConfiguration);
        void Run(OxiteModuleConfigurationElement moduleConfiguration);
        void Unload(OxiteModuleConfigurationElement moduleConfiguration);
    }
}