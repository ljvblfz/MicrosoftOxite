//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Reflection;
using Microsoft.Practices.Unity;
using Oxite.Extensions;

namespace Oxite.Infrastructure
{
    //public class OxiteRegisterModules : IRegisterModules
    //{
    //    private readonly IUnityContainer container;
    //    private readonly OxiteConfigurationSection config;

    //    public OxiteRegisterModules(IUnityContainer container)
    //    {
    //        this.container = container;
    //        this.config = container.Resolve<OxiteConfigurationSection>();
    //    }

    //    #region IRegisterModules Members

    //    public void RegisterModules(IModuleRegistry moduleRegistry)
    //    {
    //        if (config != null && config.Modules != null && config.Modules.Enabled)
    //        {
    //            foreach (OxiteModuleConfigurationElement moduleElement in config.Modules)
    //            {
    //                if (moduleElement.Enabled)
    //                {
    //                    string[] typeParts = moduleElement.Type.Split(',');
    //                    string typeName = Assembly.CreateQualifiedName(typeParts[1].Trim(), typeParts[0].Trim());
    //                    Type type = System.Type.GetType(typeName);
    //                    IOxiteModule module = Activator.CreateInstance(
    //                        type,
    //                        type.BuildTypeConstructorParametersFromContainer(p =>
    //                            p.ParameterType != typeof(OxiteModuleConfigurationElement)
    //                            ? container.Resolve(p.ParameterType)
    //                            : moduleElement
    //                            )
    //                        ) as IOxiteModule;

    //                    if (module != null)
    //                        moduleRegistry.Add(module);
    //                }
    //            }
    //        }
    //    }

    //    #endregion
    //}
}
