//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;

namespace Oxite.ViewModels
{
    public class OxiteViewModelItems<T> : OxiteViewModel
    {
        public OxiteViewModelItems()
            : base()
        {
            Items = Enumerable.Empty<T>();
        }

        public OxiteViewModelItems(IEnumerable<T> items)
            : this()
        {
            Items = items;
        }

        public OxiteViewModelItems(IEnumerable<T> items, OxiteViewModel viewModel)
            : this(items)
        {
            SyncViewModel(viewModel);
        }

        public IEnumerable<T> Items { get; private set; }
    }
}
