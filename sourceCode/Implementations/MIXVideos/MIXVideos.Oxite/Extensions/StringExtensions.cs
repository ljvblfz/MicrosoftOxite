//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------

namespace MIXVideos.Oxite.Extensions
{
    public static class StringExtensions
    {
        public static string GetFileTypeDisplayName(this string fileTypeName)
        {
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
    }
}
