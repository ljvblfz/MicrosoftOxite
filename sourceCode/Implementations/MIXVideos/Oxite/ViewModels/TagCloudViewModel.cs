//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Models;

namespace Oxite.ViewModels
{
    public class TagCloudViewModel
    {
        public TagCloudViewModel(IList<KeyValuePair<Tag, int>> tags)
        {
            Tags = tags;
        }

        public IList<KeyValuePair<Tag, int>> Tags { get; private set; }
    }
}
