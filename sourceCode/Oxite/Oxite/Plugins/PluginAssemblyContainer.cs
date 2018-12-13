//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Reflection;
using System.Collections.Generic;

namespace Oxite.Plugins
{
    public class PluginAssemblyContainer : IEquatable<PluginAssemblyContainer>
    {
        public PluginAssemblyContainer(string virtualPath, Exception exception)
        {
            VirtualPath = virtualPath;
            CompilationException = exception;
            CompilationDate = DateTime.Now;
            IsLoaded = false;
        }

        public PluginAssemblyContainer(string virtualPath, Assembly assembly)
        {
            VirtualPath = virtualPath;
            CompilationAssembly = assembly;
            CompilationDate = DateTime.Now;
            IsLoaded = true;
        }

        public string VirtualPath { get; private set; }
        public Assembly CompilationAssembly { get; private set; }
        public Exception CompilationException { get; private set; }
        public DateTime CompilationDate { get; private set; }
        public bool IsLoaded { get; private set; }

        #region IEquatable<PluginAssemblyContainer> Members

        public bool Equals(PluginAssemblyContainer other)
        {
            return
                string.Compare(VirtualPath, other.VirtualPath, true) == 0 &&
                (CompilationAssembly != null && other.CompilationAssembly != null && (CompilationAssembly.FullName == other.CompilationAssembly.FullName)) &&
                (CompilationException != null && other.CompilationException != null && (CompilationException.ToString() == other.CompilationException.ToString()));
        }

        #endregion
    }

    public class PluginAssemblyContainerComparer : IEqualityComparer<PluginAssemblyContainer>
    {
        #region IEqualityComparer<PluginAssemblyContainer> Members

        public bool Equals(PluginAssemblyContainer x, PluginAssemblyContainer y)
        {
            return string.Compare(x.VirtualPath, y.VirtualPath, true) == 0;
        }

        public int GetHashCode(PluginAssemblyContainer obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
