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
using Oxite.Infrastructure;

namespace Oxite.Models
{
    public class PluginContext : IPluginContext
    {
        private IOxiteEvents events;
        private readonly List<Type> modelBinderRegistries;
        private readonly Dictionary<object, object> repositories;
        private readonly Dictionary<object, object> services;

        public PluginContext(IPlugin plugin, IUnityContainer container)
        {
            modelBinderRegistries = new List<Type>(20);
            repositories = new Dictionary<object, object>();
            services = new Dictionary<object, object>();
            Plugin = new Plugin(plugin.ID, plugin.Category, plugin.Name, new NameValueCollection());
            Container = container;
        }

        public IUnityContainer Container { get; private set; }
        public IPlugin Plugin { get; private set; }

        public void EventAdd(string eventName, Action<object> method)
        {
            if (events == null)
                events = Container.Resolve<IOxiteEvents>();

            events.Add(eventName, method);

            Container.RegisterInstance<IOxiteEvents>(events);
        }

        public void ModelBinders<T>() where T : IRegisterModelBinders
        {
            if (!modelBinderRegistries.Contains(typeof(T)))
                modelBinderRegistries.Add(typeof(T));
        }

        public void ApplyModelBinders(ModelBinderDictionary modelBinders)
        {
            foreach (Type modelBinderRegistryType in modelBinderRegistries)
                ((IRegisterModelBinders)Container.Resolve(modelBinderRegistryType)).RegisterModelBinders(modelBinders);
        }

        public void Merge(IPlugin plugin)
        {
            //Settings
            foreach (string key in plugin.Settings.AllKeys)
                Plugin.Settings[key] = plugin.Settings[key];
            plugin.Settings.Clear();
            foreach (string key in Plugin.Settings.AllKeys)
                plugin.Settings[key] = Plugin.Settings[key];

            //Background Services
            foreach (Type type in Plugin.BackgroundServices)
                plugin.BackgroundServices.Add(type);
        }
    }
}
