//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.CMS.Models
{
    public class PageAddress
    {
        public PageAddress(string slug)
        {
            Slug = slug;
        }

        public string Slug { get; private set; }
    }
}
