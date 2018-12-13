//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Plugins.Models
{
    public class Script
    {
        public Script(string url, Func<ScriptContext, bool> pageCondition)
        {
            Url = url;
            PageCondition = pageCondition;
        }

        public string Url { get; private set; }
        public Func<ScriptContext, bool> PageCondition { get; private set; }
    }
}
