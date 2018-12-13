//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace Oxite.Models
{
    public class Post : PostBase
    {
        public Area Area { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Trackback> Trackbacks { get; set; }
        public IList<File> Files { get; set; }

        public void AddFile(FileInput fileInput)
        {
            Files.Add(
                new File()
                {
                    TypeName = fileInput.TypeName,
                    Url = new Uri(fileInput.Url),
                    MimeType = fileInput.MimeType,
                    SizeInBytes = fileInput.SizeInBytes
                }
                );
        }

        public void EditFile(FileAddress fileAddress, FileInput fileInput)
        {
            File file = GetFile(fileAddress);

            if (file != null)
            {
                file.TypeName = fileInput.TypeName;
                file.Url = new Uri(fileInput.Url);
                file.MimeType = fileInput.MimeType;
                file.SizeInBytes = fileInput.SizeInBytes;
            }
        }

        public File GetFile(FileAddress fileAddress)
        {
            return Files.Where(f => string.Compare(f.Url.ToString(), fileAddress.Url, true) == 0).FirstOrDefault();
        }

        public void RemoveFile(FileAddress fileAddress)
        {
            File file = GetFile(fileAddress);

            if (file != null)
                Files.Remove(file);
        }
    }
}
