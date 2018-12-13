//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;

namespace Oxite.Modules.CMS.Models
{
    public class ContentItemInput
    {
        public ContentItemInput(string name, string displayName, string body, DateTime? published)
        {
            Name = name;
            DisplayName = displayName;
            Body = body;
            Published = published;
        }

        public ContentItemInput(ContentItem contentItem, ContentItemInput input)
        {
            Name = !string.IsNullOrEmpty(input.Name) ? input.Name : contentItem.Name;
            DisplayName = !string.IsNullOrEmpty(input.DisplayName) ? input.DisplayName : contentItem.DisplayName;
            Body = !string.IsNullOrEmpty(input.Body) ? input.Body : contentItem.Body;
            Published = input.Published.HasValue ? input.Published : contentItem.Published;
        }

        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string Body { get; private set; }
        public DateTime? Published { get; private set; }

        public ContentItem ToContentItem(User creator, Guid siteID)
        {
            return ToContentItem(creator, siteID, Guid.Empty);
        }

        public ContentItem ToContentItem(User creator, Guid siteID, Guid pageID)
        {
            return new ContentItem(siteID, pageID, Name, DisplayName, Body, creator, Published);
        }
    }
}
