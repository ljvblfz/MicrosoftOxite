//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models
{
    public class Dialog
    {
        //TODO: (erikpo) Instead of just passing message, pass something like "LocalizedString" or something that can pass along information so the message can be localized
        public Dialog(string message, DialogFormat format, params DialogButton[] buttons)
        {
            Message = message;
            Format = format;
            Buttons = buttons != null && buttons.Length > 0 ? buttons : DialogButton.Default;
        }

        public string Message { get; private set; }
        public DialogFormat Format { get; private set; }
        public DialogButton[] Buttons { get; private set; }
    }
}
