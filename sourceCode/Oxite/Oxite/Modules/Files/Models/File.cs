//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using Oxite.Models;

namespace Oxite.Modules.Files.Models
{
    public class File : EntityBase
    {
        public File(Guid id, string typeName, string mimeType, Uri url, long sizeInBytes)
            : base(id)
        {
            TypeName = typeName;
            MimeType = mimeType;
            Url = url;
            SizeInBytes = sizeInBytes;
        }

        public string TypeName { get; private set; }
        public string MimeType { get; private set; }
        public Uri Url { get; private set; }
        public long SizeInBytes { get; private set; }
    }
}
