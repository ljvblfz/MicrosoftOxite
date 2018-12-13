//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Modules.Conferences.Models.Extensions
{
    public static class ScheduleItemUserExtensions
    {
        public static IEnumerable<ICacheEntity> GetDependencies(this ScheduleItemUser scheduleItemUser)
        {
            var dependencies = new List<ICacheEntity>();

            if (scheduleItemUser == null)
            {
                return dependencies;
            }

            dependencies.Add(scheduleItemUser);

            return dependencies;
        }
    }
}
