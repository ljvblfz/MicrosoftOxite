//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace Oxite.Infrastructure
{
    public interface IPluginContext
    {
        void ApplyModelBinders(ModelBinderDictionary modelBinders);
        IUnityContainer Container { get; }
        void EventAdd(string eventName, Action<object> method);
        void Merge(IPlugin plugin);
        void ModelBinders<T>() where T : IRegisterModelBinders;
        IPlugin Plugin { get; }
    }
}
