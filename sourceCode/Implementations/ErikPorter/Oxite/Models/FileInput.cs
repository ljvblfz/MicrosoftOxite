//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models
{
    public class FileInput
    {
        public FileInput(string displayName, string url, string mimeType, long sizeInBytes)
        {
            DisplayName = displayName;
            Url = url;
            MimeType = mimeType;
            SizeInBytes = sizeInBytes;
        }

        public string DisplayName { get; private set; }
        public string Url { get; private set; }
        public string MimeType { get; private set; }
        public long SizeInBytes { get; private set; }
    }
}
