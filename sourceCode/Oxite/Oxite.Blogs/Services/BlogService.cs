//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Oxite.Infrastructure;
using Oxite.Models;
using Oxite.Modules.Blogs.Extensions;
using Oxite.Modules.Blogs.Models;
using Oxite.Modules.Blogs.Repositories;
using Oxite.Plugins.Extensions;
using Oxite.Plugins.Models;
using Oxite.Services;
using Oxite.Validation;

namespace Oxite.Modules.Blogs.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository repository;
        private readonly IValidationService validator;
        private readonly IPluginEngine pluginEngine;
        private readonly IOxiteCacheModule cache;
        private readonly OxiteContext context;

        public BlogService(IBlogRepository repository, IValidationService validator, IPluginEngine pluginEngine, IModulesLoaded modules, OxiteContext context)
        {
            this.repository = repository;
            this.validator = validator;
            this.pluginEngine = pluginEngine;
            this.cache = modules.GetModules<IOxiteCacheModule>().Reverse().First();
            this.context = context;
        }

        #region IBlogService Members

        public bool GetBlogExists(string blogName)
        {
            return cache.GetItem<bool?>(
                string.Format("GetBlogExists-Name:{0}", blogName),
                () => repository.GetBlog(blogName) != null,
                null
                ).GetValueOrDefault(false);
        }

        public Blog GetBlog(string blogName)
        {
            return cache.GetItem<Blog>(
                string.Format("GetBlog-Name:{0}", blogName),
                () => repository.GetBlog(blogName),
                a => getBlogDependencies(a)
                );
        }

        public IPageOfItems<Blog> GetBlogs(PagingInfo pagingInfo)
        {
            //TODO: (erikpo) Add caching

            return repository.GetBlogs(pagingInfo);
        }

        public IEnumerable<Blog> FindBlogs(BlogSearchCriteria criteria)
        {
            return Enumerable.Empty<Blog>();
                /*repository.GetBlogs().ToArray()
                .Where(a => 
                    a.Name.IndexOf(criteria.Name, StringComparison.OrdinalIgnoreCase) != -1 ||
                    a.DisplayName.IndexOf(criteria.Name, StringComparison.OrdinalIgnoreCase) != -1
                    ).ToArray();*/
        }

        private void validateBlog(Blog newBlog, ValidationStateDictionary validationState)
        {
            validateBlog(newBlog, newBlog, validationState);
        }

        private void validateBlog(Blog newBlog, Blog originalBlog, ValidationStateDictionary validationState)
        {
            ValidationState state = new ValidationState();
            Blog foundBlog;

            validationState.Add(typeof(Blog), state);

            foundBlog = repository.GetBlog(newBlog.Name);

            if (foundBlog != null && newBlog.Name != originalBlog.Name)
                state.Errors.Add(new ValidationError("Blogs.NameNotUnique", newBlog.Name, "A blog already exists with the supplied name"));
        }

        private ModelResult<Blog> editBlog<T>(Blog blog, T input, Func<Blog, Blog> applyInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(T), validator.Validate(input));

            if (!validationState.IsValid) return new ModelResult<Blog>(validationState);

            Blog originalBlog = blog;
            Blog newBlog;

            using (TransactionScope transaction = new TransactionScope())
            {
                newBlog = applyInput(originalBlog);

                validateBlog(newBlog, originalBlog, validationState);

                if (!validationState.IsValid) return new ModelResult<Blog>(validationState);

                newBlog = repository.Save(newBlog);

                invalidateCachedBlogForEdit(newBlog, originalBlog);

                transaction.Complete();
            }

            pluginEngine.ExecuteAll("BlogEdited", new { context, blog = new BlogReadOnly(newBlog), blogOriginal = new BlogReadOnly(originalBlog) });

            return new ModelResult<Blog>(newBlog, validationState);
        }

        public ModelResult<Blog> AddBlog(BlogInput blogInput)
        {
            ValidationStateDictionary validationState = new ValidationStateDictionary();

            validationState.Add(typeof(BlogInput), validator.Validate(blogInput));

            if (!validationState.IsValid) return new ModelResult<Blog>(validationState);

            Blog blog;

            using (TransactionScope transaction = new TransactionScope())
            {
                blog = blogInput.ToBlog(context.Site.ID);

                validateBlog(blog, validationState);

                if (!validationState.IsValid) return new ModelResult<Blog>(validationState);

                blog = repository.Save(blog);

                invalidateCachedBlogDependencies(blog);

                transaction.Complete();
            }

            pluginEngine.ExecuteAll("BlogAdded", new { context, blog = new BlogReadOnly(blog) });

            return new ModelResult<Blog>(blog, validationState);
        }

        public ModelResult<Blog> EditBlog(Blog blog, BlogInput blogInput)
        {
            return editBlog(blog, blogInput, b => b.Apply(blogInput));
        }

        public ModelResult<Blog> EditBlog(Blog blog, BlogInputForImport blogInput)
        {
            return editBlog(blog, blogInput, b => b.Apply(blogInput));
        }

        public void RemoveBlog(Blog blog)
        {
            if (blog == null) return;

            using (TransactionScope transaction = new TransactionScope())
            {
                if (repository.Remove(blog))
                {
                    invalidateCachedBlogForRemove(blog);

                    transaction.Complete();
                }

                transaction.Complete();
            }

            pluginEngine.ExecuteAll("BlogRemoved", new { context, blog = new BlogReadOnly(blog) });

            return;
        }

        #endregion

        #region Private Methods

        private IEnumerable<ICacheEntity> getBlogDependencies(Blog blog)
        {
            List<ICacheEntity> dependencies = new List<ICacheEntity>();

            dependencies.Add(blog.Site);

            dependencies.Add(blog);

            return dependencies;
        }

        private void invalidateCachedBlogDependencies(Blog blog)
        {
            cache.InvalidateItem(blog.Site);

            cache.Invalidate(string.Format("GetBlogExists-Name:{0}", blog.Name));
        }

        private void invalidateCachedBlogForEdit(Blog newBlog, Blog originalBlog)
        {
            cache.InvalidateItem(newBlog);
        }

        private void invalidateCachedBlogForRemove(Blog blog)
        {
            invalidateCachedBlogDependencies(blog);

            cache.InvalidateItem(blog);
        }

        #endregion
    }
}
