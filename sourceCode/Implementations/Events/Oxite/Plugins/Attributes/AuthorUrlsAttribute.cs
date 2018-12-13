//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Plugins.Attributes
{
    public class AuthorUrlsAttribute : DefinitionAttribute
    {
        public AuthorUrlsAttribute(params string[] authorUrls)
            : base(authorUrls)
        {
        }
    }
}
