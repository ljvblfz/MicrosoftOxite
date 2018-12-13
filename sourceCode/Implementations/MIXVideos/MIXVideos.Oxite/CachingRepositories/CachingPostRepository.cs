using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oxite.Repositories;
using Oxite.Models;

namespace MIXVideos.Oxite.CachingRepositories
{
    public class CachingPostRepository : IPostRepository
    {
        private ICache cache;
        private IPostRepository inner;
        public CachingPostRepository(IPostRepository inner, ICache cache)
        {
            this.cache = cache;
            this.inner = inner;
        }

        private T GetCachedItem<T>(string key, Func<T> retrieve, TimeSpan duration)
        {
            T item = this.cache.Get<T>(key);
            if (item == null)
            {
                item = retrieve();
                if(item != null)
                    this.cache.Add(key, item, duration);
            }

            return item;
        }

        #region IPostRepository Members

        public IQueryable<global::Oxite.Models.Post> GetPosts(bool includeDrafts)
        {
            return inner.GetPosts(includeDrafts);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(DateTime sinceDate)
        {
            return inner.GetPosts(sinceDate);
        }

        public IQueryable<Post> GetPostsWithDrafts(Area area)
        {
            return inner.GetPostsWithDrafts(area);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area)
        {
            return inner.GetPosts(area);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area, DateTime sinceDate)
        {
            return inner.GetPosts(area, sinceDate);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Tag tag)
        {
            return inner.GetPosts(tag);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Tag tag, DateTime sinceDate)
        {
            return inner.GetPosts(tag, sinceDate);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area, Tag tag)
        {
            return inner.GetPosts(area, tag);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.Area area, Tag tag, DateTime sinceDate)
        {
            return inner.GetPosts(area, tag, sinceDate);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.ArchiveData archive)
        {
            return inner.GetPosts(archive);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.SearchCriteria criteria)
        {
            return inner.GetPosts(criteria);
        }

        public IQueryable<global::Oxite.Models.Post> GetPosts(global::Oxite.Models.SearchCriteria criteria, DateTime sinceDate)
        {
            return inner.GetPosts(criteria, sinceDate);
        }

        public IQueryable<global::Oxite.Models.Post> GetPostsByFileType(string typeName)
        {
            return inner.GetPostsByFileType(typeName);
        }

        public IList<global::Oxite.Models.Post> GetPosts(DateTime startDate, DateTime endDate)
        {
            return inner.GetPosts(startDate, endDate);
        }

        public IList<DateTime> GetPostDateGroups()
        {
            return inner.GetPostDateGroups();
        }

        public global::Oxite.Models.Post GetPost(string areaName, string slug)
        {
            string cacheKey = string.Format("Post:Area:{0}:Slug:{1}", areaName, slug);

            return this.GetCachedItem(cacheKey, () => inner.GetPost(areaName, slug), TimeSpan.FromMinutes(1));
        }

        public global::Oxite.Models.Post GetPost(Guid id)
        {
            string cacheKey = string.Format("Post:ID:{0}", id.ToString());

            return this.GetCachedItem(cacheKey, () => inner.GetPost(id), TimeSpan.FromMinutes(1));
        }

        public void Save(global::Oxite.Models.Post post)
        {
            inner.Save(post);
        }

        public void Remove(global::Oxite.Models.Post post)
        {
            inner.Remove(post);
        }

        public void RemoveAll(global::Oxite.Models.Area area)
        {
            inner.RemoveAll(area);
        }

        public IQueryable<KeyValuePair<global::Oxite.Models.ArchiveData, int>> GetArchives()
        {
            return inner.GetArchives();
        }

        public IQueryable<KeyValuePair<global::Oxite.Models.ArchiveData, int>> GetArchives(global::Oxite.Models.Area area)
        {
            return inner.GetArchives(area);
        }

        public global::Oxite.Models.Comment GetComment(Guid commentID)
        {
            return inner.GetComment(commentID);
        }

        public IQueryable<global::Oxite.Models.ParentAndChild<global::Oxite.Models.PostBase, global::Oxite.Models.Comment>> GetComments(bool includePending, bool sortDescending)
        {
            return inner.GetComments(includePending, sortDescending);
        }

        public IQueryable<global::Oxite.Models.ParentAndChild<global::Oxite.Models.PostBase, global::Oxite.Models.Comment>> GetComments(global::Oxite.Models.Area area)
        {
            return inner.GetComments(area);
        }

        public IQueryable<global::Oxite.Models.Comment> GetComments(global::Oxite.Models.Post post)
        {
            return inner.GetComments(post);
        }

        public IQueryable<global::Oxite.Models.ParentAndChild<global::Oxite.Models.PostBase, global::Oxite.Models.Comment>> GetComments(global::Oxite.Models.Tag tag)
        {
            return inner.GetComments(tag);
        }

        public void SaveComment(global::Oxite.Models.Post post, global::Oxite.Models.Comment comment)
        {
            inner.SaveComment(post,comment);
        }

        public IList<global::Oxite.Models.PostSubscription> GetSubscriptions(global::Oxite.Models.Post post)
        {
            return inner.GetSubscriptions(post);
        }

        public bool GetSubscriptionExists(global::Oxite.Models.Post post, global::Oxite.Models.UserBase user)
        {
            return inner.GetSubscriptionExists(post, user);
        }

        public void AddSubscription(global::Oxite.Models.Post post, global::Oxite.Models.UserBase user)
        {
            inner.AddSubscription(post, user);
        }

        public void SaveTrackback(global::Oxite.Models.Post post, global::Oxite.Models.Trackback trackback)
        {
            inner.SaveTrackback(post, trackback);
        }

        #endregion
    }
}
