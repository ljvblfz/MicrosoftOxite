//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Infrastructure;

namespace Oxite.Modules.Conferences.Models.Extensions
{
    public static class ScheduleItemExtensions
    {
        public static IEnumerable<ICacheEntity> GetDependencies(this ScheduleItem scheduleItem)
        {
            List<ICacheEntity> dependencies = new List<ICacheEntity>();

            if (scheduleItem == null)
                return dependencies;

            dependencies.Add(scheduleItem);

            dependencies.Add(scheduleItem.Event);

            dependencies.AddRange(scheduleItem.Tags.Cast<ICacheEntity>());

            return dependencies;
        }
    }
}
