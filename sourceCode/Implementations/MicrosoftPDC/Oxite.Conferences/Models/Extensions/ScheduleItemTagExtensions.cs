//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Tags.Services;

namespace Oxite.Modules.Conferences.Models.Extensions
{
    public static class ScheduleItemTagExtensions
    {
        public static IEnumerable<ScheduleItemTag> FillTags(this IEnumerable<ScheduleItemTag> tags, ITagService tagService)
        {
            foreach (ScheduleItemTag tag in tags)
                tagService.FillTag(tag);

            return tags;
        }

        public static ScheduleItemTag FillTag(this ScheduleItemTag tag, ITagService tagService)
        {
            tagService.FillTag(tag);

            return tag;
        }
    }
}
