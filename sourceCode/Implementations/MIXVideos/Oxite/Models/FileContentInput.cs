//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models
{
    public class FileContentInput
    {
        public FileContentInput(string displayName, string mimeType, byte[] contents)
        {
            DisplayName = displayName;
            MimeType = mimeType;
            Contents = contents;
        }

        public string DisplayName { get; private set; }
        public string MimeType { get; private set; }
        public byte[] Contents { get; private set; }
    }
}
