//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;

namespace Oxite.Skinning
{
    public class OxiteSkinResolver : ISkinResolver
    {
        private readonly Site site;

        public OxiteSkinResolver(Site site)
        {
            this.site = site;
        }

        #region ISkinResolver Members

        public void Resolve(SkinResolverContext context, IList<string> skinPaths)
        {
            string skinsPath = "~" + site.SkinsPath;
            string[] skinNames = context.Skin.Split('/');
            string skinPath = string.Format("{0}/{1}", skinsPath, skinNames[0]);

            skinPaths.Add(skinPath);

            if (skinNames.Length > 1)
                for (int i = 1; i < skinNames.Length; i++)
                    skinPaths.Add(skinPath = string.Format("{0}/Layers/{1}", skinPath, skinNames[i]));
        }

        #endregion
    }
}
