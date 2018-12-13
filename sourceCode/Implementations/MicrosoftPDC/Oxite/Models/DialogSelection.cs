//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models
{
    public class DialogSelection : DialogButton
    {
        public DialogSelection(string name, string returnUrl)
            : base(name, null, false, null)
        {
            ReturnUrl = returnUrl;
        }

        public string ReturnUrl { get; private set; }
    }
}
