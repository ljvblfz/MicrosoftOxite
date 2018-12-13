//  --------------------------------
//  Copyright (c) Microsoft Corporation. All rights reserved.
//  This source code is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.codeplex.com/oxite/license
//  ---------------------------------
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
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
    public class CommentsModule : IOxiteModule
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

            //TODO: (erikpo) Once there is a xml file provider, put this in an if statement based off of the site setting for which provider to use
            container
                .RegisterType<OxiteCommentsDataContext>(new InjectionConstructor(new ResolvedParameter<string>("ApplicationServices")))
                .RegisterType<ICommentRepository, SqlServerCommentRepository>();
        }

        #endregion
    }
}
