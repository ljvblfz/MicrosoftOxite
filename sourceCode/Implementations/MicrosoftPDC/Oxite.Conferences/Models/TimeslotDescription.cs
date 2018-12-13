//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Conferences.Models
{
    public class TimeslotDescription
    {
        public TimeslotDescription(DateRangeAddress dateRange, string label)
        {
            DateRange = dateRange;
            Label = label;
        }

        public DateRangeAddress DateRange { get; private set; }
        public string Label { get; private set; }
    }
}