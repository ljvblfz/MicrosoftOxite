// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.CMS.Models;

namespace Oxite.Modules.CMS.Extensions
{
    public static class ContentItemsInputExtensions
    {
        public static IEnumerable<ContentItemInput> GetContentItems(this ContentItemsInput contentItemsInput)
        {
            return contentItemsInput != null
                ? contentItemsInput.ContentItems
                : null;
        }
    }
}