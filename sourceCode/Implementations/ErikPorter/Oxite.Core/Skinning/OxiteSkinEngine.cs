//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using System.Web.Hosting;

namespace Oxite.Skinning
{
    public abstract class OxiteSkinEngine : ISkinEngine
    {
        protected static readonly string[] masterLocationFormats = new[]
        {
            "~/Skins/{2}/Views/{1}/{0}{3}",
            "~/Skins/{2}/Views/Shared/{0}{3}",
            "~/Views/{1}/{0}{3}",
            "~/Views/Shared/{0}{3}"
        };

        protected static readonly string[] viewLocationFormats = new[]
        {
            "~/Skins/{2}/Views/{1}/{0}{3}",
            "~/Skins/{2}/Views/Shared/{0}{3}",
            "~/Views/{1}/{0}{3}",
            "~/Views/Shared/{0}{3}",
        };

        private readonly VirtualPathProvider virtualPathProvider;
        protected string masterFileExtensions;
        protected string viewFileExtensions;

        public OxiteSkinEngine(VirtualPathProvider virtualPathProvider)
        {
            this.virtualPathProvider = virtualPathProvider;
        }

        public virtual string FindMasterPath(string name, string folderName, string skin, out string[] locationsSearched)
        {
            return FindPath(masterLocationFormats, masterFileExtensions, name, folderName, skin, out locationsSearched);
        }

        public virtual string FindViewPath(string name, string folderName, string skin, out string[] locationsSearched)
        {
            return FindPath(viewLocationFormats, viewFileExtensions, name, folderName, skin, out locationsSearched);
        }

        protected virtual string FindPath(string[] locationFormats, string fileExtensions, string name, string folderName, string skin, out string[] locationsSearched)
        {
            string[] locationsToSearch = generateLocations(locationFormats, name, folderName, skin, fileExtensions);

            locationsSearched = new string[locationsToSearch.Length];

            for (int i = 0; i < locationsToSearch.Length; i++)
            {
                string location = locationsToSearch[i];

                locationsSearched[i] = location;

                if (virtualPathProvider.FileExists(location))
                    return location;
            }

            return null;
        }

        protected virtual string[] generateLocations(string[] locationFormats, string name, string folderName, string skin, string fileExtensions)
        {
            List<string> locations = new List<string>();

            foreach (string fileExtension in fileExtensions.Split(','))
                foreach (string locationFormat in locationFormats)
                    locations.Add(string.Format(locationFormat, name, folderName, skin, fileExtension));

            return locations.ToArray();
        }
    }
}
