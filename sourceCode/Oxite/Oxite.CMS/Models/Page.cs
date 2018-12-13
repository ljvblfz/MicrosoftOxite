//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.Modules.CMS.Models
{
    public class Page : EntityBase
    {
        public Page(Guid id)
            : base(id)
        {
        }

        public Page(Guid id, SiteSmall site, string templateName, string title, string description, string slug, DateTime? published)
            : this(id)
        {
            Site = site;
            TemplateName = templateName;
            Title = title;
            Description = description;
            Slug = slug;
            Published = published;
        }

        public SiteSmall Site { get; private set; }
        public string TemplateName { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public DateTime? Published { get; private set; }
    }
}
