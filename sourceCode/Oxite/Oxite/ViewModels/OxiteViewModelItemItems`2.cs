//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.ViewModels
{
    public class OxiteViewModelItemItems<T, K> : OxiteViewModelItem<T>
    {
        public OxiteViewModelItemItems(T item, IEnumerable<K> items)
            : base(item)
        {
            Items = items;
        }

        public OxiteViewModelItemItems(T item, IEnumerable<K> items, OxiteViewModel viewModel)
            : base(item)
        {
            SyncViewModel(viewModel);
            Items = items;
        }

        public IEnumerable<K> Items { get; private set; }
    }
}
