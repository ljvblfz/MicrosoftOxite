//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Modules.Blogs.Models
{
    public class FileAddress
    {
        public FileAddress(string url)
        {
            Url = url;
        }

        public string Url { get; private set; }
    }
}
