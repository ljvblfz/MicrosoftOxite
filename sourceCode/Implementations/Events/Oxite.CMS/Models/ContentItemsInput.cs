//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;

namespace Oxite.Modules.CMS.Models
{
    public class ContentItemsInput
    {
        public ContentItemsInput(IEnumerable<ContentItemInput> contentItems)
        {
            ContentItems = contentItems;
        }

        public IEnumerable<ContentItemInput> ContentItems { get; private set; }
    }
}
