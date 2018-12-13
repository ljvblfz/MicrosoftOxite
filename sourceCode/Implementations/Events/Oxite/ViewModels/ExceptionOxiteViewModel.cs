//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.ViewModels
{
    public class ExceptionOxiteViewModel : OxiteViewModel
    {
        public ExceptionOxiteViewModel(OxiteViewModel model, Exception exception)
        {
            SyncViewModel(model);

            Exception = exception;
        }

        public Exception Exception { get; private set; }
    }
}
