//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Modules.CMS.Models
{
    public class PageInput
    {
        public PageInput(string templateName, string title, string description, string slug, DateTime? published)
        {
            TemplateName = templateName;
            Title = title;
            Description = description;
            Slug = slug;
            Published = published;
        }

        public PageInput(Page page, PageInput input)
        {
            TemplateName = input != null && !string.IsNullOrEmpty(input.TemplateName) ? input.TemplateName : page.TemplateName;
            Title = input != null && !string.IsNullOrEmpty(input.Title) ? input.Title : page.Title;
            Description = input != null && !string.IsNullOrEmpty(input.Description) ? input.Description : page.Description;
            Slug = input != null && !string.IsNullOrEmpty(input.Slug) ? input.Slug : page.Slug;
            Published = input != null && input.Published.HasValue ? input.Published : page.Published;
        }

        public string TemplateName { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public DateTime? Published { get; private set; }

        public Page ToPage(User creator, Guid siteID)
        {
            return new Page(Guid.Empty, new SiteSmall(siteID), TemplateName, Title, Description, Slug, Published);
        }
    }
}
