//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Infrastructure
{
    public class DataFormatFilterCriteria : IFilterCriteria
    {
        private readonly string format;

        public DataFormatFilterCriteria(string format)
        {
            this.format = format;
        }

        #region IFilterCriteria Members

        public bool Match(FilterRegistryContext context)
        {
            return string.Equals(format, context.ControllerContext.RouteData.Values["dataFormat"] as string, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
