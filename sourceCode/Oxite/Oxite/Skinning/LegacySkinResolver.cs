//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace Oxite.Skinning
{
    public class LegacySkinResolver : ISkinResolver
    {
        #region ISkinResolver Members

        public void Resolve(SkinResolverContext context, IList<string> skinPaths)
        {
            if (context.RequestContext.HttpContext.Request.Browser.Browser.Contains("IE"))
            {
                if (context.RequestContext.HttpContext.Request.Browser.MajorVersion == 7)
                {
                    ResolveHacksFolder(skinPaths, "IE7");
                }
                else if (context.RequestContext.HttpContext.Request.Browser.MajorVersion == 6)
                {
                    ResolveHacksFolder(skinPaths, "IE6");
                }
            }
        }

        #endregion

        public static void ResolveHacksFolder(IList<string> skinPaths, string foldername)
        {
            List<string> newSkinPaths = new List<string>(skinPaths.Count);

            foreach (string skinPath in skinPaths)
                newSkinPaths.Add(string.Format("{0}{1}/{2}", skinPath, skinPath.EndsWith("/") ? "Hacks" : "/Hacks", foldername));

            foreach (string skinPath in newSkinPaths)
                skinPaths.Add(skinPath);
        }
    }
}
