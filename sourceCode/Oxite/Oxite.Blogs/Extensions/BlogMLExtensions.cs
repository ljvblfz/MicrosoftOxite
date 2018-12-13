//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BlogML.Xml;
using Oxite.Extensions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Tags.Models;

namespace Oxite.Modules.BlogML.Extensions
{
    public static class BlogMLExtensions
    {
        public static PostInputForImport ToImportPostInput(this BlogMLPost blogMLPost, BlogMLBlog blog, bool commentingDisabled, string slugPattern, EntityState state, User creator)
        {
            string body = blogMLPost.Content.Text;
            string bodyShort = blogMLPost.HasExcerpt ? blogMLPost.Excerpt.Text : "";
            DateTime created = blogMLPost.DateCreated;
            DateTime modified = blogMLPost.DateModified;
            DateTime? published = blogMLPost.DateCreated;
            string title = blogMLPost.Title;

            string slug;
            if (!string.IsNullOrEmpty(slugPattern))
            {
                Regex regex = new Regex(slugPattern);
                Match match = regex.Match(blogMLPost.PostUrl);

                if (match != null && match.Groups != null && match.Groups.Count >= 2)
                    slug = match.Groups[1].Value;
                else
                    slug = blogMLPost.ID;
            }
            else
                slug = !string.IsNullOrEmpty(blogMLPost.PostUrl) ? blogMLPost.PostUrl : blogMLPost.ID;

            List<PostTag> tags = new List<PostTag>();
            if (blogMLPost.Categories != null && blogMLPost.Categories.Count > 0)
            {
                foreach (BlogMLCategoryReference bcr in blogMLPost.Categories)
                {
                    foreach (BlogMLCategory tag in blog.Categories)
                    {
                        if (tag.ID == bcr.Ref)
                        {
                            tags.Add(new PostTag(Guid.NewGuid(), tag.Title, tag.Title, tag.DateCreated));

                            break;
                        }
                    }
                }
            }

            List<Trackback> trackbacks = new List<Trackback>();
            if (blogMLPost.Trackbacks != null && blogMLPost.Trackbacks.Count > 0)
            {
                foreach (BlogMLTrackback tb in blogMLPost.Trackbacks)
                {
                    Trackback trackback = new Trackback
                    {
                        Created = tb.DateCreated,
                        Modified = tb.DateModified,
                        Title = tb.Title,
                        Url = tb.Url,
                        IsTargetInSource = tb.Approved,
                        BlogName = "",
                        Body = "",
                        Source = ""
                    };

                    trackbacks.Add(trackback);
                }
            }

            return new PostInputForImport(body, bodyShort, commentingDisabled, created, creator, modified, published, slug, state, tags, title, trackbacks);
        }

        public static CommentInputForImport ToImportCommentInput(this BlogMLComment blogMLComment, BlogMLBlog blog, User user, Language language)
        {
            string body = blogMLComment.Content.Text;
            DateTime created = blogMLComment.DateCreated;
            DateTime modified = blogMLComment.DateModified;
            long creatorIP = 0;
            string creatorUserAgent = "";
            EntityState state = blogMLComment.Approved ? EntityState.Normal : EntityState.PendingApproval;

            if (blogMLComment.UserEMail == user.Email || blogMLComment.UserEMail == blog.Authors[0].Email)
                return new CommentInputForImport(body, created, user, creatorIP, creatorUserAgent, language, modified, state);
            else
            {
                string creatorName;
                string creatorEmail;
                string creatorEmailHash;
                string creatorUrl;

                if (!string.IsNullOrEmpty(blogMLComment.UserName))
                    creatorName = blogMLComment.UserName.Length > 50 ? blogMLComment.UserName.Substring(0, 50) : blogMLComment.UserName;
                else
                    creatorName = "";

                if (!string.IsNullOrEmpty(blogMLComment.UserEMail))
                {
                    creatorEmail = blogMLComment.UserEMail.Length > 100 ? blogMLComment.UserEMail.Substring(0, 100) : blogMLComment.UserEMail;

                    creatorEmailHash = creatorEmail.ComputeHash();
                }
                else
                    creatorEmail = creatorEmailHash = "";

                if (!string.IsNullOrEmpty(blogMLComment.UserUrl))
                    creatorUrl = blogMLComment.UserUrl.Length > 300 ? blogMLComment.UserUrl.Substring(0, 300) : blogMLComment.UserUrl;
                else
                    creatorUrl = "";

                return new CommentInputForImport(body, created, new UserAnonymous(creatorName, creatorEmail, creatorEmailHash, creatorUrl), creatorIP, creatorUserAgent, language, modified, state);
            }
        }
    }
}
