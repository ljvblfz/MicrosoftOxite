//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.CMS.Models;
using Oxite.Services;

namespace Oxite.Modules.CMS.Services
{
    public interface IContentItemService
    {
        IEnumerable<ContentItem> GetContentItems();
        void EditContentItems(IEnumerable<ContentItemInput> contentItems);
    }
}
