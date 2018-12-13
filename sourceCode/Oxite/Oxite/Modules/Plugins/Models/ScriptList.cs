//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Modules.Plugins.Models
{
    public class ScriptList : List<Script>
    {
        public void Add(string url)
        {
            Add(url, c => true);
        }

        public void Add(string url, string pageName)
        {
            Add(url, c => string.Compare(c.PageName, pageName, true) == 0);
        }

        public void Add(string url, Func<ScriptContext, bool> pageCondition)
        {
            Add(new Script(url, pageCondition));
        }
    }
}
