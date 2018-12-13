//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Linq;
using Oxite.Models;

namespace OxiteSite.App_Code.Modules.OxiteSite.Extensions
{
    public static class FileExtensions
    {
        public static string GetDisplayName(this File file)
        {
            string fileTypeName = file.TypeName;

            switch (fileTypeName.ToLower())
            {
                case "mp3":
                    return "MP3 Audio";
                case "mp4":
                    return "MP4 Video";
                case "wmv":
                    return "Windows Media Video";
                case "wmvstreaming":
                    return "Windows Media Video (Streaming)";
                case "wmvhigh":
                    return "Windows Media Video (High)";
                case "wma":
                    return "Windows Media Audio";
                case "zune":
                    return "Zune Video";
            }

            return fileTypeName;
        }

        public static File GetMediaForFeed(this IList<File> files)
        {
            File file = null;

            if (file == null)
                file = files.Where(f => f.TypeName == "WMVHigh" || f.TypeName == "WMVHigh").FirstOrDefault();

            if (file == null)
                file = files.Where(f => f.TypeName == "WMVStreaming" || f.TypeName == "WMVStreaming").FirstOrDefault();

            if (file == null)
                file = files.Where(f => f.TypeName == "WMV" || f.TypeName == "WMV").FirstOrDefault();

            return file;
        }

        public static File ByTypeName(this IList<File> files, string typeName)
        {
            return files.Where(f => string.Compare(f.TypeName, typeName, true) == 0).FirstOrDefault();
        }
    }
}