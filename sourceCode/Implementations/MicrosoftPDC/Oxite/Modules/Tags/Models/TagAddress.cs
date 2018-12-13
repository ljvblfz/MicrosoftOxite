//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Tags.Models
{
    public class TagAddress
    {
        public TagAddress(string tagName)
        {
            TagName = tagName;
        }

        public string TagName { get; private set; }
    }
}
