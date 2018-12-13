//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using Oxite.Modules.Blogs.Models;

namespace Oxite.Plugins.Models
{
    public class PostReadOnly
    {
        public PostReadOnly(Post post, string url)
        {
            Blog = new BlogReadOnly(post.Blog);
            Body = post.Body;
            Excerpt = post.BodyShort;
            Comments = post.Comments.Select(c => new CommentReadOnly(c, "")).ToArray();
            CommentingDisabled = post.CommentingDisabled;
            Created = post.Created;
            Creator = new UserReadOnly(post.Creator);
            Modified = post.Modified;
            IsPublished = post.Published.HasValue && post.Published.Value <= DateTime.UtcNow;
            PublishedDate = post.Published.GetValueOrDefault();
            Slug = post.Slug;
            State = (State)(byte)post.State;
            Tags = post.Tags.Select(t => new TagReadOnly(t)).ToArray();
            Title = post.Title;
            Trackbacks = post.Trackbacks.Select(tb => new TrackbackReadOnly(tb)).ToArray();
            Url = url;
        }

        public BlogReadOnly Blog { get; private set; }
        public string Body { get; private set; }
        public string Excerpt { get; private set; }
        public IEnumerable<CommentReadOnly> Comments { get; private set; }
        public bool CommentingDisabled { get; private set; }
        public DateTime Created { get; private set; }
        public UserReadOnly Creator { get; private set; }
        public DateTime Modified { get; private set; }
        public bool IsPublished { get; private set; }
        public DateTime PublishedDate { get; private set; }
        public string Slug { get; private set; }
        public State State { get; private set; }
        public IEnumerable<TagReadOnly> Tags { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<TrackbackReadOnly> Trackbacks { get; private set; }
        public string Url { get; private set; }
    }
}
