//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Plugins;

namespace Oxite.ViewModels
{
    public class OxiteViewModel
    {
        private readonly Dictionary<Type, object> modelItems;

        public OxiteViewModel()
        {
            modelItems = new Dictionary<Type, object>();
            PluginTemplates = new List<PluginTemplate>();
        }

        public OxiteViewModel(OxiteViewModel viewModel)
            : this()
        {
            Parent = viewModel;
            SyncViewModel(viewModel);
        }

        public OxiteViewModel Parent { get; private set; }
        public INamedEntity Container { get; set; }
        public SiteViewModel Site { get; set; }
        public UserViewModel User { get; set; }
        public IList<PluginTemplate> PluginTemplates { get; private set; }
        public string SignInUrl { get; set; }
        public string SignOutUrl { get; set; }

        public void AddModelItem(object modelItem)
        {
            modelItems[modelItem.GetType()] = modelItem;
        }

        public T GetModelItem<T>() where T : class
        {
            return GetModelItem(typeof(T)) as T;
        }

        public object GetModelItem(Type type)
        {
            if (modelItems.ContainsKey(type))
                return modelItems[type];

            return null;
        }

        internal IEnumerable<Type> GetModelItemTypes()
        {
            return modelItems.Select(kvp => kvp.Key);
        }

        public void RemoveModelItem(Type type)
        {
            if (GetModelItemTypes().Any(t => t == type))
                modelItems.Remove(type);
        }

        public void RemoveModelItem<T>() where T : class
        {
            RemoveModelItem(typeof(T));
        }

        public string Localize(string key)
        {
            return Localize(key, key);
        }

        public string Localize(string key, string defaultValue)
        {
            ICollection<Phrase> phrases = GetModelItem<ICollection<Phrase>>();

            if (phrases != null)
                return phrases.Where(p => p.Key == key && p.Language == Site.LanguageDefault).Select(p => p.Value).FirstOrDefault() ?? defaultValue;

            return defaultValue;
        }

        protected void SyncViewModel(OxiteViewModel viewModel)
        {
            if (viewModel == null) return;

            Container = viewModel.Container;
            Site = viewModel.Site;
            User = viewModel.User;
            PluginTemplates = viewModel.PluginTemplates;
            SignInUrl = viewModel.SignInUrl;
            SignOutUrl = viewModel.SignOutUrl;
            modelItems.Clear();
            viewModel.GetModelItemTypes().ToList().ForEach(mit => AddModelItem(viewModel.GetModelItem(mit)));
        }
    }
}
