//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Infrastructure
{
    public class PartialViewRegistration
    {
        public PartialViewRegistration(string partialView, object model)
        {
            PartialView = partialView;
            Model = model;
        }

        public string PartialView { get; private set; }
        public object Model { get; private set; }
    }
}
