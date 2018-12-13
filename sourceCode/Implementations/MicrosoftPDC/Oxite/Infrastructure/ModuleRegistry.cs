//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Oxite.Infrastructure
{
    //public class ModuleRegistry : IModuleRegistry
    //{
    //    private readonly IUnityContainer container;
    //    private readonly List<IOxiteModule> items;

    //    public ModuleRegistry(IUnityContainer container)
    //    {
    //        this.container = container;
    //        items = new List<IOxiteModule>();
    //    }

    //    #region IModuleRegistry Members

    //    public void Clear()
    //    {
    //        items.Clear();
    //    }

    //    public void Add(IOxiteModule module)
    //    {
    //        items.Add(module);
    //    }

    //    public IOxiteModule GetModule(string name)
    //    {
    //        return items.Where(m => string.Compare(m.Name, name, true) == 0).FirstOrDefault();
    //    }

    //    public IList<IOxiteModule> GetModules(Type type)
    //    {
    //        return items.Where(m => m.GetType() == type || m.GetType().IsSubclassOf(type)).ToList();
    //    }

    //    public IList<IOxiteModule> GetModules()
    //    {
    //        return items;
    //    }

    //    #endregion
    //}
}
