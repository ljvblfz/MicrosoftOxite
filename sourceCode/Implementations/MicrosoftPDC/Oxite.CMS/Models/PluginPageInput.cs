//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.CMS.Models
{
    public class PluginPageInput
    {
        private readonly PageInput originalInput;

        public PluginPageInput(PageInput pageInput)
        {
            originalInput = pageInput;

            TemplateName = pageInput.TemplateName;
            Title = pageInput.Title;
            Description = pageInput.Description;
            Slug = pageInput.Slug;
            Published = pageInput.Published.HasValue && pageInput.Published.Value <= DateTime.Now;
            if (Published)
                PublishedDate = pageInput.Published.Value;
        }

        public string TemplateName { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public bool Published { get; private set; }
        public DateTime PublishedDate { get; private set; }
        public ContentItemsInput ContentItemsInput { get; private set; }

        public PageInput ToPageInput()
        {
            return new PageInput(TemplateName, Title, Description, Slug, originalInput.Published);
        }
    }
}
