//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Text.RegularExpressions;
using Oxite.Infrastructure;

namespace Oxite.Tests.Fakes
{
    public class FakeRegularExpressions : IRegularExpressions
    {
        #region IRegularExpressions Members

        public System.Text.RegularExpressions.Regex GetExpression(string expressionName)
        {
            return new Regex("$^");
        }

        public bool IsMatch(string expressionName, string input)
        {
            throw new NotImplementedException();
        }

        public string Clean(string expressionName, string input)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
