//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Modules.Conferences.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.ViewModels
{
    public class TagListViewModel
    {
        public TagListViewModel(IEnumerable<ScheduleItemTag> tags)
        {
            Tags = tags;
        }

        public IEnumerable<ScheduleItemTag> Tags { get; private set; }
    }
}