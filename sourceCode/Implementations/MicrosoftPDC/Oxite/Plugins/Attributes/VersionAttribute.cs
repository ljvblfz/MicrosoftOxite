//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Plugins.Attributes
{
    public class VersionAttribute : DefinitionAttribute
    {
        public VersionAttribute(string version)
            : base(version)
        {
        }

        public VersionAttribute(int major, int minor)
            : base(new Version(major, minor))
        {
        }

        public VersionAttribute(int major, int minor, int build)
            : base(new Version(major, minor, build))
        {
        }

        public VersionAttribute(int major, int minor, int build, int revision)
            : base(new Version(major, minor, build, revision))
        {
        }
    }
}
