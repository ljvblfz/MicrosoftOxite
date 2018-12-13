//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Search.Models
{
    public class SearchCriteria
    {
        public SearchCriteria(string term)
        {
            Term = term;
        }

        public string Term { get; private set; }

        public bool HasCriteria()
        {
            return !string.IsNullOrEmpty(Term);
        }
    }
}
