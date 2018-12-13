//---------------------------------------------------------------------
// <copyright file="OxiteViewModel.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Oxite.Models;

    /// <summary>
    /// Main view model used in Oxite
    /// </summary>
    public class OxiteViewModel
    {
        #region Fields

        private readonly OxiteViewModel parent;
        private readonly Dictionary<Type, object> modelItems;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the OxiteViewModel class.
        /// </summary>
        public OxiteViewModel()
        {
            modelItems = new Dictionary<Type, object>();
        }

        /// <summary>
        /// Initializes a new instance of the OxiteViewModel class.
        /// </summary>
        /// <param name="viewModel">OxiteViewModel instance that is the parent of the newly create instance.</param>
        public OxiteViewModel(OxiteViewModel viewModel)
            : this()
        {
            parent = viewModel;
            
            SyncViewModel(viewModel);
        }

        #endregion

        #region Properties

        public INamedEntity Container { get; set; }
        public SiteViewModel Site { get; set; }

        #endregion

        #region Methods

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
            {
                return modelItems[type];
            }

            return null;
        }

        private IEnumerable<Type> getModelItemTypes()
        {
            return modelItems.Select(kvp => kvp.Key);
        }

        public void RemoveModelItem(Type type)
        {
            if (getModelItemTypes().Any(t => t == type))
                modelItems.Remove(type);
        }

        public void RemoveModelItem<T>() where T : class
        {
            RemoveModelItem(typeof(T));
        }

        /// <summary>
        /// Use the given key to get the localized string.
        /// </summary>
        /// <param name="key">Resource key for the requested localized text.</param>
        /// <returns>Localized text for the given key if text exists.  The given key otherwise.</returns>
        public string Localize(string key)
        {
            return Localize(key, key);
        }

        /// <summary>
        /// Use the given key to get the localized string.
        /// </summary>
        /// <param name="key">Resource key for the requested localized text.</param>
        /// <param name="defaultValue">Default value to return if there is no localized text for the given key.</param>
        /// <returns>Localized text for the given key if text exists.  The default value otherwise.</returns>
        public string Localize(string key, string defaultValue)
        {
            ICollection<Phrase> phrases = GetModelItem<ICollection<Phrase>>();

            if (phrases != null)
            {
                return phrases.Where(p => p.Key == key && p.Language == Site.LanguageDefault).Select(p => p.Value).FirstOrDefault() ?? defaultValue;
            }

            return defaultValue;
        }

        /// <summary>
        /// Set all the properties of the OxiteViewModel to the properties of the given OxiteViewModel instance.
        /// </summary>
        /// <param name="viewModel">OxiteViewModel to use for setting all the properties of the current OxiteViewModel.</param>
        protected void SyncViewModel(OxiteViewModel viewModel)
        {
            if (viewModel == null) return;

            Container = viewModel.Container;
            Site = viewModel.Site;
            
            modelItems.Clear();
            viewModel.getModelItemTypes().ToList().ForEach(mit => AddModelItem(viewModel.GetModelItem(mit)));
        }

        #endregion
    }
}
