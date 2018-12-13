using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxite.Repositories;
using Oxite.Models;

namespace MIXVideos.Oxite.Test
{
    public class FakePostRepository : IPostRepository
    {
        public global::Oxite.Models.Post GetPost(string areaName, string slug)
        {
            return new Post() { Area = new Area() { Name = areaName }, Slug = slug };
        }

        public global::Oxite.Models.Post GetPost(Guid id)
        {
            return new Post() { ID = id };
        }

        #region IPostRepository Members

        public IQueryable<global::Oxite.Models.Post> GetPosts(bool includeDrafts)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(DateTime sinceDate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area, DateTime sinceDate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Tag tag)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Tag tag, DateTime sinceDate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area, Tag tag)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area, Tag tag, DateTime sinceDate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.ArchiveData archive)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.SearchCriteria criteria, DateTime sinceDate)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Post> GetPostsByFileType(string typeName)
        {
            throw new NotImplementedException();
        }

        public IList<global::Oxite.Models.Post> GetPosts(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IList<DateTime> GetPostDateGroups()
        {
            throw new NotImplementedException();
        }

        public void Save(global::Oxite.Models.Post post)
        {
            throw new NotImplementedException();
        }

        public void Remove(global::Oxite.Models.Post post)
        {
            throw new NotImplementedException();
        }

        public void RemoveAll(global::Oxite.Models.Area area)
        {
            throw new NotImplementedException();
        }

        public IQueryable<KeyValuePair<global::Oxite.Models.ArchiveData, int>> GetArchives()
        {
            throw new NotImplementedException();
        }

        public IQueryable<KeyValuePair<global::Oxite.Models.ArchiveData, int>> GetArchives(global::Oxite.Models.Area area)
        {
            throw new NotImplementedException();
        }

        public global::Oxite.Models.Comment GetComment(Guid commentID)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.ParentAndChild<global::Oxite.Models.PostBase, global::Oxite.Models.Comment>> GetComments(bool includePending, bool sortDescending)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.ParentAndChild<global::Oxite.Models.PostBase, global::Oxite.Models.Comment>> GetComments(global::Oxite.Models.Area area)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.Comment> GetComments(global::Oxite.Models.Post post)
        {
            throw new NotImplementedException();
        }

        public IQueryable<global::Oxite.Models.ParentAndChild<global::Oxite.Models.PostBase, global::Oxite.Models.Comment>> GetComments(global::Oxite.Models.Tag tag)
        {
            throw new NotImplementedException();
        }

        public void SaveComment(global::Oxite.Models.Post post, global::Oxite.Models.Comment comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<global::Oxite.Models.TrackbackOutbound> GetUnsentTrackbacks(global::Oxite.Models.Post post)
        {
            throw new NotImplementedException();
        }

        public void SaveTrackbacks(IEnumerable<global::Oxite.Models.TrackbackOutbound> trackbacks)
        {
            throw new NotImplementedException();
        }

        public void RemoveTrackbacks(IEnumerable<global::Oxite.Models.TrackbackOutbound> trackbacks)
        {
            throw new NotImplementedException();
        }

        public void SaveMessages(IEnumerable<global::Oxite.Models.MessageOutbound> messages)
        {
            throw new NotImplementedException();
        }

        public IList<global::Oxite.Models.PostSubscription> GetSubscriptions(global::Oxite.Models.Post post)
        {
            throw new NotImplementedException();
        }

        public bool GetSubscriptionExists(global::Oxite.Models.Post post, global::Oxite.Models.UserBase user)
        {
            throw new NotImplementedException();
        }

        public void AddSubscription(global::Oxite.Models.Post post, global::Oxite.Models.UserBase user)
        {
            throw new NotImplementedException();
        }

        public void SaveTrackback(global::Oxite.Models.Post post, global::Oxite.Models.Trackback trackback)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IPostRepository Members


        public IQueryable<Post> GetPostsWithDrafts(Area area)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
