//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Modules.CMS.Models;
using System.Collections.Generic;

namespace Oxite.Modules.CMS.Repositories
{
    public interface IPageRepository
    {
        Page GetPage(Guid siteID, string slug);
        IQueryable<Page> GetPages(Guid siteID);
        Page Save(Page page);
        bool Remove(Guid pageID);
        ContentItem GetContentItem(Guid siteID, Guid pageID, string name);
        IQueryable<ContentItem> GetContentItems(Guid siteID, Guid pageID);
        ContentItem Save(ContentItem contentItem);
    }
}
