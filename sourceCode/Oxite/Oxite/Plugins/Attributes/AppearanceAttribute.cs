//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.UI.WebControls;

namespace Oxite.Plugins.Attributes
{
    public class AppearanceAttribute : PropertyDefinitionAttribute
    {
        public AppearanceAttribute()
            : base(null)
        {
        }

        public string Width { get; set; }
        public string Height { get; set; }

        protected override void EnsureValue()
        {
            Value = new PropertyAppearance() { Width = Width, Height = Height };
        }
    }
}
