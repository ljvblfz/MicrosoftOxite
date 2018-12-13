//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Models
{
    public class DialogFormat : IEquatable<DialogFormat>
    {
        public DialogFormat(string name)
            : this(name, name)
        {
        }

        public DialogFormat(string name, string cssClass)
        {
            Name = name;
            CssClass = cssClass;
        }

        public string Name { get; private set; }
        public string CssClass { get; private set; }

        public static DialogFormat Info
        {
            get { return new DialogFormat("info"); }
        }

        public static DialogFormat Question
        {
            get { return new DialogFormat("question"); }
        }

        public static DialogFormat Warning
        {
            get { return new DialogFormat("warning"); }
        }

        public static DialogFormat Error
        {
            get { return new DialogFormat("error"); }
        }

        #region IEquatable<DialogFormat> Members

        public bool Equals(DialogFormat other)
        {
            return string.Compare(Name, other.Name, true) == 0;
        }

        #endregion
    }
}
