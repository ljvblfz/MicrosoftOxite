//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models
{
    public class PostAddress
    {
        public PostAddress(string areaName, string slug)
        {
            AreaName = areaName;
            Slug = slug;
        }

        public string AreaName { get; private set; }
        public string Slug { get; private set; }
    }
}
