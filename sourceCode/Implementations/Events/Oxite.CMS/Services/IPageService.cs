//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;
using Oxite.Modules.CMS.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.CMS.Services
{
    public interface IPageService
    {
        Page GetPage(PageAddress pageAddress);
        IEnumerable<Page> GetPages();
        ValidationStateDictionary ValidatePageInput(PageInput pageInput);
        ModelResult<Page> AddPage(PageInput pageInput);
        ModelResult<Page> EditPage(PageAddress pageAddress, PageInput pageInput);
        void RemovePage(PageAddress pageAddress);
        IEnumerable<ContentItem> GetContentItems(Page page);
        void EditPageContent(PageAddress pageAddress, ContentItemsInput contentItemsInput);
    }
}
