//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Microsoft.Practices.Unity;
using Oxite.Configuration;

namespace Oxite.Infrastructure
{
    public interface IOxiteDataProvider
    {
        //TODO: (erikpo) Need to wrap up config data into a proxy class instead of passing OxiteConfigurationSection directly
        void ConfigureProvider(OxiteConfigurationSection config, OxiteDataProviderConfigurationElement dataProviderConfig, IUnityContainer container);
    }
}