//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Linq;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Repositories
{
    public interface IContentItemRepository
    {
        ContentItem GetContentItem(Guid siteID, string name);
        IQueryable<ContentItem> GetContentItems(Guid siteID);
        ContentItem Save(Guid siteID, ContentItem contentItem);
    }
}
