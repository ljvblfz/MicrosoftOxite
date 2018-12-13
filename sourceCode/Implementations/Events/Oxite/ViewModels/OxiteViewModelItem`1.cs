//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.ViewModels
{
    public class OxiteViewModelItem<T> : OxiteViewModel
    {
        public OxiteViewModelItem(T item)
        {
            Item = item;
        }

        public OxiteViewModelItem(T item, OxiteViewModel viewModel)
            : this(item)
        {
            SyncViewModel(viewModel);
        }

        public T Item { get; private set; }

        //INFO: (erikpo) If there get to be other "settings" for posts that are needed, move this and others into a class
        public bool CommentingDisabled { get; set; }
    }
}
