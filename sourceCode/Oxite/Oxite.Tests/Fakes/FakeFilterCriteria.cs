//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Infrastructure;

namespace Oxite.Tests.Fakes
{
    public class FakeFilterCriteria : IFilterCriteria
    {
        public bool IsMatch { get; set; }

        #region IFilterCriteria Members

        public bool Match(FilterRegistryContext context)
        {
            return IsMatch;
        }

        #endregion
    }
}
