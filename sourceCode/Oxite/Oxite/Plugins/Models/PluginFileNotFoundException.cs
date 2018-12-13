//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.IO;

namespace Oxite.Plugins.Models
{
    public class PluginFileNotFoundException : FileNotFoundException
    {
        public PluginFileNotFoundException(string fileName)
            : this(fileName, null)
        {
        }

        public PluginFileNotFoundException(string fileName, Exception innerException)
            : base("The code file for this installed plugin could not be found", fileName, innerException)
        {
        }
    }
}
