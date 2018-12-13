//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;

namespace Oxite.Infrastructure
{
    public class ViewTrackingStoreItem
    {
        public ViewTrackingStoreItem(Guid postID, string viewType)
        {
            PostID = postID;
            ViewType = viewType;
            Count = 1;
        }

        public Guid PostID { get; set; }
        public string ViewType { get; set; }
        public int Count { get; set; }
    }
}
