//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Models
{
    public class DialogButton : IEquatable<DialogButton>
    {
        public DialogButton(string name, string displayName, bool postData)
            : this(name, displayName, postData, name)
        {
        }

        public DialogButton(string name, string displayName, bool postData, string cssClass)
        {
            Name = name;
            DisplayName = displayName;
            PostData = postData;
            CssClass = cssClass;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public bool PostData { get; private set; }
        public string CssClass { get; private set; }

        public static DialogButton FromName(string name)
        {
            return new DialogButton(name, name, false, name);
        }

        public static DialogButton[] Default
        {
            get { return new DialogButton[] { OK }; }
        }

        public static DialogButton OK
        {
            get { return new DialogButton("ok", "OK", true); }
        }

        public static DialogButton Cancel
        {
            get { return new DialogButton("cancel", "Cancel", false); }
        }

        public static DialogButton Apply
        {
            get { return new DialogButton("apply", "Apply", true); }
        }

        public static DialogButton Yes
        {
            get { return new DialogButton("yes", "Yes", true); }
        }

        public static DialogButton No
        {
            get { return new DialogButton("no", "No", false); }
        }

        #region IEquatable<DialogButton> Members

        public bool Equals(DialogButton other)
        {
            return string.Compare(Name, other.Name, true) == 0;
        }

        #endregion
    }
}
