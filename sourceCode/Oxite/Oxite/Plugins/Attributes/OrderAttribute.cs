//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Web.UI.WebControls;

namespace Oxite.Plugins.Attributes
{
    public class OrderAttribute : PropertyDefinitionAttribute
    {
        public OrderAttribute(int value)
            : base(value)
        {
        }
    }
}
