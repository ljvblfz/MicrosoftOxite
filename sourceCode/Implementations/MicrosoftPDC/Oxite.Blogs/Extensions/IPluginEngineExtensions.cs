//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Models;
using Oxite.Plugins.Extensions;
using Oxite.Plugins.Models;

namespace Oxite.Modules.Blogs.Extensions
{
    public static class IPluginEngineExtensions
    {
        public static Post ProcessDisplayOfPost(this IPluginEngine pluginEngine, OxiteContext context, Func<Post> getPost)
        {
            PostForProcessing post = new PostForProcessing(getPost());

            pluginEngine.ExecuteAll("ProcessDisplayOfPost", new { context, post });

            return post.ToPost();
        }

        public static IEnumerable<Post> ProcessDisplayOfPosts(this IPluginEngine pluginEngine, OxiteContext context, Func<IEnumerable<Post>> getPosts)
        {
            IEnumerable<Post> posts = getPosts();
            List<Post> newPosts = new List<Post>();

            foreach (Post post in posts)
                newPosts.Add(pluginEngine.ProcessDisplayOfPost(context, () => post));

            return newPosts;
        }

        public static IPageOfItems<Post> ProcessDisplayOfPosts(this IPluginEngine pluginEngine, OxiteContext context, Func<IPageOfItems<Post>> getPosts)
        {
            IPageOfItems<Post> posts = getPosts();
            List<Post> newPosts = new List<Post>();

            foreach (Post post in posts)
                newPosts.Add(pluginEngine.ProcessDisplayOfPost(context, () => post));

            return new PageOfItems<Post>(newPosts, posts.PageIndex, posts.PageSize, posts.TotalItemCount);
        }
    }
}
