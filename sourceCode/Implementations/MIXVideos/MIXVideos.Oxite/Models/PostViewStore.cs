using System;
using System.Collections.Generic;

namespace MIXVideos.Oxite.Models
{
    public class PostViewStore
    {
        private readonly List<PostViewStoreItem> items;

        public PostViewStore()
        {
            items = new List<PostViewStoreItem>(1000);
        }

        public void EnqueueView(Guid postID, string viewType)
        {
            lock (items)
            {
                PostViewStoreItem foundItem = null;

                foreach (PostViewStoreItem item in items)
                {
                    if (item.PostID == postID && string.Compare(item.ViewType, viewType, true) == 0)
                    {
                        foundItem = item;

                        item.Count++;

                        break;
                    }
                }

                if (foundItem == null)
                    items.Add(new PostViewStoreItem(postID, viewType));
            }
        }

        public bool DequeueView(out PostViewStoreItem view)
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
