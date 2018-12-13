//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Oxite.Configuration;
using Oxite.Infrastructure;
using Oxite.Modules.Comments.ModelBinders;
using Oxite.Modules.Comments.Models;
using Oxite.Modules.Comments.Repositories;
using Oxite.Modules.Comments.Repositories.SqlServer;
using Oxite.Modules.Comments.Services;
using Oxite.Modules.Comments.Validation;
using Oxite.Validation;

namespace Oxite.Modules.Comments
{
    public class CommentsModule : IOxiteModule, IOxiteDataProvider
    {
        private readonly IUnityContainer container;

        public CommentsModule(IUnityContainer container)
        {
            this.container = container;
        }

        #region IOxiteModule Members

        public void Initialize()
        {
        }

        public void Unload()
        {
        }

        public void RegisterRoutes(RouteCollection routes)
        {
        }

        public void RegisterCatchAllRoutes(RouteCollection routes)
        {
        }

        public void RegisterFilters(IFilterRegistry filterRegistry)
        {
        }

        public void RegisterModelBinders(ModelBinderDictionary modelBinders)
        {
            modelBinders[typeof(CommentInput)] = new CommentInputModelBinder();
        }

        public void RegisterWithContainer()
        {
            container
                .RegisterType<ICommentService, CommentService>()
                .RegisterType<IValidator<CommentInput>, CommentInputValidator>();
        }

        #endregion

        #region IOxiteDataProvider Members

        public void ConfigureProvider(OxiteConfigurationSection config, OxiteDataProviderConfigurationElement dataProviderConfig, IUnityContainer container)
        {
            if (dataProviderConfig.Category == "LinqToSql")
                container
                    .RegisterType<OxiteCommentsDataContext>(new InjectionConstructor(new ResolvedParameter<string>(!string.IsNullOrEmpty(dataProviderConfig.ConnectionString) ? dataProviderConfig.ConnectionString : config.Providers.DefaultConnectionString)))
                    .RegisterType<ICommentRepository, SqlServerCommentRepository>();
        }

        #endregion
    }
}
