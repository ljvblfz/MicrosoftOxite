//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Modules.Blogs.Models
{
    public class OneMonthDateRangeAddress : DateRangeAddress
    {
        public OneMonthDateRangeAddress(int year, int month)
            : base(new DateTime(year, month, 1), new DateTime(year, month, 1).AddMonths(1))
        {
        }
    }
}
