//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using Oxite.Models;

namespace MIXVideos.Oxite.Models
{
    public class FeedPageContainer : INamedEntity
    {
        public FeedPageContainer(string typeName, string typeDisplayName)
        {
            Name = typeName;
            DisplayName = typeDisplayName;
        }

        #region INamedEntity Members

        public string Name
        {
            get;
            set;
        }

        public string DisplayName
        {
            get;
            set;
        }

        #endregion
    }
}
