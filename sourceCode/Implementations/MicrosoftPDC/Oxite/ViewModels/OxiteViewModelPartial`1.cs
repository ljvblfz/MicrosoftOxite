//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.ViewModels
{
    public class OxiteViewModelPartial<T> : OxiteViewModel
    {
        public OxiteViewModelPartial(OxiteViewModel viewModel, T partialModel)
        {
            SyncViewModel(viewModel);
            PartialModel = partialModel;
        }

        public T PartialModel { get; private set; }
    }
}
