//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;

namespace Oxite.Infrastructure
{
    public class ViewTrackingStore
    {
        private readonly List<ViewTrackingStoreItem> items;

        public ViewTrackingStore()
        {
            items = new List<ViewTrackingStoreItem>(1000);
        }

        public void EnqueueView(Guid postID, string viewType)
        {
            lock (items)
            {
                ViewTrackingStoreItem foundItem = null;

                foreach (ViewTrackingStoreItem item in items)
                {
                    if (item.PostID == postID && string.Compare(item.ViewType, viewType, true) == 0)
                    {
                        foundItem = item;

                        item.Count++;

                        break;
                    }
                }

                if (foundItem == null)
                    items.Add(new ViewTrackingStoreItem(postID, viewType));
            }
        }

        public bool DequeueView(out ViewTrackingStoreItem view)
        {
            bool dequeuedView = false;

            view = null;

            if (items.Count > 0)
            {
                lock (items)
                {
                    view = items[0];

                    items.RemoveAt(0);

                    dequeuedView = true;
                }
            }

            return dequeuedView;
        }
    }
}
