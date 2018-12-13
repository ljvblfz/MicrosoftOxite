//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Plugins;

namespace Oxite.Modules.Plugins.Models
{
    public class ScriptContext : PluginScriptContext
    {
        public ScriptContext(PluginScriptContext context)
            : base(context)
        {
        }
    }
}
