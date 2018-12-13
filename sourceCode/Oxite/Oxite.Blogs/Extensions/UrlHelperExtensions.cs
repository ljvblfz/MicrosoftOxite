// --------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// This source code is made available under the terms of the Microsoft Public License (Ms-PL)
// http://www.codeplex.com/oxite/license
// ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Oxite.Extensions;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Visitors;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string Container(this UrlHelper urlHelper, INamedEntity entity)
        {
            PostVisitor vistor = new PostVisitor(urlHelper);

            return vistor.Visit<string>(entity);
        }

        public static string Container(this UrlHelper urlHelper, INamedEntity entity, string dataFormat)
        {
            PostVisitor vistor = new PostVisitor(urlHelper);

            return vistor.Visit<string>(entity, dataFormat);
        }

        public static string ContainerComments(this UrlHelper urlHelper, INamedEntity entity, string dataFormat)
        {
            CommentVisitor vistor = new CommentVisitor(urlHelper);

            return vistor.Visit<string>(entity, dataFormat);
        }

        public static string Pingback(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.AppPath("/Pingback");
        }

        public static string Trackback(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("Trackback", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string Posts(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("PageOfPosts");
        }

        public static string PostsWithDrafts(this UrlHelper urlHelper)
        {
            return urlHelper.PostsWithDrafts(1);
        }

        public static string PostsWithDrafts(this UrlHelper urlHelper, int pageNumber)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();

            if (pageNumber > 1)
                routeValueDictionary["pageNumber"] = string.Format("Page{0}", pageNumber);

            return urlHelper.RouteUrl("PostsWithDrafts", routeValueDictionary);
        }

        public static string Posts(this UrlHelper urlHelper, string dataFormat)
        {
            return urlHelper.RouteUrl("Posts", new { dataFormat });
        }

        public static string Posts(this UrlHelper urlHelper, Blog blog)
        {
            return urlHelper.Posts(blog, 1);
        }

        public static string Posts(this UrlHelper urlHelper, Blog blog, int pageNumber)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(new { blogName = blog.Name });

            if (pageNumber > 1)
                routeValueDictionary["pageNumber"] = string.Format("Page{0}", pageNumber);

            return urlHelper.RouteUrl("PageOfPostsByBlog", routeValueDictionary);
        }

        public static string Posts(this UrlHelper urlHelper, Blog blog, string dataFormat)
        {
            return urlHelper.RouteUrl("PostsByBlog", new { blogName = blog.Name, dataFormat });
        }

        public static string Tags(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("Tags");
        }

        public static string Posts(this UrlHelper urlHelper, Tag tag)
        {
            return urlHelper.Posts(tag, 1);
        }

        public static string Posts(this UrlHelper urlHelper, Tag tag, int pageNumber)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(new { tagName = tag.Name });

            if (pageNumber > 1)
                routeValueDictionary["pageNumber"] = string.Format("Page{0}", pageNumber);

            return urlHelper.RouteUrl("PageOfPostsByTag", routeValueDictionary);
        }

        public static string Posts(this UrlHelper urlHelper, Tag tag, string dataFormat)
        {
            return urlHelper.RouteUrl("PostsByTag", new { tagName = tag.Name, dataFormat });
        }

        public static string Posts(this UrlHelper urlHelper, int year, int month, int day)
        {
            return urlHelper.RouteUrl("PostsByArchive", new { archiveData = string.Format("{0}/{1}/{2}", year, month, day) });
        }

        public static string Posts(this UrlHelper urlHelper, int year, int month)
        {
            return urlHelper.RouteUrl("PostsByArchive", new { archiveData = string.Format("{0}/{1}", year, month) });
        }

        public static string Posts(this UrlHelper urlHelper, int year)
        {
            return urlHelper.RouteUrl("PostsByArchive", new { archiveData = string.Format("{0}", year) });
        }

        public static string Post(this UrlHelper urlHelper, PostSmall postSmall)
        {
            return urlHelper.RouteUrl("Post", new { blogName = postSmall.BlogName, postSlug = postSmall.Slug, dataFormat = "" });
        }

        public static string Post(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("Post", new { blogName = post.Blog != null ? post.Blog.Name : "", postSlug = post.Slug, dataFormat = "" });
        }

        public static string ValidatePostInput(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("ValidatePostInput");
        }

        public static string PostAdd(this UrlHelper urlHelper, Blog blog)
        {
            return urlHelper.PostAdd(blog != null ? blog.Name : null);
        }

        public static string PostAdd(this UrlHelper urlHelper, string blogName)
        {
            return !string.IsNullOrEmpty(blogName)
                ? urlHelper.RouteUrl("PostAddToBlog", new { blogName })
                : urlHelper.RouteUrl("PostAddToSite");
        }

        public static string PostEdit(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("PostEdit", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string PostRemove(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("PostRemove", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string FilesByPost(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("FilesByPost", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string AddFileContentToPost(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("AddFileContentToPost", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string AddFileToPost(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("AddFileToPost", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string EditFileOnPost(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("EditFileOnPost", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string RemoveFileFromPost(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("RemoveFileFromPost", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string Comments(this UrlHelper urlHelper, string dataFormat)
        {
            return urlHelper.RouteUrl("Comments", new { dataFormat });
        }

        public static string ManageComments(this UrlHelper urlHelper)
        {
            return urlHelper.ManageComments(1);
        }

        public static string ManageComments(this UrlHelper urlHelper, int pageNumber)
        {
            RouteValueDictionary routeValueDictionary = new RouteValueDictionary();

            if (pageNumber > 1)
                routeValueDictionary["pageNumber"] = string.Format("Page{0}", pageNumber);

            return urlHelper.RouteUrl("AllComments", routeValueDictionary);
        }

        public static string ManageComment(this UrlHelper urlHelper, PostComment comment)
        {
            return urlHelper.RouteUrl("AllCommentsPermalink", new { comment = comment.Slug });
        }

        public static string ValidateCommentInput(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("ValidateCommentInput");
        }

        public static string Comments(this UrlHelper urlHelper, Post post, string dataFormat)
        {
            return urlHelper.RouteUrl("CommentsByPost", new { blogName = post.Blog.Name, postSlug = post.Slug, dataFormat });
        }

        public static string Comments(this UrlHelper urlHelper, Blog blog, string dataFormat)
        {
            return urlHelper.RouteUrl("CommentsByBlog", new { blogName = blog.Name, dataFormat });
        }

        public static string Comments(this UrlHelper urlHelper, Tag tag, string dataFormat)
        {
            return urlHelper.RouteUrl("CommentsByTag", new { tagName = tag.Name, dataFormat });
        }

        public static string Comment(this UrlHelper urlHelper, PostComment comment)
        {
            return urlHelper.RouteUrl("PostCommentPermalink", new { blogName = comment.Post.BlogName, postSlug = comment.Post.Slug, commentSlug = comment.Slug });
        }

        public static string Comment(this UrlHelper urlHelper, PostCommentSmall comment, PostSmall post)
        {
            return urlHelper.RouteUrl("PostCommentPermalink", new { blogName = post.BlogName, postSlug = post.Slug, commentSlug = comment.Slug });
        }

        public static string CommentPermalinkReply(this UrlHelper urlHelper, PostComment comment)
        {
            return string.Format("{1}#{0}", comment.Slug, urlHelper.RouteUrl("Post", new { to = comment.Slug, blogName = comment.Post.BlogName, postSlug = comment.Post.Slug }));
        }

        public static string CommentReply(this UrlHelper urlHelper, PostComment comment)
        {
            return string.Format("{0}#reply", urlHelper.RouteUrl("Post", new { to = comment.Slug, blogName = comment.Post.BlogName, postSlug = comment.Post.Slug }));
        }

        public static string CommentPending(this UrlHelper urlHelper, PostComment comment)
        {
            //todo: (nheskew) really want PostCommentForm w/ a query string inserted but that's not going to happen in the near term so hacking together the URL
            return string.Format("{0}#comment", urlHelper.RouteUrl("Post", new { blogName = comment.Post.BlogName, postSlug = comment.Post.Slug, pending = bool.TrueString }));
        }

        public static string CommentOnPost(this UrlHelper urlHelper, PostSmall postSmall)
        {
            return string.Format("{0}#comment", urlHelper.Post(postSmall));
        }

        public static string AddComment(this UrlHelper urlHelper, PostSmall postSmall)
        {
            return urlHelper.RouteUrl("AddCommentToPost", new { blogName = postSmall.BlogName, postSlug = postSmall.Slug });
        }

        public static string AddComment(this UrlHelper urlHelper, Post post)
        {
            return urlHelper.RouteUrl("AddCommentToPost", new { blogName = post.Blog.Name, postSlug = post.Slug });
        }

        public static string RemoveComment(this UrlHelper urlHelper, PostComment comment)
        {
            return urlHelper.RouteUrl("RemovePostComment", new { blogName = comment.Post.BlogName, postSlug = comment.Post.Slug, commentSlug = comment.Slug });
        }

        public static string ApproveComment(this UrlHelper urlHelper, PostComment comment)
        {
            return urlHelper.RouteUrl("ApprovePostComment", new { blogName = comment.Post.BlogName, postSlug = comment.Post.Slug, commentSlug = comment.Slug });
        }

        public static string ManageBlogs(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("ManageBlogs");
        }

        public static string BlogFind(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("BlogFind");
        }

        public static string BlogAdd(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("BlogAdd");
        }

        public static string BlogEdit(this UrlHelper urlHelper, Blog blog)
        {
            return urlHelper.RouteUrl("BlogEdit", new { blogName = blog.Name });
        }

        public static string BlogML(this UrlHelper urlHelper, Blog blog)
        {
            return urlHelper.RouteUrl("BlogML", new { blogName = blog.Name });
        }

        public static string MetaWeblog(this UrlHelper urlHelper)
        {
            return urlHelper.AppPath("/MetaWeblog");
        }

        public static string Rsd(this UrlHelper urlHelper)
        {
            return urlHelper.Rsd((Blog)null);
        }

        public static string Rsd(this UrlHelper urlHelper, Blog blog)
        {
            if (blog != null)
                return urlHelper.Rsd(blog.Name);

            return urlHelper.Rsd((string)null);
        }

        public static string Rsd(this UrlHelper urlHelper, string blogName)
        {
            if (!string.IsNullOrEmpty(blogName))
                return urlHelper.RouteUrl("BlogRsd", new { blogName });

            return urlHelper.RouteUrl("Rsd");
        }

        public static string LiveWriterManifest(this UrlHelper urlHelper)
        {
            return urlHelper.RouteUrl("LiveWriterManifest");
        }
    }
}
