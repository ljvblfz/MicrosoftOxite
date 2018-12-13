//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.ViewModels
{
    public class ContentItemViewModel
    {
        public ContentItemViewModel(IEnumerable<ContentItem> contentItems)
        {
            ContentItems = contentItems;
        }

        public IEnumerable<ContentItem> ContentItems { get; private set; }
    }
}
