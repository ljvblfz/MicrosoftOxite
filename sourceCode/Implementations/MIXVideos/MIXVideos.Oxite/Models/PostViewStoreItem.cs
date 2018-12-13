using System;

namespace MIXVideos.Oxite.Models
{
    public class PostViewStoreItem
    {
        public PostViewStoreItem(Guid postID, string viewType)
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
