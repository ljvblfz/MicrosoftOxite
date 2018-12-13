//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Collections.Generic;
using Oxite.Infrastructure;

namespace OxiteSite.App_Code.Modules.OxiteSite.Skinning
{
    public class ConferenceSkinResolver : ISkinResolver
    {
        private readonly OxiteContext context;

        public ConferenceSkinResolver(OxiteContext context)
        {
            this.context = context;
        }

        #region ISkinResolver Members

        public void Resolve(SkinResolverContext skinResolverContext, IList<string> skinPaths)
        {
            // We want to favor the selected skin over the conferences default layer
            skinPaths.Insert(1, "~/Skins/Conferences");
        }

        #endregion
    }
}