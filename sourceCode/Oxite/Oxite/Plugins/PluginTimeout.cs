//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Plugins
{
    public class PluginTimeout
    {
        private DateTime limit;

        public PluginTimeout(TimeSpan length)
        {
            limit = DateTime.UtcNow + length;
        }

        public bool Expired
        {
            get
            {
                return DateTime.UtcNow > limit;
            }
        }
    }
}
