//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace Oxite.Models
{
    public class FileInput
    {
        public FileInput(string typeName, string url, string mimeType, long sizeInBytes)
        {
            TypeName = typeName;
            MimeType = mimeType;
            Url = url;
            SizeInBytes = sizeInBytes;
        }

        public string TypeName { get; private set; }
        public string MimeType { get; private set; }
        public string Url { get; private set; }
        public long SizeInBytes { get; private set; }
    }
}
