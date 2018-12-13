//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Repositories
{
    public interface IPageRepository
    {
        Page GetPage(string slug);
        IEnumerable<Page> GetPages();
        Page Save(Page page);
        bool Remove(Page page);
        ContentItem GetContentItem(Page page, string name);
        IEnumerable<ContentItem> GetContentItems(Guid pageID);
        ContentItem Save(ContentItem contentItem);
    }
}
