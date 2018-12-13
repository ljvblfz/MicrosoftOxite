//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Repositories
{
    public interface IContentItemRepository
    {
        ContentItem GetContentItem(string name);
        IEnumerable<ContentItem> GetContentItems();
        ContentItem Save(ContentItem contentItem);
    }
}
