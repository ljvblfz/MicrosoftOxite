// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System;

namespace Oxite.Plugins
{
    internal class PluginLoadException : Exception
    {
        public PluginLoadException(string message)
            : base(message) { }
    }
}