//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Plugins.Validators;

namespace Oxite.Plugins.Attributes
{
    public class StringArrayValidationAttribute : PropertyDefinitionAttribute
    {
        public StringArrayValidationAttribute()
            : base(null, "Validation")
        {
        }

        private string separator = ",";
        public string Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        private bool removeEmptyItems = true;
        public bool RemoveEmptyItems
        {
            get { return removeEmptyItems; }
            set { removeEmptyItems = value; }
        }

        private bool trimItemSpaces = true;
        public bool TrimItemSpaces
        {
            get { return trimItemSpaces; }
            set { trimItemSpaces = value; }
        }

        public int MinCount { get; set; }

        public int MaxCount { get; set; }

        public int MinItemLength { get; set; }

        public int MaxItemLength { get; set; }

        public RegularExpressionMatcher RegularExpressionItemMatcher { get; set; }

        protected override void EnsureValue()
        {
            Value = new StringArrayValidator(Separator, RemoveEmptyItems, TrimItemSpaces, MinCount, MaxCount, MinItemLength, MaxItemLength, RegularExpressionItemMatcher);
        }
    }
}
