//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Plugins.Validators
{
    public class PluginValidatorRange<T> where T : struct
    {
        public T? Start { get; set; }
        public T? End { get; set; }
    }
}
