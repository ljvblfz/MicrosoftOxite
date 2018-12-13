//---------------------------------------------------------------------
// <copyright file="OxiteViewModelItem`1.cs" company="Microsoft">
//      This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//      http://www.codeplex.com/oxite/license
// </copyright>
//---------------------------------------------------------------------
namespace Oxite.ViewModels
{
    public class OxiteViewModelItem<T> : OxiteViewModel
    {
        /// <summary>
        /// Initializes a new instance of the OxiteViewModelItem class.
        /// </summary>
        /// <param name="item">Object that the class is holding.</param>
        public OxiteViewModelItem(T item)
        {
            this.Item = item;
        }

        /// <summary>
        /// Initializes a new instance of the OxiteViewModelItem class.
        /// </summary>
        /// <param name="item">Object that the class is holding.</param>
        /// <param name="viewModel">OxiteViewModel instance that the new OxiteViewModelItem class
        /// should take its property settings from.</param>
        public OxiteViewModelItem(T item, OxiteViewModel viewModel)
            : this(item)
        {
            this.SyncViewModel(viewModel);
        }

        /// <summary>
        /// Object that the class is holding.
        /// </summary>
        public T Item { get; private set; }

        //INFO: (erikpo) If there get to be other "settings" for posts that are needed, move this and others into a class
        public bool CommentingDisabled { get; set; }
    }
}
