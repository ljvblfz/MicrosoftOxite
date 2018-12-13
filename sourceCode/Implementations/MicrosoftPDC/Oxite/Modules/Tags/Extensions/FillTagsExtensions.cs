//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;
using Oxite.Modules.Tags.Infrastructure;
using Oxite.Modules.Tags.Services;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Tags.Extensions
{
    public static class FillTagsExtensions
    {
        public static IPageOfItems<T> FillTags<T>(this IPageOfItems<T> items, ITagService tagService) where T : ITaggedEntity
        {
            foreach (T item in items)
                item.FillTags(tagService);

            return items;
        }

        public static IEnumerable<T> FillTags<T>(this IEnumerable<T> items, ITagService tagService) where T : ITaggedEntity
        {
            foreach (T item in items)
                item.FillTags(tagService);

            return items;
        }

        public static IQueryable<T> FillTags<T>(this IQueryable<T> items, ITagService tagService) where T : ITaggedEntity
        {
            //foreach (T item in items)
            //    item.FillTags(tagService);

            //return items;

            tagService.FillTags(items.Cast<ITaggedEntity>());

            return items;
        }

        public static IEnumerable<Tag> FillTags(this IEnumerable<Tag> tags, ITagService tagService)
        {
            foreach (Tag tag in tags)
                tagService.FillTag(tag);

            return tags;
        }

        public static T FillTags<T>(this T item, ITagService tagService) where T : ITaggedEntity
        {
            tagService.FillTags(item);

            return item;
        }
    }
}
