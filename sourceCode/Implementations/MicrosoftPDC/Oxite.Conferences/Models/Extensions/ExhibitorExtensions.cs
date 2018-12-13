//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Modules.Conferences.Models.Extensions
{
    public static class ExhibitorExtensions
    {
        public static IEnumerable<ICacheEntity> GetDependencies(this Exhibitor exhibitor)
        {
            List<ICacheEntity> dependencies = new List<ICacheEntity>();

            if (exhibitor == null)
            {
                return dependencies;
            }

            dependencies.Add(exhibitor);

            return dependencies;
        }
    }
}